using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryCollectedMessage : BaseMessage
{
	public Battery Battery { get; private set;}

	public BatteryCollectedMessage(Battery battery)
	{
		Battery = battery;
	}
}
