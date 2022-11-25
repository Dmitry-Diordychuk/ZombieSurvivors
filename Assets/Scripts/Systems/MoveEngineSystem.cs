using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class MoveEngineSystem : IEcsRunSystem
	{
		private EcsFilter<MoveEngineTask, CharacterControllerRef, Position2D, TransformRef, MoveSpeed> _filter;
		
		public void Run()
		{
			foreach (int index in _filter)
			{
				CharacterControllerRef characterControllerRef = _filter.Get2(index);
				ref TransformRef transformRef = ref _filter.Get4(index);
				float moveSpeed = _filter.Get5(index).Value;

				ref MoveEngineTask moveTask = ref _filter.Get1(index);
				if (moveTask.StartPosition == moveTask.EndPosition) // TODO: почему равны?
				{
					_filter.GetEntity(index).Del<MoveEngineTask>();
					Debug.LogWarning("Start and End positions are equal!");
				}
				
				ref Position2D position2D = ref _filter.Get3(index);
				Vector2 nextPosition = Vector2.Lerp(moveTask.StartPosition, moveTask.EndPosition, moveTask.Progress);
				characterControllerRef.Value.Move(new Vector3(
					nextPosition.x - position2D.Value.x,
					0.0f,
					 nextPosition.y - position2D.Value.y
				));

				Vector3 position = transformRef.Value.transform.position;
				position2D.Value = new Vector2(position.x, position.z);
				Debug.Log($"After: {position2D.Value}");

				moveTask.Progress = moveTask.Timer / moveTask.Distance;
				moveTask.Timer += Time.deltaTime * moveSpeed;
				
				if (moveTask.Progress > 1.0f)
				{
					_filter.GetEntity(index).Del<MoveEngineTask>();
				}
			}
		}
	}
}