using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class CommandHandleSystem : IEcsRunSystem
	{
		private EcsFilter<CommandMoveTo, Position2D> _filterMoveTo;
		private EcsFilter<CommandRotateTo, Position2D, Rotation> _filterRotateTo;

		public void Run()
		{
			foreach (int index in _filterMoveTo)
			{
				Debug.Log($"Foreach {index}");
				
				CommandMoveTo moveToCommand = _filterMoveTo.Get1(index);
				Position2D entityPosition2D = _filterMoveTo.Get2(index);
				var entity = _filterMoveTo.GetEntity(index);

				if (!entity.Has<MoveEngineTask>())
				{
					ref var newMoveTask = ref entity.Get<MoveEngineTask>();
					newMoveTask.StartPosition = entityPosition2D.Value;
					newMoveTask.EndPosition = moveToCommand.Target;
					newMoveTask.Distance = Vector2.Distance(newMoveTask.StartPosition, newMoveTask.EndPosition);
					newMoveTask.Timer = 0.0f;
					newMoveTask.Progress = 0.0f;
					//Debug.Log($"{index}Exc: {newMoveTask.StartPosition}-{newMoveTask.EndPosition} Progress:{newMoveTask.Progress}");
					entity.Del<CommandMoveTo>();
					return;
				}
				
				ref var moveTask = ref entity.Get<MoveEngineTask>();
				if (moveTask.EndPosition != moveToCommand.Target)
				{
					moveTask.StartPosition = entityPosition2D.Value;
					moveTask.EndPosition = moveToCommand.Target;
					moveTask.Distance = Vector2.Distance(moveTask.StartPosition, moveTask.EndPosition);
					moveTask.Timer = 0.0f;
					moveTask.Progress = 0.0f;
				}
				entity.Del<CommandMoveTo>();
				//Debug.Log($"{index}Exc: {moveTask.StartPosition}-{moveTask.EndPosition} Progress:{moveTask.Progress}");
			}

			foreach (int index in _filterRotateTo)
			{
				CommandRotateTo rotateToCommand = _filterRotateTo.Get1(index);
				Position2D entityPosition2D = _filterMoveTo.Get2(index);
				Rotation entityRotation = _filterRotateTo.Get3(index);
				var entity = _filterRotateTo.GetEntity(index);

				if (rotateToCommand.Target == entityPosition2D.Value)
				{
					return;
				}
				
				if (!entity.Has<MoveEngineTask>())
				{
					ref var newRotateTask = ref entity.Get<RotateEngineTask>();
					var newEndRotation = Quaternion.LookRotation(new Vector3(
						rotateToCommand.Target.x - entityPosition2D.Value.x,
						0.0f,
						rotateToCommand.Target.y - entityPosition2D.Value.y
					));
					newRotateTask.StartRotation = entityRotation.Value;
					newRotateTask.EndRotation = newEndRotation;
					newRotateTask.Progress = 0.0f;
					entity.Del<CommandRotateTo>();
					return;
				}
				
				ref var rotateTask = ref entity.Get<RotateEngineTask>();
				var endRotation = Quaternion.LookRotation(new Vector3(
					rotateToCommand.Target.x - entityPosition2D.Value.x,
					0.0f,
					rotateToCommand.Target.y - entityPosition2D.Value.y
				));
				if (rotateTask.EndRotation != endRotation)
				{
					rotateTask.StartRotation = entityRotation.Value;
					rotateTask.EndRotation = endRotation;
					rotateTask.Progress = 0.0f;
				}
				entity.Del<CommandRotateTo>();
			}
		}
	}
}