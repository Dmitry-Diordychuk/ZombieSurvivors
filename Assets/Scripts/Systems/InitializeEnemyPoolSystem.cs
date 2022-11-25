using Leopotam.Ecs;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class InitializeEnemyPoolSystem : IEcsInitSystem
	{
		private EcsWorld _world;
		private Configuration _configuration;

		public void Init()
		{
			_configuration.EnemyPool.Init(_configuration.EnemyPoolSize, _configuration.EnemyView);
			_configuration.EnemyPool.GeneratePool();
			var entity = _world.NewEntity();
			entity.Get<EnemyCounter>();
		}
	}
}