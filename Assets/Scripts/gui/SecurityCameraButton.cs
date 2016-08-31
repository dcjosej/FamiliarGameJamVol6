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

	public LightCameraController lightCameraController;
	public LightCamera lightCamera;

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
		UpdateMainScreen();
		lightCameraController.LightOn(lightCamera);
    }

	public void UpdateMainScreen()
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