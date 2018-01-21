using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMessenger : MonoBehaviour 
{
	private List<IMessageReceiver> receivers;

	private void Awake()
	{
		receivers = new List<IMessageReceiver>(GetComponentsInChildren<IMessageReceiver>());
	}

	public void Subscribe(IMessageReceiver receiver)
	{
		receivers.Add (receiver);
	}
		
	public void Broadcast(BaseMessage message)
	{
		foreach (var r in receivers) 
		{
			r.ReceiveMessage (message);
		}
	}
}
