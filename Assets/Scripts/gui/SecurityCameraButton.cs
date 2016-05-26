using UnityEngine;
using System.Collections;

public class SecurityCameraButton : MonoBehaviour {

	public MainCameraRTInputReceiver mainCameraInputReceiver;

	public Camera cameraToAsign;

	public Transform mainQuad;
	public Material materialToAsign;

	public RotationCamera[] rotationCameras;

	public MonitorNumber myMonitorNumber;
	public MonitorNumber currentMonitorNumber;


	void Awake()
	{
		//rotationCameras = FindObjectsOfType<RotationCamera>();
	}
	
	private void DisableAllRotationCamera()
	{
		foreach(RotationCamera rotationCamera in rotationCameras)
		{
			rotationCamera.isActive = false;
		}
	}

	void OnMouseDown()
	{
		mainQuad.GetComponent<Renderer>().material = materialToAsign;

		DisableAllRotationCamera();
		cameraToAsign.GetComponent<RotationCamera>().isActive = true;
		//MainCameraRTInputReceiver rtInputReceiver = mainCameraTexture.GetComponent<MainCameraRTInputReceiver>();
		mainCameraInputReceiver.SetCamera(cameraToAsign);

		currentMonitorNumber.monitorNumber = myMonitorNumber.monitorNumber;
		currentMonitorNumber.UpdateNumber();
	}
}