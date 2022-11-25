using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class MouseInputSystem : IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter<CameraRef> _cameras;
		private EcsFilter<Player> _players;

		public void Run()
		{
			
			if (Input.GetMouseButton(0))
			{
				if (TryGetRayHitPoint(out Vector2 point))
				{
					var player = _players.GetEntity(0);
					ref var commandMove = ref player.Get<CommandMoveTo>();
					ref var commandRotate = ref player.Get<CommandRotateTo>();
					commandMove.Target = point;
					commandRotate.Target = point;
				}
			}

			if (Input.GetMouseButton(1))
			{
				if (TryGetRayHitPoint(out Vector2 point))
				{
					var player = _players.GetEntity(0);
					ref var command = ref player.Get<CommandRotateTo>();
					command.Target = point;
				}
			}
		}

		private bool TryGetRayHitPoint(out Vector2 point)
		{
			var camera = _cameras.Get1(0).Value;
			point = Vector2.zero;

			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out var hitInfo))
			{
				point = new Vector2(hitInfo.point.x, hitInfo.point.z);
				return true;
			}
			return false;
		}
	}
}