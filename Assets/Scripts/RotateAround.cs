using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour 
{
	enum Axis
	{
		x,y,z
	}

	[SerializeField]private Axis axis;
	[SerializeField] private float speed;
	private Vector3 rotationAxis;

	private void Start()
	{
		rotationAxis = transform.right;
		if (axis == Axis.y) rotationAxis = transform.up;
		else if (axis == Axis.z) rotationAxis = transform.forward;
	}

	private void Update()
	{
		transform.Rotate (rotationAxis * speed * Time.deltaTime);
	}

}
