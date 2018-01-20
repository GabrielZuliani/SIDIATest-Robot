using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Poolable : MonoBehaviour 
{
	public event Action<Poolable> OnDestroyed;

	public void Destroy()
	{
		if (OnDestroyed != null)
			OnDestroyed (this);
	}
}
