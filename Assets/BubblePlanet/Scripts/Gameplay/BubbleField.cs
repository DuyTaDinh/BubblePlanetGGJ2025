using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
namespace Gameplay
{
	public class BubbleField : MonoBehaviour
	{
		[Header("Bubble Stability Settings")]
		[SerializeField] float maxInstability = 100f;
		[SerializeField] float currentInstability = 0f;

		[SerializeField] SpriteRenderer warningSprite;
		
		[SerializeField] float radius = 0.33f;

		private bool isStable = true;
		private Dictionary<string, Action<int>> eventListeners;
		private float timeFromLastChange = 0f;
		private bool isNotified = false;
		private void Awake()
		{
			InitializeEventListeners();
		}
		

		private void InitializeEventListeners()
		{
			eventListeners = new Dictionary<string, Action<int>>
			{
				{
					EventName.ChangeScore, OnChangeScore
				}
			};
		}

		void Start()
		{
			transform.localScale = Vector3.one * radius * 2f;
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
			if (!isStable) return;
			radius = GetRadius();
			transform.localScale = Vector3.one * radius * 2f;
			timeFromLastChange = 0f;
			float newScorePercentChange = Mathf.Abs((float)scoreChange / Mathf.Max(100, DataManager.Instance.GetScore())) * 100f;
			float instabilityIncreaseRate = CalculateInstabilityRate(newScorePercentChange);
			IncreaseInstability(instabilityIncreaseRate);
		}

		private float GetRadius()
		{
			int currentScore = DataManager.Instance.GetScore();
			float radiusResult = 0.33f;
			if (currentScore <= 100)
			{
				radiusResult += (currentScore - 1) * 0.0037f;
			}
			else
			{
				radiusResult += 0.37f + (currentScore - 100) * 0.007f;
			}
			return radiusResult;
		}

		private float CalculateInstabilityRate(float scorePercentageChange)
		{
			if (scorePercentageChange >= 40f)
				return 40f;
			else if (scorePercentageChange >= 20f)
				return 15f;
			else if (scorePercentageChange >= 10f)
				return 5f;
			else
				return 2f;
		}

		void LateUpdate()
		{
			if (!isStable) return;
			timeFromLastChange += Time.deltaTime;
			if (timeFromLastChange > 0.5f)
			{
				DecreaseStability();
			}
			// Debug.Log($"Stable: {currentInstability}");
			Color color = warningSprite.color;
			color.a = currentInstability / maxInstability;
			warningSprite.color = color;
		}

		private void IncreaseInstability(float amount)
		{
			currentInstability += amount;
			if (currentInstability > maxInstability)
			{
				PopBubble();
			}else if (currentInstability > maxInstability * 0.8f && !isNotified)
			{
				isNotified = true;
				GameManager.Instance.DontBeOverhead();
			}
		}

		private void DecreaseStability()
		{
			currentInstability -= timeFromLastChange;
			currentInstability = Mathf.Max(0, currentInstability);
		}
		private void PopBubble()
		{
			isStable = false;
			EventManager.TriggerEvent(EventName.GameOver, 2);
		}
	}


}