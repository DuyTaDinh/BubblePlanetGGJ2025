using Gameplay;
using UnityEngine;
using Utilities;
namespace Managers
{
	public class DataManager : Singleton<DataManager>
	{
		[SerializeField] private int currentScore = 1;
		[SerializeField] private int currentGameLevel = 1;
		[SerializeField] private BubbleField bubbleField;
		[SerializeField] private int scorePerClick = 1;
		[SerializeField] private int additionalScorePerClick = 0;
		[SerializeField] [Range(0.1f, 0.5f)] private float clickIntervalAuto = 0.3f;
		
		private bool gameEnded = false;
		private int highestScore = 1;
		private int currentUpgradeLevel = 0;
		private bool isAutoClickEnabled = false;

		public BubbleField BubbleField => bubbleField;
		public float ClickIntervalAuto => clickIntervalAuto;
		public void CalculateLevel()
		{
			// if score = 1 -> level = 1, if score = 100 -> level = 2, if score = 1000 -> level = 10
			int newLevel = Mathf.CeilToInt(currentScore / 100f);
			if (newLevel == currentGameLevel)
			{
				return;
			}
			currentGameLevel = newLevel;
			EventManager.TriggerEvent(EventName.ChangeBubbleStep, newLevel);
		}

		public int GetCurrentLevel()
		{
			return currentGameLevel;
		}

		public bool IsGameEnded()
		{
			return gameEnded;
		}

		public void SetGameEnded()
		{
			gameEnded = true;
		}

		public void AddScore(int amount)
		{
			if (amount <= 0)
			{
				Debug.LogError("Score amount must be greater than 0");
				return;
			}
			currentScore += amount;
			highestScore = Mathf.Max(highestScore, currentScore);
			EventManager.TriggerEvent(EventName.ChangeScore, amount);
			CalculateLevel();
		}

		public void DecreaseScore(int amount)
		{
			if (amount <= 0)
			{
				Debug.LogError("Score amount must be greater than 0");
				return;
			}
			int deltaChange = Mathf.Min(amount, currentScore);
			currentScore -= deltaChange;
			if(currentScore <= 0)
			{
				EventManager.TriggerEvent(EventName.GameOver, 1);
				return;
			}
			EventManager.TriggerEvent(EventName.ChangeScore, -deltaChange);
			CalculateLevel();
		}

		public int GetScore()
		{
			return currentScore;
		}

		public int GetScorePerClick()
		{
			return scorePerClick;
		}
		
		public int GetAdditionalScorePerClick()
		{
			return additionalScorePerClick;
		}

		public void IncreaseAdditionalScorePerClick(int amount)
		{
			if (amount <= 0)
			{
				Debug.LogError("Score amount must be greater than 0");
				return;
			}
			additionalScorePerClick += amount;
		}

		public string GetHighestScoreText()
		{
			return $"{highestScore/100f}%";
		}
		
		public void ChangeAutoClick()
		{
			isAutoClickEnabled = !isAutoClickEnabled;
			EventManager.TriggerEvent(EventName.ChangeAutoClick, isAutoClickEnabled ? 1 : 0);
		}
		
		public bool IsAutoClickEnabled()
		{
			return isAutoClickEnabled;
		}
	}
}