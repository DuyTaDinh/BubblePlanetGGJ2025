using Gameplay;
using UnityEngine;
using Utilities;
namespace Managers
{
	public class DataManager : Singleton<DataManager>
	{
		[SerializeField] private int currentGameLevel = 1;
		[SerializeField] private BubbleField bubbleField;
		private bool gameEnded = false;
		private int currentScore = 1;
		
		public BubbleField BubbleField => bubbleField;
		
		public void UpdateCurrentLevel(int newLevel)
		{
			if (newLevel == currentGameLevel) return;
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
			if(amount <= 0)
			{
				Debug.LogError("Score amount must be greater than 0");
				return;
			}
			currentScore += amount;
			EventManager.TriggerEvent(EventName.ChangeScore, amount);
		}
		
		public void DecreaseScore(int amount)
		{
			if(amount <= 0)
			{
				return;
			}
			int deltaChange = Mathf.Min(amount, currentScore);
			currentScore -= deltaChange;
			EventManager.TriggerEvent(EventName.ChangeScore, -deltaChange);
		}
		
		public int GetScore()
		{
			return currentScore;
		}
	}
}