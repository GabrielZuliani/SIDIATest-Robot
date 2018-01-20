using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour 
{
	private Level level;

	public void Init(Level level)
	{
		this.level = level;
	}

	private void Update()
	{
		if (level == null)
			return;

		transform.position -= Vector3.forward * level.Speed * Time.deltaTime;
	}
}
