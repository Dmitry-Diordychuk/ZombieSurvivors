using UnityEngine;

namespace ZombieSurvivors.Components
{
	internal struct MoveEngineTask
	{
		public Vector2 StartPosition;
		public Vector2 EndPosition;
		public float Progress;
		public float Distance;
		public float Timer;
	}
}