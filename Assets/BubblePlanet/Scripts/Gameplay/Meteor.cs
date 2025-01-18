using System;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Gameplay
{
	public class Meteor : MonoBehaviour
	{
		[SerializeField] private float minSpeed = 4f;
		[SerializeField] private float maxSpeed = 5f;
		[SerializeField] private float directionVariationMin = 0.3f;
		[SerializeField] private float directionVariationMax = 0.5f;
		[SerializeField] private GameObject bubbleVisual;
		
		[SerializeField] private ParticleSystem meteorExplosion;
		
		// private Animator animator;
		// private Faction faction;
		private Vector3 direction;
		private float currentSpeed;
		private EntityState entityState = EntityState.Normal;

		private void Start()
		{
			currentSpeed = Random.Range(minSpeed, maxSpeed);
			bubbleVisual.SetActive(false);
		}

		public void Init(Vector3 spawnPosition, Vector2 initialDirection)
		{
			transform.position = spawnPosition;
			InitializeComponents();
			SetMeteorType();
			SetRandomDirection(initialDirection);
			AdjustRotation();

			// GetComponent<Anim_Shrink>().EnableShrink();
		}

		private void InitializeComponents()
		{
			// faction = GetComponent<Faction>();
			// animator = transform.GetChild(0).GetComponent<Animator>();
		}

		private void SetMeteorType()
		{
			// faction.SetRandom();
			// animator.SetInteger("Type", faction.IntType());
			// animator.SetTrigger("Initialize");
		}

		private void SetRandomDirection(Vector2 initialDirection)
		{
			float variation = Random.Range(directionVariationMin, directionVariationMax);
			variation *= Random.value > 0.5f ? 1 : -1;

			if (Mathf.Abs(initialDirection.x) > Mathf.Abs(initialDirection.y))
			{
				direction = new Vector3(initialDirection.x, variation);
			}
			else
			{
				direction = new Vector3(variation, initialDirection.y);
			}

			direction.Normalize();
		}

		private void AdjustRotation()
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}

		private void Update()
		{
			switch (entityState)
			{
				case EntityState.Normal:
					OnNormalState(Time.deltaTime);
					break;
				case EntityState.Bubble:
					OnBubbleState();
					break;
			}
		}

		private void OnNormalState(float deltaTime)
		{
			transform.Translate(currentSpeed * deltaTime * direction, Space.World);
		}

		private void OnBubbleState()
		{
			

		}

		private void OnTriggerExit2D(Collider2D other)
		{
			if (other.CompareTag(TagName.DeathZone))
			{
				Destroy(gameObject);
			}
		}
		void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag(TagName.Meteor))
			{
				// ParticleSystem bullet = Instantiate(meteorExplosion, gameObject.transform.position, transform.rotation);
				// bullet.Play();
				Destroy(gameObject);

			}
			
			switch (entityState)
			{
				case EntityState.Normal:
					if (other.CompareTag(TagName.BubbleProjectile))
					{
						ChangeToBubble();
					}
					if (other.CompareTag(TagName.Planet))
					{
						int currentBubbleLevel = DataManager.Instance.GetCurrentLevel();
						DataManager.Instance.DecreaseScore(10 * currentBubbleLevel);
						Destroy(gameObject);
					}
					break;
				case EntityState.Bubble:
					if (other.CompareTag(TagName.Planet))
					{
						int currentBubbleLevel = DataManager.Instance.GetCurrentLevel();
						DataManager.Instance.AddScore(currentBubbleLevel * 5);
						Destroy(gameObject);
					}
					break;
			}
		}
		void ChangeToBubble()
		{
			entityState = EntityState.Bubble;
			gameObject.tag = TagName.Bubble;
			bubbleVisual.SetActive(true);
		}

	}
}