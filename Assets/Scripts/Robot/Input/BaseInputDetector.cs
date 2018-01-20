using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseInputDetector : MonoBehaviour 
{
	public event Action OnLeftInput;
	public event Action OnRightInput;

	protected void RaiseLeft()
	{
		if (OnLeftInput != null)
			OnLeftInput ();
	}

	protected void RaiseRight()
	{
		if (OnRightInput != null)
			OnRightInput ();
	}

	public static BaseInputDetector AddInputTo(GameObject target)
	{
		#if UNITY_EDITOR
		return target.AddComponent<EditorInput>();
		#elif UNITY_ANDROID
		return target.AddComponent<AndroidInput>();
		#endif
		Debug.LogError("No Input Detectors found!");
		return null;
	}


}
