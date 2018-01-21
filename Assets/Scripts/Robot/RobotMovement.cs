using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RobotMovement : MonoBehaviour, IMessageReceiver
{
    [SerializeField] private float baseSpeed;
	[SerializeField] private float maxSpeed = 20;
    [SerializeField] private float speedIncreasePerUnit;
	[SerializeField] private float switchLaneSpeed = 3f;
	[SerializeField] private float initialDelay = 3f;
    private float currentSpeed;
    private int currentLane = 0;
	private int forwardTilePosition = 4;
	private Level level;
	private Vector3? targetPosition;
	private float tempSpeed;
	private bool isStunned;
	private bool isDepleted;

	public event Action OnStartedMoving;
 
	public bool IsSwitchingLanes
	{
		get{ return targetPosition != null; }
	}

	public void Init (Level level)
    {
		this.level = level;
        currentSpeed = baseSpeed;
		currentLane = level.NumberOfLanes / 2;
		transform.position = level.GetLanePosition(currentLane, forwardTilePosition);
		var input = BaseInputDetector.AddInputTo (gameObject);
		input.OnLeftInput += MoveLeft;
		input.OnRightInput += MoveRight;
		StartCoroutine (UpdateCoroutine());
    }

    public float CurrentSpeed
    {
        get { return currentSpeed; }
    }

	public float BaseSpeed
	{
		get { return baseSpeed;}
	}
    
	private IEnumerator UpdateCoroutine()
    {
		tempSpeed = currentSpeed;
		currentSpeed = 0;
		isStunned = true;
		yield return new WaitForSeconds (initialDelay);
		isStunned = false;
		currentSpeed = tempSpeed;
		if (OnStartedMoving != null) OnStartedMoving ();

		while (!isDepleted) 
		{
			Debug.Assert (level != null);
			currentSpeed += speedIncreasePerUnit * Time.deltaTime;
			if (currentSpeed > maxSpeed)
				currentSpeed = maxSpeed;

			if (IsSwitchingLanes) 
			{
				transform.position = Vector3.MoveTowards (transform.position, targetPosition.Value, switchLaneSpeed * Time.deltaTime);
				if (transform.position == targetPosition.Value)
					targetPosition = null;
			}
			yield return null;
		}
    }

    private void MoveLeft()
    {
		if (CantSwitch() || currentLane == 0)
            return;
        currentLane--;
		targetPosition = level.GetLanePosition(currentLane, forwardTilePosition);
    }

    private void MoveRight()
    {
		if(CantSwitch() || currentLane == level.NumberOfLanes - 1)
            return;
        currentLane++;
		targetPosition = level.GetLanePosition(currentLane, forwardTilePosition);
    }

	private bool CantSwitch()
	{
		return IsSwitchingLanes || isStunned || isDepleted;
	}

	private void ObstacleHit(Obstacle obs)
	{
		StartCoroutine (ObstacleHitSequence (obs));
	}

	private IEnumerator ObstacleHitSequence(Obstacle obs)
	{
		isStunned = true;
		tempSpeed = currentSpeed;
		currentSpeed = 0;
		yield return new WaitForSeconds (obs.StopDelay);
		if (!isDepleted) 
		{
			currentSpeed = tempSpeed - obs.SpeedReduction;
			if (currentSpeed < baseSpeed)
				currentSpeed = baseSpeed;
			isStunned = false;
		}
	}

	public void ReceiveMessage (BaseMessage message)
	{
		if (message is ObstacleHitMessage)
		{
			var ohm = (ObstacleHitMessage)message;
			ObstacleHit (ohm.Obstacle);
		} 
		else if (message is BatteryDepletedMessage) 
		{
			isDepleted = true;
			currentSpeed = 0;
		}
	}
}
