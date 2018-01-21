using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUDController : MonoBehaviour, IMessageReceiver
{
	[SerializeField] private Text scoreText;
	[SerializeField] private Slider batteryBar;

	[SerializeField] private GameObject retryScreen;
	[SerializeField] private Text retryScoreText;

	[SerializeField] private GameObject buttonsGroup;
	[SerializeField] private Button retryButton;
	[SerializeField] private Button menuButton;

	[SerializeField] private GameObject pauseScreen;
	[SerializeField] private Button unpauseButton;
	[SerializeField] private Button pauseButton;

	private bool isPaused;
	private ScoreManager score;
	private BatteryManager battery;

	public void Init(GameObject player)
	{
		this.score = player.GetComponent<ScoreManager>();
		this.battery = player.GetComponent<BatteryManager>();
		Debug.Assert (score != null, "Couldn't find ScoreManager in the Player Object");
		Debug.Assert (battery != null, "Couldn't find BatteryManager in the Player Object");
		var messenger = player.GetComponent<RobotMessenger> ();
		Debug.Assert (messenger != null, "Couldn't find RobotMessenger int the Player Obejct");
		messenger.Subscribe (this);

		retryButton.onClick.AddListener (() => {
			if(isPaused) Time.timeScale = 1;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		});

		menuButton.onClick.AddListener (() => {
			if(isPaused) Time.timeScale = 1;
			SceneManager.LoadScene(0);
		});

		pauseButton.onClick.AddListener (() => {
			PauseGame();
		});

		unpauseButton.onClick.AddListener (() => {
			UnpauseGame();
		});

		buttonsGroup.gameObject.SetActive (false);
		retryScreen.gameObject.SetActive (false);
		pauseScreen.gameObject.SetActive (false);
	}

	private void Update () 
	{
		scoreText.text = "Score : " + score.Score;
		batteryBar.value = battery.CurrentBatteryAmount / battery.MaxBattery;
	}
		
	public void ReceiveMessage (BaseMessage message)
	{
		if (message is BatteryDepletedMessage) 
		{
			StartCoroutine (ShowGameOverScreen ());
		}
	}

	private IEnumerator ShowGameOverScreen()
	{
		yield return new WaitForSeconds (1.5f);
		retryScreen.SetActive (true);
		retryScoreText.text = "Score : " + score.Score;
		buttonsGroup.gameObject.SetActive (true);
	}

	private void PauseGame()
	{
		isPaused = true;
		Time.timeScale = 0;
		pauseScreen.gameObject.SetActive (true);
		buttonsGroup.gameObject.SetActive (true);
	}

	private void UnpauseGame()
	{
		isPaused = false;
		Time.timeScale = 1;
		pauseScreen.gameObject.SetActive (false);
		buttonsGroup.gameObject.SetActive (false);
	}
}
