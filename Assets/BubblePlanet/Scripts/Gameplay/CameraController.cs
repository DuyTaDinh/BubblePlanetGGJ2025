using System;
using Managers;
using UnityEngine;
using Utilities;
namespace Gameplay
{
	public class CameraController : Singleton<CameraController>
	{
		[Header("Camera Setting")]
		[SerializeField] private int currentStep = 1;
		[SerializeField] private float sizePerStep = 8f;
		[SerializeField] private float zoomSpeed = 5f;
		
		private Camera _mainCamera;
		private float _targetSize;
		const float BubbleSizeThreshold = 0.7f;
		protected override void Awake()
		{
			base.Awake();
			// set the main camera to aspect ratio 16:9
			_mainCamera = Camera.main;
			if (_mainCamera != null)
			{
				_mainCamera.aspect = 16f / 9f;
				CalculateTargetSize();
				_mainCamera.orthographicSize = _targetSize;
			}
		}
		void LateUpdate()
		{
			CalculateTargetSize();
			_mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, _targetSize, Time.deltaTime * zoomSpeed);
		}

		private void CalculateTargetSize()
		{
			float bubbleFieldRadius = DataManager.Instance.BubbleField.Radius;
			// float maxBubbleFieldRadius = currentStep * BubbleSizeThreshold;
			currentStep = Mathf.CeilToInt(bubbleFieldRadius / BubbleSizeThreshold);
			_targetSize = currentStep * sizePerStep;
		}
	}
}