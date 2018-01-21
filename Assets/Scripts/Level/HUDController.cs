using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour 
{
	[SerializeField] private Text scoreText;
	[SerializeField] private Slider batteryBar;
	[SerializeField] private GameObject retryScreen;
	[SerializeField] private Text retryScoreTex;
	private ScoreManager score;
	private BatteryManager battery;

	public void Init(GameObject player)
	{
		this.score = player.GetComponent<ScoreManager>();
		this.battery = player.GetComponent<BatteryManager>();
	}

	private void Update () 
	{
		scoreText.text = "Score : " + score.Score;
		batteryBar.value = battery.CurrentBatteryAmount / battery.MaxBattery;
	}
}
