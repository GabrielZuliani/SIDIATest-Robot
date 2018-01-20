using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelGenerator))]
public class Level : MonoBehaviour
{
    public RobotMovement robot;
    private LevelGenerator levelGenerator;
    private Vector3 initialPosition;

    public int NumberOfLanes
    {
        get { return levelGenerator.NumberOfLanes; }
    }
    
    private void Awake()
    {
        levelGenerator = GetComponent<LevelGenerator>();
        transform.position -= Vector3.forward * levelGenerator.LaneSize;
        initialPosition = transform.position;
        
        levelGenerator.Generate();
    }

    private void Update()
    {
        transform.position -= Vector3.forward * robot.CurrentSpeed * Time.deltaTime;
        if (Vector3.Distance(transform.position, initialPosition) > levelGenerator.LaneSize * levelGenerator.PathLength/3)
            transform.position = initialPosition;
    }

    public Vector3 GetLanePosition(int laneIndex)
    {
        var rightMultiplier = levelGenerator.LaneSize * (-levelGenerator.NumberOfLanes / 2 + laneIndex + (1f - (float)levelGenerator.NumberOfLanes % 2)/2 );
        Debug.Log(rightMultiplier);
        return initialPosition + Vector3.right * rightMultiplier + Vector3.forward * levelGenerator.LaneSize * 10 + Vector3.up * levelGenerator.LaneSize;
    }
}
