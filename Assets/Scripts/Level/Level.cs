using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelGenerator))]
public class Level : MonoBehaviour
{
    private LevelGenerator levelGenerator;
    private Vector3 initialPosition;
	// save robotMovement to have access to robot speed
	private RobotMovement robotMov;

    public int NumberOfLanes
    {
        get { return levelGenerator.NumberOfLanes; }
    }

	public int LevelLength
	{
		get { return levelGenerator.PathLength; }
	}

	public float Speed
	{
		get { return robotMov.CurrentSpeed; } 
	}
    
    private void Awake()
    {
        levelGenerator = GetComponent<LevelGenerator>();
		var geometry = levelGenerator.Generate();
		geometry.parent = transform;
		Debug.Log (geometry.position);
		geometry.position += Vector3.forward * levelGenerator.LaneSize * (levelGenerator.PathLength/2 - 7) ;
		Debug.Log (geometry.position);
        initialPosition = transform.position;  
  
		var robot = RobotFactory.GetRobot (RobotType.Robot1);
		robotMov = robot.GetComponent<RobotMovement> ();
		Debug.Assert (robotMov != null, "Robot Prefab has no Robot Movement Component");
		robotMov.Init (this);

		var spawners = GetComponentsInChildren<Spawner> ();
		foreach (var s in spawners)
			s.Init (this);

    }

    private void Update()
    {
        transform.position -= Vector3.forward * robotMov.CurrentSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, initialPosition) > levelGenerator.LaneSize * levelGenerator.PathLength/3)
			transform.position += Vector3.forward * levelGenerator.PathLength/3 * levelGenerator.LaneSize;
    }

    public Vector3 GetLanePosition(int laneIndex, int forwardOffset)
    {
        var rightMultiplier = levelGenerator.LaneSize * (-levelGenerator.NumberOfLanes / 2 + laneIndex + (1f - (float)levelGenerator.NumberOfLanes % 2)/2 );
		return initialPosition + Vector3.right * rightMultiplier + Vector3.forward * levelGenerator.LaneSize * forwardOffset + Vector3.up * levelGenerator.LaneSize;
    }
}
