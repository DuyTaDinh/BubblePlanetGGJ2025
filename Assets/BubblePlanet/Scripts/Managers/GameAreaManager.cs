using System;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using Utilities;
namespace Managers
{
	public class GameAreaManager : Singleton<GameAreaManager>
	{
		public Transform area;
		public BoxCollider2D spawnerMeteorBounds; 
		public BoxCollider2D deathZoneBounds;

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
				{ EventName.ChangeBubbleStep, OnChangeBubbleStep }
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
		
		void OnChangeBubbleStep(int step)
		{
			Vector3 scale = area.localScale;
			scale.x = step;
			scale.y = step;
			area.localScale = scale * 1.2f;
		}
		
	}
}