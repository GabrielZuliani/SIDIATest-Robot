using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour 
{
	private enum ViewMode
	{
		Menu,
		ChooseRobot,
		Instructions
	}

	public GameObject menuScreen;
	public GameObject chooseRobotScreen;
	public GameObject instructionsScreen;
	public Button playButton;
	public Button chooseRobotButton;
	public Button instructionsButton;
	public RobotChangeManager robotChanger;
	public Button changeRobot1;
	public Button changeRobot2;
	public Button changeRobot3;
	public Button robotBack;
	public Button instructionsBack;

	private ViewMode viewMode;

	private void Awake()
	{
		ShowMenuScreen ();
		playButton.onClick.AddListener (() => {
			SceneManager.LoadScene(1);
		});
		chooseRobotButton.onClick.AddListener (() => {
			ShowChooseRobot();
		});
		instructionsButton.onClick.AddListener (() => {
			ShowInstructions();
		});
		changeRobot1.onClick.AddListener (() => {
			ChangeRobot(RobotType.Robot1);
		});
		changeRobot2.onClick.AddListener (() => {
			ChangeRobot(RobotType.Robot2);
		});
		changeRobot3.onClick.AddListener (() => {
			ChangeRobot(RobotType.Robot3);
		});

		robotBack.onClick.AddListener (() => {
			ShowMenuScreen();
		});

		instructionsBack.onClick.AddListener (() => {
			ShowMenuScreen ();
		});

		ChangeRobot (RobotChangeManager.CurrentType);

	}
		
	private void ChangeRobot(RobotType type)
	{
		changeRobot1.interactable = true;
		changeRobot2.interactable = true;
		changeRobot3.interactable = true;

		if (type == RobotType.Robot1)
			changeRobot1.interactable = false;
		else if (type == RobotType.Robot2)
			changeRobot2.interactable = false;
		else if (type == RobotType.Robot3)
			changeRobot3.interactable = false;

		robotChanger.ChangeRobot (type);
	}

	private void ShowChooseRobot()
	{
		menuScreen.gameObject.SetActive (false);
		chooseRobotScreen.gameObject.SetActive (true);
		instructionsScreen.gameObject.SetActive (false);
		viewMode = ViewMode.ChooseRobot;
	}

	private void ShowMenuScreen()
	{
		menuScreen.gameObject.SetActive (true);
		chooseRobotScreen.gameObject.SetActive (false);
		instructionsScreen.gameObject.SetActive (false);
		viewMode = ViewMode.Menu;
	}

	private void ShowInstructions()
	{
		instructionsScreen.gameObject.SetActive (true);
		viewMode = ViewMode.Instructions;
	}
		
		

}
