using Managers;
using UnityEngine;
namespace Gameplay
{
	public class BubbleProjectile : MonoBehaviour
	{
		[SerializeField] private float speed = 9f;
		
		private Vector3 direction;
		
		public void Init(Vector3 spawnPosition, Vector2 initialDirection)
		{
			transform.position = spawnPosition;
			float additionalScale = DataManager.Instance.GetCurrentLevel() * 0.4f;
			transform.localScale = new Vector3(additionalScale, additionalScale, 1);
			direction = initialDirection;
			direction.Normalize();
			AdjustRotation();
		}
		
		private void AdjustRotation()
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}

		private void Update()
		{
			transform.Translate(speed * Time.deltaTime * direction, Space.World);
		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag(TagName.DeathZone))
			{
				Destroy(gameObject);
			}
		}
	}
}