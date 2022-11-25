using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class RotateEngineSystem : IEcsRunSystem
	{
		private EcsFilter<RotateEngineTask, CharacterControllerRef, Rotation> _filter;
		
		public void Run()
		{
			foreach (int index in _filter)
			{
				CharacterControllerRef characterControllerRef = _filter.Get2(index);
				//float moveSpeed = _filter.Get4(index).Value;

				ref RotateEngineTask rotateTask = ref _filter.Get1(index);
				if (rotateTask.StartRotation == rotateTask.EndRotation) // TODO: почему равны?
				{
					_filter.GetEntity(index).Del<MoveEngineTask>();
					Debug.LogWarning("Start and End rotations are equal!");
					return;
				}
				
				Quaternion nextRotation = Quaternion.Slerp(rotateTask.StartRotation, rotateTask.EndRotation, rotateTask.Progress);
				characterControllerRef.Value.transform.rotation = nextRotation;
				ref Rotation rotation = ref _filter.Get3(index);
				rotation.Value = characterControllerRef.Value.transform.rotation;

				rotateTask.Progress += Time.deltaTime;
				
				if (rotateTask.Progress > 1.0f)
				{
					_filter.GetEntity(index).Del<RotateEngineTask>();
				}
			}
		}
	}
}