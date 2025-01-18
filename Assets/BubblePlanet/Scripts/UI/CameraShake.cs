using UnityEngine;
namespace UI
{
	public class CameraShake : MonoBehaviour
	{
		public Transform camTransform;

		private static float _shakeDuration = 0f;

		private static float _shakeAmount = 0.7f;

		private float _vel;
		private Vector3 _vel2 = Vector3.zero;

		Vector3 _originalPos;

		void Awake()
		{
			if (camTransform == null)
			{
				camTransform = transform;
			}

			_originalPos = camTransform.localPosition;
		}

		// length : 0.15f => 0.5f
		// strength : 1f => 2.5f
		public static void ShakeOnce(float lenght, float strength)
		{
			_shakeDuration = lenght;
			_shakeAmount = strength;
		}

		void Update()
		{

			if (_shakeDuration > 0)
			{
				Vector3 newPos = _originalPos + Random.insideUnitSphere * _shakeAmount;

				camTransform.localPosition = Vector3.SmoothDamp(camTransform.localPosition, newPos, ref _vel2, 0.05f);

				_shakeDuration -= Time.deltaTime;
				_shakeAmount = Mathf.SmoothDamp(_shakeAmount, 0, ref _vel, 0.7f);
			}
			else
			{
				camTransform.localPosition = _originalPos;
			}

		}
	}
}