using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RobotMovement), typeof(RobotMessenger))]
public class BatteryManager : MonoBehaviour , IMessageReceiver
{
	[SerializeField] private float maxBattery;
	[SerializeField] private float decreaseSpeed;
	private RobotMovement robMov;
	private RobotMessenger robMes;
	private bool isDepleted;

	public float CurrentBatteryAmount { get; private set; }
 	public float MaxBattery { get { return maxBattery; } }

	private void Awake()
	{
		robMov = GetComponent<RobotMovement> ();
		robMes = GetComponent<RobotMessenger> ();
		CurrentBatteryAmount = maxBattery;
	}

	private void Update()
	{
		if (isDepleted) return;
			
		CurrentBatteryAmount -= decreaseSpeed * robMov.CurrentSpeed / robMov.BaseSpeed * Time.deltaTime;
		if (CurrentBatteryAmount < 0) 
		{
			robMes.Broadcast (new BatteryDepletedMessage ());
			isDepleted = true;
		}
	}
		
	public void ReceiveMessage (BaseMessage message)
	{
		if (message is BatteryCollectedMessage) 
		{
			CurrentBatteryAmount += ((BatteryCollectedMessage)message).Battery.BatteryIncrease;
			if (CurrentBatteryAmount > maxBattery)
				CurrentBatteryAmount = maxBattery;
		} 
		else if (message is ObstacleHitMessage) 
		{
			CurrentBatteryAmount -= ((ObstacleHitMessage)message).Obstacle.Damage;
		}
	}
}
