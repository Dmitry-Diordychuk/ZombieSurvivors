using System.Collections.Generic;
using UnityEngine;

namespace ZombieSurvivors.UnityComponents
{
	public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
	{
		private int _poolSize;
		private T _prefab;

		private readonly Stack<(int, T)> _objects = new();
		private readonly Dictionary<int, T> _activeObjects = new();

		public void Init(int poolSize, T prefab)
		{
			_poolSize = poolSize;
			_prefab = prefab;
		}
		
		public void GeneratePool()
		{
			for (int i = 0; i < _poolSize; i++)
			{
				T obj = Instantiate(_prefab);
				obj.gameObject.SetActive(false);
				_objects.Push((i, obj));
			}
		}

		public bool TryGetObject(out int id, out T obj)
		{
			if (_objects.TryPop(out var tupleValue))
			{
				id = tupleValue.Item1;
				obj = tupleValue.Item2;
				obj.gameObject.SetActive(true);
				_activeObjects.Add(id, obj);
				return true;
			}

			id = -1;
			obj = null;
			return false;
		}

		public bool ReturnObject(int id, T obj)
		{
			if (_activeObjects.Remove(id))
			{
				_objects.Push((id, obj));
				return true;
			}

			Debug.LogWarning("Wrong object Id", this);
			return false;
		}
	}
}