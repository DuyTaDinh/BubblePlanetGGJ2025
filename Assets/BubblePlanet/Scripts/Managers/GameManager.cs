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
		[SerializeField] private TextMeshProUGUI tooltipText;
		[SerializeField] private TextMeshProUGUI resultText;
		[SerializeField] private List<GameObject> disableWhenGameOver;
		[SerializeField] private List<GameObject> enableWhenGameOver;
		[SerializeField] private List<GameObject> enableWhenGameWin;

		[Header("Button Auto Click")]
		[SerializeField] private Outline autoClickOutline;
		[SerializeField] private TextMeshProUGUI autoClickText;

		[SerializeField] private ParticleSystem planetExplosion;
		[SerializeField] private GameObject planet;
		[SerializeField] private GameObject planetBubbleField;
		
		private Dictionary<string, Action<int>> eventListeners;

		protected override void Awake()
		{
			base.Awake();
			InitializeEventListeners();
			StartCoroutine(ShowTooltip());
		}
		
		IEnumerator ShowTooltip()
		{
			yield return new WaitForSeconds(0.1f);
			AudioManager.Instance.PlaySound(SoundName.GameTheme);
			tooltipText.text = $"<size=20>Left Mouse: Click to Increases the bubble size</size>";
			Tween.StopAll(tooltipText.transform);
			Tween.Scale(tooltipText.transform, 0, 1, 0.3f, startDelay:0.5f);
			Tween.Scale(tooltipText.transform, 1, 0, 0.15f, startDelay:3.5f);
			
			yield return new WaitForSeconds(3.5f);
			tooltipText.text = $"<size=20>Right Mouse: Click to Shoots the meteors</size>";
			Tween.Scale(tooltipText.transform, 0, 1, 0.3f, startDelay:4.5f);
			Tween.Scale(tooltipText.transform, 1, 0, 0.15f, startDelay:7f);
			yield return null;
		}
		
		public void DontBeOverhead()
		{
			tooltipText.text = $"<size=20>Don't spam! Just Chill!</size>";
			Tween.StopAll(tooltipText.transform);
			Tween.Scale(tooltipText.transform, 0, 1, 0.3f);
			Tween.Scale(tooltipText.transform, 1, 0, 0.15f, startDelay:2.5f);
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
			planet.SetActive(false);
			planetBubbleField.SetActive(false);
			ParticleSystem bullet = Instantiate(planetExplosion, planet.transform.position, transform.rotation);
			bullet.Play();
			AudioManager.Instance.PlaySound(SoundName.PlanetExplosion);
			yield return new WaitForSecondsRealtime(0.5f);
			DataManager.Instance.SetGameEnded();
			yield return new WaitForSecondsRealtime(0.5f);
			Tween.StopAll(resultText.transform);
			string gameOverText = $"<size=90>GAME OVER</size>\n \n<size=30>HIGHEST PROGRESS</size>\n<size=60>{DataManager.Instance.GetHighestScoreText()}</size>\n";
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
			Tween.StopAll(resultText.transform);
			string gameOverText = $"<size=90>YOU WIN!</size>\n \n<size=30> </size>\n<size=60> </size>\n";
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