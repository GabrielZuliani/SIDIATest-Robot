using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotCollision : MonoBehaviour 
{
	private RobotMessenger messenger;

	private void Awake()
	{
		messenger = GetComponentInParent<RobotMessenger> ();
		Debug.Assert (messenger != null, "Couldn't find a Messenger in the GameObject");
	}

	private void OnTriggerEnter(Collider c)
	{
		var battery = c.gameObject.GetComponent<Battery> ();
		if (battery) 
		{
			messenger.Broadcast(new BatteryCollectedMessage(battery));
		}
		else
		{
			var obstacle = c.gameObject.GetComponent<Obstacle> ();
			if (obstacle) 
			{
				messenger.Broadcast (new ObstacleHitMessage (obstacle));
			}
		}
	}
}
