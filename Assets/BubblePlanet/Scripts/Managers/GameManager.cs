using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using PrimeTween;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;
namespace Managers
{
	public class GameManager : Singleton<GameManager>
	{
		[Header("Game Over UI")]
		[SerializeField] private TextMeshProUGUI resultText;
		[SerializeField] private List<GameObject> disableWhenGameOver;
		[SerializeField] private List<GameObject> enableWhenGameOver;
		[SerializeField] private List<GameObject> enableWhenGameWin;

		[Header("Button Auto Click")]
		[SerializeField] private Outline autoClickOutline;
		[SerializeField] private TextMeshProUGUI autoClickText;

		private Dictionary<string, Action<int>> eventListeners;

		protected override void Awake()
		{
			base.Awake();
			InitializeEventListeners();
		}
		private void InitializeEventListeners()
		{
			eventListeners = new Dictionary<string, Action<int>>
			{
				{
					EventName.ChangeScore, OnChangeScore
				},
				{
					EventName.ChangeAutoClick, OnChangeAutoClick
				},
				{
					EventName.GameOver, OnGameOver
				}
			};
		}

		void Start()
		{
			OnChangeAutoClick(DataManager.Instance.IsAutoClickEnabled() ? 1 : 0);
		}
		private void OnEnable()
		{
			RegisterEventListeners();
		}

		private void OnDisable()
		{
			UnregisterEventListeners();
		}

		private void RegisterEventListeners()
		{
			foreach (var listener in eventListeners)
			{
				EventManager.StartListening(listener.Key, listener.Value);
			}
		}

		private void UnregisterEventListeners()
		{
			foreach (var listener in eventListeners)
			{
				EventManager.StopListening(listener.Key, listener.Value);
			}
		}

		void OnChangeScore(int scoreChange)
		{
			if (!DataManager.Instance.IsGameEnded())
			{
				HandleGameState();
			}
		}
		
		void OnGameOver(int state)
		{
			StartCoroutine(GameOver());
		}

		void OnChangeAutoClick(int autoClickChange)
		{
			Color effectColor = autoClickChange > 0 ? Color.green : Color.gray;
			autoClickText.color = effectColor;
			autoClickOutline.effectColor = effectColor;
		}


		private void HandleGameState()
		{
			int currentScore = DataManager.Instance.GetScore();
			if (currentScore <= 0)
			{
				StartCoroutine(GameOver());
			}
			else if (currentScore >= 1000)
			{
				StartCoroutine(GameWin());
			}
		}

		void Update()
		{
#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.V))
			{
				StartCoroutine(GameWin());
			}
			if (Input.GetKeyDown(KeyCode.B))
			{
				StartCoroutine(GameOver());
			}
#endif
		}
		IEnumerator GameOver()
		{
			DataManager.Instance.SetGameEnded();
			yield return new WaitForSecondsRealtime(0.5f);
			int finalScore = DataManager.Instance.GetMaxScore();
			string gameOverText = $"<size=90>GAME OVER</size>\n \n<size=30>FINAL SCORE</size>\n<size=60>{finalScore}</size>\n";
			resultText.text = gameOverText;
			Tween.Scale(resultText.transform, 0, 1, 0.3f, Easing.Bounce(1));
			foreach (var obj in disableWhenGameOver)
			{
				obj.SetActive(false);
			}
			foreach (var obj in enableWhenGameOver)
			{
				obj.SetActive(true);
			}
		}

		IEnumerator GameWin()
		{
			DataManager.Instance.SetGameEnded();
			yield return new WaitForSecondsRealtime(0.5f);
			int finalScore = DataManager.Instance.GetMaxScore();
			string gameOverText = $"<size=90>YOU WIN!</size>\n \n<size=30>FINAL SCORE</size>\n<size=60>{finalScore}</size>\n";
			resultText.text = gameOverText;
			Tween.Scale(resultText.transform, 0, 1, 0.3f, Easing.Bounce(1));

			foreach (var obj in disableWhenGameOver)
			{
				obj.SetActive(false);
			}
			foreach (var obj in enableWhenGameWin)
			{
				obj.SetActive(true);
			}
		}

#region Button Actions
		public void Retry()
		{
			// Time.timeScale = 1f;
			Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.buildIndex);
		}
		
		public void ChangeAutoClick()
		{
			DataManager.Instance.ChangeAutoClick();
		}

#endregion
		
	}
}