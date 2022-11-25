using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class CameraControlSystem : IEcsRunSystem
	{
		private EcsFilter<Player, Position2D> _players;
		private EcsFilter<CameraRef, TransformRef, CameraOffset> _cameras;

		public void Run()
		{
			ref var cameraTransform = ref _cameras.Get2(0).Value;
			var cameraOffset = _cameras.Get3(0).Value; 
			var playerPosition = _players.Get2(0).Value;
			
			cameraTransform.position = new Vector3(playerPosition.x, 0.0f, playerPosition.y) + cameraOffset;
		}
	}
}