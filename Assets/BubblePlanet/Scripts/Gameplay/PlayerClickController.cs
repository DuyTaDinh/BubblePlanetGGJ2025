using Managers;
using UnityEngine;
namespace Gameplay
{
	public class PlayerClickController : MonoBehaviour
	{
		[SerializeField] private BoxCollider2D clickCollider;
		[SerializeField] private BubbleField bubbleField;

		float _lastClickTime = 0f;
		
		private void Update()
		{
			if(DataManager.Instance.IsGameEnded()) return;
			
			if(Input.GetMouseButtonDown(1))
			{
				FireBubbleProjectile();
			}else if (DataManager.Instance.IsAutoClickEnabled())
			{
				AutoClick();
			}
			else if (Input.GetMouseButtonDown(0))
			{
				Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				if (clickCollider.OverlapPoint(clickPosition))
				{
					AddScore();
				}
				
			}
		}
		void AutoClick()
		{
			if(Time.time - _lastClickTime > DataManager.Instance.ClickIntervalAuto)
			{
				AddScore();
				_lastClickTime = Time.time;
			}
		}
		
		void AddScore()
		{
			int value = DataManager.Instance.GetScorePerClick();
			int additional = DataManager.Instance.GetAdditionalScorePerClick();
			DataManager.Instance.AddScore(value + additional);
		}
		void FireBubbleProjectile()
		{
			if(DataManager.Instance.GetScore() <= 5) return;
			DataManager.Instance.DecreaseScore(5);
			Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 center = transform.position;
			Vector2 direction = clickPosition - center;
			SpawnerManager.Instance.SpawnBubbleProjectile(center, direction);
			AudioManager.Instance.PlaySound(SoundName.FireBubble);
		}

	}
}