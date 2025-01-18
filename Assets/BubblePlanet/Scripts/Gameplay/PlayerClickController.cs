using Managers;
using UnityEngine;
namespace Gameplay
{
	public class PlayerClickController : MonoBehaviour
	{
		[SerializeField] private CircleCollider2D clickCollider;
		[SerializeField] private BubbleField bubbleField;
		
		public void ClickBubblePlanet()
		{
			DataManager.Instance.AddScore(1);
		}
		
		private void Update()
		{
			if(DataManager.Instance.IsGameEnded()) return;
			
			if(Input.GetMouseButtonDown(1))
			{
				FireBubbleProjectile();
			}else if (Input.GetMouseButtonDown(0))
			{
				Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				if (clickCollider.OverlapPoint(clickPosition))
				{
					ClickBubblePlanet(); 
				}
			}
		}
		void FireBubbleProjectile()
		{
			if(DataManager.Instance.GetScore() <= 5) return;
			DataManager.Instance.DecreaseScore(5);
			Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 center = clickCollider.bounds.center;
			Vector2 direction = clickPosition - center;
			SpawnerManager.Instance.SpawnBubbleProjectile(center, direction);
		}

	}
}