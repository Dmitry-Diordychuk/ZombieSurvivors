using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class KeyboardInputSystem : IEcsRunSystem
	{
		private EcsWorld _world;
		private EcsFilter<Player, Position2D> _players;

		public void Run()
		{
			if (Input.GetKeyDown(KeyCode.W))
			{
				SendMoveToCommand(new Vector2(1.0f ,0.0f));
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				SendMoveToCommand(new Vector2(-1.0f, 0.0f));
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
				SendMoveToCommand(new Vector2(0.0f, -1.0f));
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
				SendMoveToCommand(new Vector2(0.0f, 1.0f));
			}
		}

		private void SendMoveToCommand(Vector2 offset)
		{
			ref var player = ref _players.GetEntity(0);
			ref var moveToCommand = ref player.Get<CommandMoveTo>();
			var playerPosition = _players.Get2(0).Value;
			
			moveToCommand.Target = new Vector2(playerPosition.x + offset.x, playerPosition.y + offset.y);
		}
	}
}