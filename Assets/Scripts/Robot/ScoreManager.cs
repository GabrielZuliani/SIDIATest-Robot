using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RobotMovement))]
public class ScoreManager : MonoBehaviour
{
	public int Score { get { return Mathf.CeilToInt (score); }  }

	[SerializeField] private int scoreIncreasePerUnit;
	private RobotMovement robMov;
	private float distance;
	private float score;

	private void Awake()
	{
		robMov = GetComponent<RobotMovement>();
	}

	private void Update()
	{
		distance += robMov.CurrentSpeed * Time.deltaTime;
		score = distance * scoreIncreasePerUnit;
	}
}
