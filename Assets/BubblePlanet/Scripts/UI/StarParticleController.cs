using System;
using System.Collections.Generic;
using Gameplay;
using Managers;
using UnityEngine;
namespace UI
{
	public class StarParticleController : MonoBehaviour
	{
		[SerializeField] private List<GameObject> startParticles = new List<GameObject>();
		[SerializeField] GameObject shootingStar;
		
		private Dictionary<string, Action<int>> eventListeners;
		void Awake()
		{
			InitializeEventListeners();
		}
		
		void Start()
		{
			OnChangeBubbleStep(DataManager.Instance.GetCurrentLevel());
		}
		
		private void InitializeEventListeners()
		{
			eventListeners = new Dictionary<string, Action<int>>
			{
				{
					EventName.ChangeBubbleStep, OnChangeBubbleStep
				}
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
			for (int i = 0; i < startParticles.Count; i++)
			{
				startParticles[i].SetActive(i >= step - 5 && i < step + 1);
			}
			shootingStar.transform.localScale = new Vector3(0.57f + 1f * (step - 1), 0.57f + 1f * (step - 1), 1);
		}

	}
}