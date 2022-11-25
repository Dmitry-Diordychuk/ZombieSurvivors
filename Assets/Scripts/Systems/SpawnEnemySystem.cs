using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;
using ZombieSurvivors.UnityComponents;

namespace ZombieSurvivors.Systems
{
	internal class SpawnEnemySystem : IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter<EnemyCounter> _filterCounter;
		private Configuration _configuration;
		private int _enemyCount;
        
		public void Run()
		{
			ref var counter = ref _filterCounter.Get1(0).Value;
			for (; counter < _configuration.StartMaxEnemies; counter++)
			{
				if (_configuration.EnemyPool.TryGetObject(out int id, out EnemyView enemyView))
				{
					float randValue = Random.Range(0.0f, 2 * Mathf.PI);
					Vector3 spawnPoint = new Vector3(
						Mathf.Cos(randValue),
						0.0f,
						Mathf.Sin(randValue)
					) * _configuration.EnemyRespawnDistance;
					enemyView.gameObject.transform.position = spawnPoint;

					var enemyEntity = _world.NewEntity();
					enemyEntity.Get<Enemy>().ID = id;
					enemyEntity.Get<EnemyViewRef>().Value = enemyView;
					enemyEntity.Get<CharacterControllerRef>().Value = enemyView.GetComponentInChildren<CharacterController>();
					enemyEntity.Get<TransformRef>().Value = enemyView.transform;
					enemyEntity.Get<Position2D>().Value = new Vector2(spawnPoint.x, spawnPoint.z);
					enemyEntity.Get<Rotation>().Value = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
					enemyEntity.Get<MoveSpeed>().Value = _configuration.EnemyMoveSpeed;
				}
				else
				{
					break;
				}
			}
		}
	}
}