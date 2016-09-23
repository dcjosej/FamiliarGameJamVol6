using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour
{
	public float deltaTime;
	public float timeScale = 1f;

	void Update ()
	{
		deltaTime = Time.deltaTime * timeScale;
	}
}
