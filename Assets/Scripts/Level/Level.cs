using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(LevelGenerator))]
public class Level : MonoBehaviour
{
    private LevelGenerator levelGenerator;
    private Vector3 initialPosition;
	// save robotMovement to have access to robot speed
	private RobotMovement robotMov;
	private Transform levelGeometry;
	private List<int> availableLastLanes = new List<int>();
	private float distance;

	public event Action OnGameOver;

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

	public Vector3 EndPosition
	{
		get { return transform.position - Vector3.forward * levelGenerator.LaneSize; }
	}
    
    private void Awake()
    {
        levelGenerator = GetComponent<LevelGenerator>();
		levelGeometry = levelGenerator.Generate();
		levelGeometry.parent = transform;
		levelGeometry.position -= Vector3.forward * levelGenerator.LaneSize;
		initialPosition = levelGeometry.position;  
  
		var robot = RobotFactory.GetRobot (RobotChangeManager.CurrentType);
		robotMov = robot.GetComponent<RobotMovement> ();
		Debug.Assert (robotMov != null, "Robot Prefab has no Robot Movement Component");
		robotMov.Init (this);

		var spawners = GetComponentsInChildren<Spawner> ();
		foreach (var s in spawners)
			s.Init (this);

		var hudController = GetComponentInChildren<HUDController> ();
		if (hudController != null)
			hudController.Init (robot);

		FillAvailableLastLanes ();
    }

	private void FillAvailableLastLanes()
	{
		availableLastLanes.Clear ();
		for (int i = 0; i < levelGenerator.NumberOfLanes; i++)
			availableLastLanes.Add (i);
	}
		
    private void Update()
    {
		levelGeometry.position -= Vector3.forward * robotMov.CurrentSpeed * Time.deltaTime;
		if (Vector3.Distance(levelGeometry.position, initialPosition) > levelGenerator.LaneSize * levelGenerator.PathLength - levelGenerator.PortalDistance)
			levelGeometry.position = initialPosition;

		distance += robotMov.CurrentSpeed * Time.deltaTime;
		if (distance > levelGenerator.LaneSize)
		{
			distance = 0;
			FillAvailableLastLanes ();
		}
		
    }

	//used to avoid  spawning Level Objects on the same tile
	public Vector3 GetUniqueEndPathPosition()
	{
		var rnd = UnityEngine.Random.Range (0, availableLastLanes.Count);
		var pos = GetLanePosition (availableLastLanes[rnd], levelGenerator.PathLength);
		availableLastLanes.RemoveAt (rnd);
		return pos;
	}

    public Vector3 GetLanePosition(int laneIndex, int forwardOffset)
    {
        var rightMultiplier = levelGenerator.LaneSize * (-levelGenerator.NumberOfLanes / 2 + laneIndex + (1f - (float)levelGenerator.NumberOfLanes % 2)/2 );
		return initialPosition + Vector3.right * rightMultiplier + Vector3.forward * levelGenerator.LaneSize * forwardOffset + Vector3.up * levelGenerator.LaneSize;
    }
}
