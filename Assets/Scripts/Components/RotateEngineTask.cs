using UnityEngine;

namespace ZombieSurvivors.Components
{
	internal struct RotateEngineTask
	{
		public Quaternion StartRotation;
		public Quaternion EndRotation;
		public float Progress;
	}
}