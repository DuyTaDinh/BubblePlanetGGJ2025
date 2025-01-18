using System;
using System.Collections.Generic;
using Gameplay;
using PrimeTween;
using TMPro;
using UnityEngine;
using Utilities;
namespace Managers
{
	public class ScoreManager : Singleton<ScoreManager>
	{
		[SerializeField] private TextMeshProUGUI scoreText;
		[SerializeField] private TextMeshProUGUI deltaScoreText;
		private Dictionary<string, Action<int>> eventListeners;

		private RectTransform deltaScoreTransform;
		
		void Start()
		{
			deltaScoreTransform = deltaScoreText.GetComponent<RectTransform>();
		}
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
			deltaScoreText.color = scoreChange > 0 ? Color.green : Color.red;
			deltaScoreText.alpha = 1;
			deltaScoreText.text = scoreChange > 0 ? $"+{scoreChange}" : $"{scoreChange}";
			Tween.StopAll(onTarget:deltaScoreText);
			Tween.Scale(deltaScoreText.transform, 0, 1, 0.3f, Easing.Bounce(1));
			Tween.Alpha(deltaScoreText, 1, 0, 0.1f,startDelay:0.3f);
		}
		
		void Update()
		{
			scoreText.text = $"{DataManager.Instance.GetScore()/10f} %";
			deltaScoreTransform.anchoredPosition = new Vector2(scoreText.preferredWidth, scoreText.preferredHeight);
		}
	}
}