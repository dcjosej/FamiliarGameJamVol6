using UnityEngine;
using System.Collections;

public class LightCameraController : MonoBehaviour
{
	public Material lightOn;

	public LightCamera[] lightsCameras;


	void Start()
	{
		LightOn(lightsCameras[0]);
	}

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

	public void GameOver()
	{
		foreach(LightCamera lightCamera in lightsCameras)
		{
			lightCamera.TurnOff();
		}
	}
}