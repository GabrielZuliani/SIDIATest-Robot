using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorInput : BaseInputDetector 
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) 
		{
			//Debug.Log ("Input Right");
			RaiseRight ();
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) 
		{
			//Debug.Log ("Input Left");
			RaiseLeft ();
		}

	}

}
