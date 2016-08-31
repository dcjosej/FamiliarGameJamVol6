using UnityEngine;
using System.Collections;

public class LightCameraController : MonoBehaviour
{
	public Material lightOn;

	public LightCamera[] lightsCameras;

	public void LightOn(LightCamera lightCamera)
	{
		lightCamera.TurnOn(lightOn);

		foreach(LightCamera lc in lightsCameras)
		{
			if (lc == lightCamera)
			{
				continue;
			}

			lc.TurnOff();
		}
	}
}