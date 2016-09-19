using UnityEngine;
using System.Collections;

public class CCTVMonitorsController : MonoBehaviour
{
	public SecurityCameraButton [] cctvCameras;

	public void MouseOverMonitor(SecurityCameraButton cctvCamera)
	{
		foreach(SecurityCameraButton cameraButton in cctvCameras)
		{
			if(cameraButton == cctvCamera || cameraButton.selectedMonitor)
			{
				cameraButton.Select();
				continue;
			}

			cameraButton.Disable();

		}
    }

	public void UpdateSelectedMonitor()
	{
		foreach (SecurityCameraButton cameraButton in cctvCameras)
		{
			cameraButton.UpdateSelectedMonitor();
		}
	}

}