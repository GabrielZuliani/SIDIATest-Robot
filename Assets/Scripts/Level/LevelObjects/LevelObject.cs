﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObject : MonoBehaviour 
{
	[SerializeField] private GameObject collisionEffect;
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
		if (transform.position.z < level.EndPosition.z)
			DestroyThis ();

	}

	private void OnTriggerEnter(Collider c)
	{
		if (c.gameObject.GetComponent<RobotCollision>()) 
		{			
			DestroyThis ();
			if (collisionEffect != null)
			{
				var fx = Instantiate (collisionEffect, transform.position, collisionEffect.transform.rotation);
				Destroy (fx, 2f);
			}
		}
	}

	private void DestroyThis()
	{
		var poolable = GetComponent<Poolable> ();
		if (poolable != null)
			poolable.Destroy ();
		else
			Destroy (gameObject);
	}
}
