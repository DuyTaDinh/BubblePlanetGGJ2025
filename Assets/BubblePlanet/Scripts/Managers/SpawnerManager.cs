using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;
namespace Managers
{
	public class SpawnerManager: Singleton<SpawnerManager>
    {
        [SerializeField] private GameObject meteorPrefab;
        [SerializeField][Range(0.1f, 0.3f)] private float spawnIntervalRange = 0.3f;
        [SerializeField] private GameObject bubbleProjectilePrefab;
        
        float spawnMeteorInterval = 2f;
        private Dictionary<string, Action<int>> eventListeners;
        private void Awake()
        {
            InitializeEventListeners();
        }

        void Start()
        {
            OnChangeBubbleStep(DataManager.Instance.GetCurrentLevel());
        }
        private void OnEnable()
        {
            RegisterEventListeners();
            StartCoroutine(SpawnMeteors());
        }

        private void OnDisable()
        {
            UnregisterEventListeners();
            StopAllCoroutines();
        }

        private void InitializeEventListeners()
        {
            eventListeners = new Dictionary<string, Action<int>>
            {
                { EventName.ChangeBubbleStep, OnChangeBubbleStep }
            };
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

        private void OnChangeBubbleStep(int newStep)
        {
            if (newStep < 3)
            {
                spawnMeteorInterval = 2f;
            }else if (newStep < 5)
            {
                spawnMeteorInterval = 1.5f;
            } else if (newStep < 7)
            {
                spawnMeteorInterval = 1.1f;
            } else 
            {
                spawnMeteorInterval = 0.8f;
            }
        }

        private IEnumerator SpawnMeteors()
        {
            while (true)
            {
                if (DataManager.Instance.IsGameEnded())
                {
                    yield break;
                }
                SpawnMeteor();
                yield return new WaitForSecondsRealtime(
                    Random.Range(spawnMeteorInterval - spawnIntervalRange, spawnMeteorInterval + spawnIntervalRange));
            }
        }

        private void SpawnMeteor()
        {
            var meteorObject = Instantiate(meteorPrefab, transform);
            Transform meteorTransform = meteorObject.transform;
            int currentLevel = DataManager.Instance.GetCurrentLevel();
            meteorTransform.localScale = Vector3.one * (1f + 0.3f * (currentLevel - 1));
            var meteor = meteorObject.GetComponent<Meteor>();
#if UNITY_EDITOR
            if (meteor == null)
            {
                throw new Exception("Meteor prefab does not have Meteor component attached.");
            }
#endif

            Vector3 position;
            Vector2 direction;
            GetSpawnPositionAndDirection(out position, out direction);
            meteor.Init(position, direction);
        }

        private void GetSpawnPositionAndDirection(out Vector3 position, out Vector2 direction)
        {
            var bounds = GameAreaManager.Instance.spawnerMeteorBounds.bounds;
            bool spawnOnYAxis = UnityEngine.Random.value > 0.5f;
            bool spawnPositive = UnityEngine.Random.value > 0.5f;

            if (spawnOnYAxis)
            {
                // Spawn on top/bottom
                position = new Vector3(
                    UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
                    spawnPositive ? bounds.max.y : bounds.min.y
                );
                direction = spawnPositive ? Vector2.down : Vector2.up;
            }
            else
            {
                // Spawn on left/right
                position = new Vector3(
                    spawnPositive ? bounds.max.x : bounds.min.x,
                    UnityEngine.Random.Range(bounds.min.y, bounds.max.y)
                );
                direction = spawnPositive ? Vector2.left : Vector2.right;
            }
        }
        
        public void SpawnBubbleProjectile(Vector3 position, Vector2 direction)
        {
            var bubbleProjectileObject = Instantiate(bubbleProjectilePrefab, transform);
            var bubbleProjectile = bubbleProjectileObject.GetComponent<BubbleProjectile>();
#if UNITY_EDITOR
            if (bubbleProjectile == null)
            {
                throw new Exception("BubbleProjectile prefab does not have BubbleProjectile component attached.");
            }
#endif
            bubbleProjectile.Init(position, direction);
        }
    }
}