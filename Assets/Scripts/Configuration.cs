using UnityEngine;
using ZombieSurvivors.UnityComponents;

namespace ZombieSurvivors
{
	[CreateAssetMenu]
	public class Configuration : ScriptableObject
	{
		[Header("Map")]
		public int Width = 30;
		public int Height = 30;
		public FieldView FieldView;
		
		[Header("Player")]
		public PlayerView PlayerView;
		public Vector2 PlayerStartPosition;
		public float PlayerMoveSpeed;
		public Quaternion PlayerStartRotation;
		public float PlayerRotationSpeed;

		[Header("Enemy")]
		public EnemyView EnemyView;
		public EnemyPool EnemyPool;
		public int EnemyPoolSize;
		public int StartMaxEnemies;
		public float EnemyRespawnDistance;
		public float EnemyAiUpdateFrequency;
		public float EnemyMoveSpeed;
		public float EnemyRotationSpeed;
		

		[Header("Camera")]
		public Vector3 CameraOffset;
	}
}