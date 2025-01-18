using System;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;
namespace Managers
{
	public class GameManager : Singleton<GameManager>
	{
		[Header("Game Over UI")]
		[SerializeField] private GameObject gameOverUI;
		[SerializeField] private GameObject gameWinUI;

		private const float LoseThreshold = 0.25f;
		private const float WinThreshold = 5f;

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
				{ EventName.ChangeScore, OnChangeScore }
			};
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
		
		private void HandleGameState()
		{
			int currentScore = DataManager.Instance.GetScore();
			if (currentScore <= 0)
			{
				GameOver();
			}
			else if (currentScore >= 1000)
			{
				GameWin();
			}

			// float bubbleRadius = DataManager.Instance.BubbleField.Radius;
			//
			// if (bubbleRadius < LoseThreshold)
			// {
			// 	GameOver();
			// }
			// else if (bubbleRadius > WinThreshold)
			// {
			// 	GameWin();
			// }
		}

		private void GameOver()
		{
			DataManager.Instance.SetGameEnded();
			Time.timeScale = 0f;
			gameOverUI.SetActive(true);
		}

		private void GameWin()
		{
			DataManager.Instance.SetGameEnded();
			Time.timeScale = 0f;
			gameWinUI.SetActive(true);
		}

		public void Retry()
		{
			Time.timeScale = 1f;
			Scene scene = SceneManager.GetActiveScene();
			SceneManager.LoadScene(scene.buildIndex);
		}
	}
}