using UnityEngine;
using System.Collections;

public class SecurityCameraButton : MonoBehaviour {

	public Camera cameraDest;

	void OnMouseDown()
	{
		Debug.Log("CAMBIAR DE CAMARA!!");

		Camera.main.gameObject.SetActive(false);
		cameraDest.gameObject.SetActive(true);
	}
}
