using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	[SerializeField] private ObjectPool pool;
	[SerializeField] private float timeToSpawn;
	protected Level level;
	private float timer;

	public void Init(Level level)
	{
		this.level = level;
	}

	protected void Update()
	{
		timer += Time.deltaTime;
		if (timer > timeToSpawn) 
		{
			Spawn ();
			timer = 0;
		}
	}

	protected virtual GameObject Spawn()
	{
		var obj = pool.GetObject();
		obj.transform.position = GetSpawnPoint ();
		var lvlObj = obj.GetComponent<LevelObject> ();
		if (lvlObj != null) lvlObj.Init (level);
		return obj;
	}

	protected virtual Vector3 GetSpawnPoint()
	{
		return level.GetLanePosition (GetRandomLane (), level.LevelLength);
	}

	protected int GetRandomLane()
	{
		return Random.Range (0, level.NumberOfLanes);
	}
}
