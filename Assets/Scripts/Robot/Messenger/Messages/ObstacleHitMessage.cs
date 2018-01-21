using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHitMessage : BaseMessage 
{
	public Obstacle Obstacle { get; private set; }

	public ObstacleHitMessage(Obstacle obs)
	{
		Obstacle = obs;
	}
}
