using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float speedIncreasePerUnit;
    private float currentSpeed;
    private int currentLane = 0;

    private void Start()
    {
        currentSpeed = baseSpeed;
        transform.position = level.GetLanePosition(currentLane);
    }

    public float CurrentSpeed
    {
        get { return currentSpeed; }
    }
    
    

    private void Update()
    {
        currentSpeed += speedIncreasePerUnit * Time.deltaTime ;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveRight();
    }

    private void MoveLeft()
    {
        if (currentLane == 0)
            return;
        currentLane--;
        transform.position = level.GetLanePosition(currentLane);
    }

    private void MoveRight()
    {
        if(currentLane == level.NumberOfLanes - 1)
            return;
        currentLane++;
        transform.position = level.GetLanePosition(currentLane);
    }
    
    
}
