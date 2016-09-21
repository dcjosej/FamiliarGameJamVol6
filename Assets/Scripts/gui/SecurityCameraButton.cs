using UnityEngine;
using System.Collections;

public class SecurityCameraButton : MonoBehaviour {

	public MainCameraRTInputReceiver mainCameraInputReceiver;
	public CCTVMonitorsController cctvMonitorsController;

	public Camera cameraToAsign;

	public Transform mainQuad;
	public Material materialToAsign;

	public RotationCamera[] rotationCameras;

	public MonitorNumber myMonitorNumber;
	public MonitorNumber currentMonitorNumber;

	public LightCameraController lightCameraController;
	public LightCamera lightCamera;

	public bool selectedMonitor { get; set; }

	void Awake()
	{
		//rotationCameras = FindObjectsOfType<RotationCamera>();
	}

	void OnEnable()
	{
		if(mainCameraInputReceiver.currentMonitorNumber.monitorNumber == myMonitorNumber.monitorNumber)
		{
			selectedMonitor = true;
			myMonitorNumber.text.color = Color.white;
		}
	}
	
	private void DisableAllRotationCamera()
	{
		foreach(RotationCamera rotationCamera in rotationCameras)
		{
			rotationCamera.isActive = false;
		}
	}

	void OnMouseOver()
	{
		cctvMonitorsController.MouseOverMonitor(this);
    }

	void OnMouseExit()
	{
		if (!selectedMonitor)
		{
			myMonitorNumber.OnMouseExit();
		}
	}

	public void Select()
	{
		myMonitorNumber.OnMouseOver();
	}

	public void Disable()
	{
		myMonitorNumber.OnMouseExit();
	}

	public void UpdateSelectedMonitor()
	{
		selectedMonitor = mainCameraInputReceiver.currentMonitorNumber.monitorNumber == myMonitorNumber.monitorNumber;
		if (selectedMonitor)
		{
			cctvMonitorsController.MouseOverMonitor(this);
		}
		else
		{
			OnMouseExit();
		}
	}

	void OnMouseDown()
	{
		if (!selectedMonitor)
		{
			AudioManager.instance.PlayMouseClick();
		}

		UpdateMainScreen();
		lightCameraController.LightOn(lightCamera);
		Select();
		cctvMonitorsController.UpdateSelectedMonitor();
	}

	public void UpdateMainScreen()
	{
		mainQuad.GetComponent<Renderer>().material = materialToAsign;

		DisableAllRotationCamera();
		cameraToAsign.GetComponent<RotationCamera>().isActive = true;
		//MainCameraRTInputReceiver rtInputReceiver = mainCameraTexture.GetComponent<MainCameraRTInputReceiver>();
		mainCameraInputReceiver.SetCamera(cameraToAsign);

		mainCameraInputReceiver.currentMonitorNumber = myMonitorNumber;
        currentMonitorNumber.monitorNumber = myMonitorNumber.monitorNumber;
		currentMonitorNumber.UpdateNumber();
	}
}