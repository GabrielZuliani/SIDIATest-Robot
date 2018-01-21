using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	[SerializeField] private ObjectPool pool;
	[SerializeField] private float timeToSpawn;
	[SerializeField] private float difficultyIncrease;
	[SerializeField] private float minTimer = 0.2f;
	[SerializeField] private float maxTimer = 10f;
	protected Level level;
	private float timer;

	public void Init(Level level)
	{
		this.level = level;
		timer = timeToSpawn;
	}

	protected void Update()
	{
		if (level.Speed > 0) 
		{
			timer += Time.deltaTime;
			if (timer > timeToSpawn) 
			{
				Spawn ();
				timeToSpawn -= difficultyIncrease;
				if (timeToSpawn < minTimer) timeToSpawn = minTimer;
				if (timeToSpawn > maxTimer) timeToSpawn = maxTimer;
				timer = 0;
			}
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
		return level.GetUniqueEndPathPosition ();
	}
}
