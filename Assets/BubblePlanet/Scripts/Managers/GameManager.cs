using System;
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
		
		const float LoseThreshold = 0.25f;
		const float WinThreshold = 5f;

		void Update()
		{
			HandleGameState();
		}
		
		private void HandleGameState()
		{
			float bubbleRadius = DataManager.Instance.BubbleField.Radius;
			if(bubbleRadius < LoseThreshold)
			{
				GameOver();
			}
			else if(bubbleRadius > WinThreshold)
			{
				GameWin();
			}
		}

		private void GameOver()
		{
			Time.timeScale = 0f;
			gameOverUI.SetActive(true);
		}
		
		private void GameWin()
		{
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