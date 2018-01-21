using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidInput : BaseInputDetector 
{
	private Vector3 firstPos;   
	private Vector3 lastPos;   
	private float screenHeightPercent = 0.1f;
	private float swipeDistance; 

	void Start()
	{
		swipeDistance = Screen.width * screenHeightPercent;
	}

	void Update()
	{
		if (Input.touchCount == 1) 
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) 
			{
				firstPos = touch.position;
				lastPos = touch.position;
			} 
			else if (touch.phase == TouchPhase.Moved) 
			{
				lastPos = touch.position;
			} 
			else if (touch.phase == TouchPhase.Ended) 
			{
				lastPos = touch.position;  
				CheckSwipe ();
			}
		}
	}

	private void CheckSwipe()
	{
		if (Mathf.Abs (lastPos.x - firstPos.x) > swipeDistance) 
		{
			if ((lastPos.x > firstPos.x)) 
				RaiseRight ();
			else    
				RaiseLeft ();
		}
	}
}
