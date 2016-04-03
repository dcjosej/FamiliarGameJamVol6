﻿using UnityEngine;
using System.Collections;

public class RotationCamera : MonoBehaviour {

	public float minAngle = 0f;
	public float maxAngle = 0f;

	public float minAngleUp = 0f;
	public float maxAngleUp = 0f;

	private float rotationVelocity = 40;
	private Vector3 rotationVector = Vector3.zero;

	//public Camera cameraRT;

	public bool isActive;
	public bool normalCameraRotation = false;

	void Update()
	{
		CheckKeyBoard();
	}

	public void CheckKeyBoard()
	{

		float angleToCheck = 0f;

		float angleUpToCheck = transform.localEulerAngles.x < 180 ? transform.eulerAngles.x : transform.eulerAngles.x - 360; ;
		Debug.Log("EEEEEEE!!!! ======= " + angleUpToCheck);

		if (!normalCameraRotation)
		{
			angleToCheck = transform.localEulerAngles.y < 180 ? transform.eulerAngles.y : transform.eulerAngles.y - 360;
		}
		else
		{
			angleToCheck = transform.localEulerAngles.y;
		}


		//float angleToCheck = transform.localEulerAngles.y;

		if (isActive)
		{
			if (Input.GetKey(KeyCode.A) && angleToCheck >= minAngle)
			{
				rotationVector.Set(0, -rotationVelocity * Time.deltaTime, 0f);
				transform.Rotate(rotationVector, Space.World);
			}
			else if (Input.GetKey(KeyCode.D) && angleToCheck <= maxAngle)
			{
				rotationVector.Set(0, rotationVelocity * Time.deltaTime, 0f);
				transform.Rotate(rotationVector, Space.World);
			}

			if (Input.GetKey(KeyCode.W) && angleUpToCheck >= minAngleUp)
			{
				rotationVector.Set(-rotationVelocity * Time.deltaTime, 0f, 0f);
				transform.Rotate(rotationVector, Space.Self);
			}
			else if (Input.GetKey(KeyCode.S) && angleUpToCheck <= maxAngleUp)
			{
				rotationVector.Set(rotationVelocity * Time.deltaTime, 0f, 0f);
				transform.Rotate(rotationVector, Space.Self);
			}

		}
	}
}