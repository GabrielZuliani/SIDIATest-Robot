using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RobotType
{
	Robot1,
	Robot2,
	Robot3
}

public static class RobotFactory
{
	private static Dictionary<RobotType, string> prefabNames = new Dictionary<RobotType, string>
	{
		{RobotType.Robot1, "Robot1"},
		{RobotType.Robot2, "Robot2"},
		{RobotType.Robot3, "Robot3"}
	};

	private static Dictionary<RobotType, Object> prefabsCache = new Dictionary<RobotType, Object> ();

	public static GameObject GetRobot(RobotType type)
	{
		Object robotPrefab = null;
		if (prefabsCache.ContainsKey (type))
			robotPrefab = prefabsCache [type];
		else 
		{
			robotPrefab = Resources.Load ("Robots/" + prefabNames [type], typeof(GameObject));
			Debug.Assert (robotPrefab != null, "Couldn't Find Prefab Robots/" + prefabNames [type]);
			prefabsCache [type] = robotPrefab;
		}

		return GameObject.Instantiate(robotPrefab) as GameObject;
	}
}
