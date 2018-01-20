using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float speedIncreasePerUnit;
	[SerializeField] private float switchLaneSpeed = 3f;
    private float currentSpeed;
    private int currentLane = 0;
	private int forwardTilePosition = 6;
	private Level level;
	private Vector3? targetPosition;

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
    }

    public float CurrentSpeed
    {
        get { return currentSpeed; }
    }
    
    private void Update()
    {
		Debug.Assert (level != null);
        currentSpeed += speedIncreasePerUnit * Time.deltaTime ;

		if (IsSwitchingLanes) 
		{
			transform.position = Vector3.MoveTowards (transform.position, targetPosition.Value, switchLaneSpeed * Time.deltaTime);
			if (transform.position == targetPosition.Value)
				targetPosition = null;
		}
    }

    private void MoveLeft()
    {
		if (IsSwitchingLanes || currentLane == 0)
            return;
        currentLane--;
		targetPosition = level.GetLanePosition(currentLane, forwardTilePosition);
    }

    private void MoveRight()
    {
		if(IsSwitchingLanes || currentLane == level.NumberOfLanes - 1)
            return;
        currentLane++;
		targetPosition = level.GetLanePosition(currentLane, forwardTilePosition);
    }
    
    
}
