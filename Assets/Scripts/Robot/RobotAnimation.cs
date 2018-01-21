using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RobotAnimation : MonoBehaviour, IMessageReceiver
{
	[SerializeField] private float runSpeed = 15;
	private RobotMovement robMov;
	private Animator animator;
	private bool isWalking;

	private void Awake()
	{
		robMov = GetComponentInParent<RobotMovement> ();
		Debug.Assert (robMov, "Couldn't find RobotMovement in parent");
		animator = GetComponent<Animator> ();
		robMov.OnStartedMoving += StartMoving;
	}

	private void Update()
	{
		if (isWalking && robMov.CurrentSpeed > runSpeed) 
		{
			isWalking = false;
			animator.SetBool ("IsRunning", true);
		}

		if(!isWalking && robMov.CurrentSpeed < runSpeed) 
		{
			isWalking = true;
			animator.SetBool ("IsRunning", false);
		}
	}

	private void StartMoving()
	{
		animator.SetTrigger ("StartMoving");
	}
		
	public void ReceiveMessage (BaseMessage message)
	{
		if (message is ObstacleHitMessage)
			StartCoroutine (DamageAnimation (((ObstacleHitMessage)message).Obstacle.StopDelay));
		else if (message is BatteryDepletedMessage)
			animator.SetTrigger ("PowerOff");
	}

	private IEnumerator DamageAnimation(float delay)
	{
		animator.SetBool ("IsDamaged", true);
		yield return new WaitForSeconds (delay);
		animator.SetBool ("IsDamaged", false);
	}

}
