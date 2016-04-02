using UnityEngine;
using System.Collections;

public class RotationCamera : MonoBehaviour {

	public float minAngle = 0f;
	public float maxAngle = 0f;

	private float rotationVelocity = 40;
	private Vector3 rotationVector = Vector3.zero;

	void Update()
	{
		CheckKeyBoard();
	}

	public void CheckKeyBoard()
	{

		float angleToCheck = transform.localEulerAngles.y < 180 ? transform.localEulerAngles.y : transform.localEulerAngles.y - 360;

		if (Input.GetKey(KeyCode.A) && angleToCheck >= minAngle)
		{
			//Quaternion newRotation = Quaternion.Euler(0f, newEulerAngles.y - rotationVelocity * Time.deltaTime, 0f);
			//newEulerAngles.Set(0f, newEulerAngles.y - rotationVelocity * Time.deltaTime, 0f);
			rotationVector.Set(0, -rotationVelocity * Time.deltaTime, 0f);
			transform.Rotate(rotationVector, Space.World);
			//transform.rotation = newRotation;
		}
		else if (Input.GetKey(KeyCode.D) && angleToCheck <= maxAngle)
		{
			//Quaternion newRotation = Quaternion.Euler(0f, newEulerAngles.y + rotationVelocity * Time.deltaTime, 0f);
			//1newEulerAngles.Set(0f, newEulerAngles.y + rotationVelocity * Time.deltaTime, 0f);
			rotationVector.Set(0, rotationVelocity * Time.deltaTime, 0f);
			transform.Rotate(rotationVector, Space.World);
			//transform.eulerAngles = newEulerAngles;
			//transform.rotation = newRotation;
		}
	}
}