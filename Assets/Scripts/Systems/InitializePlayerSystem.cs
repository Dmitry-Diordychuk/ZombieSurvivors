using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class InitializePlayerSystem : IEcsInitSystem
	{
		private EcsWorld _world;
		private Configuration _configuration;

		public void Init()
		{
			var player = _world.NewEntity();
			var position = _configuration.PlayerStartPosition;
			var rotation = _configuration.PlayerStartRotation;
			var playerView = Object.Instantiate(_configuration.PlayerView);

			player.Get<Player>();
			player.Get<CharacterControllerRef>().Value = playerView.GetComponentInChildren<CharacterController>();
			player.Get<Position2D>().Value = position;
			player.Get<Rotation>().Value = rotation;
			player.Get<MoveSpeed>().Value = _configuration.PlayerMoveSpeed;

			var trans = playerView.transform;
			trans.position = new Vector3(position.x, 0.0f, position.y);
			trans.rotation = rotation;

			player.Get<TransformRef>().Value = trans;
			player.Get<PlayerViewRef>().Value = playerView;
		}
	}
}