using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotChangeManager : MonoBehaviour 
{
	public static RobotType CurrentType;

	public GameObject previewPrefab1;
	public GameObject previewPrefab2;
	public GameObject previewPrefab3;
	private GameObject currentModel;

	public void ChangeRobot(RobotType type)
	{
		if (type == RobotChangeManager.CurrentType && currentModel != null)
			return;
		
		if(currentModel != null) Destroy (currentModel);
		RobotChangeManager.CurrentType = type;
		switch (type) 
		{
			case RobotType.Robot1:
				currentModel = Instantiate (previewPrefab1, transform);
				break;
			case RobotType.Robot2:
				currentModel = Instantiate (previewPrefab2, transform);
				break;
			case RobotType.Robot3:
				currentModel = Instantiate (previewPrefab3, transform);
				break;
		}
		currentModel.transform.localPosition = Vector3.zero;
	}
}
