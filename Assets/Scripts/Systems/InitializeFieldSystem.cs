using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class InitializeFieldSystem : IEcsInitSystem
	{
		private Configuration _configuration;
		private EcsWorld _world;

		public void Init()
		{
			for (int y = 0; y < _configuration.Height; y++)
			{
				for (int x = 0; x < _configuration.Width; x++)
				{
					var field = _world.NewEntity();
					field.Get<Field>();
					field.Get<Position2D>().Value = new Vector2(x, y);
				}
			}
		}
	}
}