using System;
using UnityEngine;
namespace UI
{
	public class ParallaxEffect : MonoBehaviour
	{
		[SerializeField] float speed = 2f;
		[SerializeField] int distance = 75;
		private Vector2 _startPos;

		void Start()
		{
			_startPos = transform.localPosition;
		}

		void Update()
		{
			Vector2 mousePos = Input.mousePosition;
			Vector2 camCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
			float t = speed * Time.deltaTime;

			Vector2 normalizedMousePos = (mousePos - camCenter) / new Vector2(Screen.width, Screen.height);
			Vector2 targetPos = -normalizedMousePos * distance;

			transform.localPosition = Vector2.Lerp(transform.localPosition, _startPos + targetPos, t);
		}
	}
}