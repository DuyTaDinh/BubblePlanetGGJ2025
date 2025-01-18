using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
namespace Gameplay
{
	public class BubbleField : MonoBehaviour
	{
		[SerializeField] private float radius = 0.33f;
		// [Header("Warning Indicator")]
		// [SerializeField] private SpriteRenderer warningSprite;
		// [SerializeField, Range(0.5f, 1)] private float warningStartRadiusScale = 0.8f;
		// [SerializeField] private Gradient warningGradient;
		// [SerializeField] private float warningAlphaScale;

		private Dictionary<string, Action<int>> eventListeners;

		private void Awake()
		{
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
			radius += scoreChange * 0.01f;
			transform.localScale = Vector3.one * radius * 2f;
		}
		
		public float GetRadius()
		{
			return radius;
		}
	}
}