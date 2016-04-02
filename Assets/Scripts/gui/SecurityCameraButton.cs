using UnityEngine;
using System.Collections;

public class SecurityCameraButton : MonoBehaviour {

	public Camera cameraDest;

	void OnMouseDown()
	{
		Debug.Log("CAMBIAR DE CAMARA!!");
		
		cameraDest.gameObject.SetActive(false);
		cameraDest.gameObject.SetActive(true);
	}
}
