using UnityEngine;
namespace Gameplay
{
	public class BubbleField : MonoBehaviour
	{
		[SerializeField] private float radius = 0.33f;
		[SerializeField] private float tolerance = 0.1f;
		[Header("Warning Indicator")]
		[SerializeField] private SpriteRenderer warningSprite;
		[SerializeField, Range(0.5f, 1)] private float warningStartRadiusScale = 0.8f;
		[SerializeField] private Gradient warningGradient;
		[SerializeField] private float warningAlphaScale;

		public float Radius => radius;

		void Awake()
		{
			UpdateScale();
#if UNITY_EDITOR
			InvokeRepeating(nameof(UpdateScale), 0.2f, 0.2f);
  #endif
		}
		
		// void Update()
		// {
		// 	radius += Time.deltaTime * 0.1f; // Test
		// }

		public void IncreaseRadius(float amount)
		{
			radius += amount;
			UpdateScale();
		}

		void UpdateScale()
		{
			transform.localScale = Vector3.one * radius * 2f;
		}
	}
}