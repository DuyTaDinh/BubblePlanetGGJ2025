using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Utilities;
namespace Gameplay
{
	public class CameraController : Singleton<CameraController>
	{
		[Header("Camera Setting")]
		[SerializeField] private float sizePerStep = 7f;
		[SerializeField] private float zoomSpeed = 5f;
		
		private Camera _mainCamera;
		private float _targetSize;
		const float BubbleSizeThreshold = 0.7f;
		
		private Dictionary<string, Action<int>> eventListeners;

		protected override void Awake()
		{
			base.Awake();
			InitializeEventListeners();
			// set the main camera to aspect ratio 16:9
			_mainCamera = Camera.main;
			if (_mainCamera != null)
			{
				_mainCamera.aspect = 16f / 9f;
			}
		}

		void Start()
		{
			OnChangeBubbleStep(DataManager.Instance.GetCurrentLevel());
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
		
		void OnChangeBubbleStep(int newStep)
		{
			_targetSize = newStep * sizePerStep;
		}
		
		void LateUpdate()
		{
			_mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, _targetSize, Time.deltaTime * zoomSpeed);
		}
	}
}