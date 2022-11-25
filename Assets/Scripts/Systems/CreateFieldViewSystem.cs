using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Components;

namespace ZombieSurvivors.Systems
{
	internal class CreateFieldViewSystem : IEcsRunSystem
	{
		private EcsFilter<Field, Position2D>.Exclude<FieldViewRef> _filter;
		private Configuration _configuration;

		public void Run()
		{
			foreach (int index in _filter)
			{
				ref var position = ref _filter.Get2(index);
				var fieldView = Object.Instantiate(_configuration.FieldView);
				
				fieldView.transform.position = new Vector3(
					position.Value.x - (float)_configuration.Width / 2, 
					0.0f, 
					position.Value.y - (float)_configuration.Height / 2
				);
				_filter.GetEntity(index).Get<FieldViewRef>().Value = fieldView;
			}
		}
	}
}