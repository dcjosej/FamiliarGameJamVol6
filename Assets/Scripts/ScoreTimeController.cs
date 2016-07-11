using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ScoreTimeController : MonoBehaviour
{
	public float timerCount;
	private TimeSpan timeSpan = new TimeSpan(0);

	public Text guiScoreTimer;

	void Update()
	{
		timerCount += Time.deltaTime;
		
		float ss = Mathf.Floor(timerCount % 60);
		float mm = Mathf.Floor(timerCount / 60);

		Debug.Log("" + ss);

		guiScoreTimer.text = string.Format("{0:00}:{1:00}", mm, ss);
	}
}