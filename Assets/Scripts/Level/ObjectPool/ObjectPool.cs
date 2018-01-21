using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour 
{
	[SerializeField] private Poolable poolablePrefab;
	[SerializeField] private int initialPoolSize;
	private Queue<Poolable> objectPool = new Queue<Poolable>();

	private void Awake()
	{
		for(int i = 0; i < initialPoolSize; i++)
		{
			var obj = GetNewObject ();
			AddToPool (obj);
		}
	}

	public GameObject GetObject()
	{
		if (objectPool.Count > 0)
		{
			var obj = objectPool.Dequeue ();
			obj.gameObject.SetActive (true);
			return obj.gameObject;
		} 
		else 
		{
			return GetNewObject().gameObject;
		}
	}

	private void AddToPool(Poolable target)
	{
		objectPool.Enqueue (target);
		target.gameObject.SetActive(false);
	}

	private Poolable GetNewObject()
	{
		var obj = Instantiate (poolablePrefab, transform);
		obj.OnDestroyed += AddToPool;
		return obj;
	}
}
