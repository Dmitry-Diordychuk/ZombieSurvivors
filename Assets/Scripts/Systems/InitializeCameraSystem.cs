using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class InitializeCameraSystem : IEcsInitSystem
	{
		private Configuration _configuration;
		private EcsWorld _world;
		private EcsFilter<Player, Position2D> _players;
		private EcsFilter<CameraRef, TransformRef, CameraOffset> _cameras;

		public void Init()
		{
			var mainCamera = Camera.main;
			var camera = _world.NewEntity();
            
			if (!mainCamera)
			{
				Debug.LogWarning("Main camera is absent!", mainCamera);
			}
			else
			{
				camera.Get<CameraRef>().Value = mainCamera;
				camera.Get<TransformRef>().Value = mainCamera.transform;
				camera.Get<CameraOffset>().Value = _configuration.CameraOffset;
			}
		}
	}
}