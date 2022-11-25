using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class EnemyAISystem : IEcsRunSystem
	{
		private EcsFilter<Player, Position2D> _playerFilter;
		private EcsFilter<Enemy> _enemyFilter;
		private Configuration _configuration;
		
		private float _startTime;
		private bool _isTimerSet;
		
		public void Run()
		{
			if (_isTimerSet && Time.time - _startTime < _configuration.EnemyAiUpdateFrequency)
			{
				return;
			}
			
			foreach (int index in _enemyFilter)
			{
				var enemyEntity = _enemyFilter.GetEntity(index);
				enemyEntity.Get<CommandMoveTo>().Target = _playerFilter.Get2(0).Value;
				enemyEntity.Get<CommandRotateTo>().Target = _playerFilter.Get2(0).Value;
				Debug.Log($"{index} get move command!");
			}

			_startTime = Time.time;
			_isTimerSet = true;
		}
	}
}