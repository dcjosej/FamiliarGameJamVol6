using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ScoreTimeController : MonoBehaviour
{
	public float timerCount;
	private TimeSpan timeSpan = new TimeSpan(0);
	private bool isActive = true;

	public Text guiScoreTimer { get; set; }


	void OnEnable()
	{
		GameLogic.OnGameOver += OnGameOver;
    }

	void Start()
	{
		guiScoreTimer = GetComponent<Text>();
	}

	void Update()
	{
		if (isActive)
		{
			timerCount += Time.deltaTime;

			float zz = Mathf.Floor((timerCount * 100) % 100);
			float ss = Mathf.Floor(timerCount % 60);
			float mm = Mathf.Floor(timerCount / 60);
			float hh = Mathf.Floor(mm / 60);

			//Debug.Log("" + ss);

			guiScoreTimer.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", hh, mm, ss, zz);
		}
	}

	private void OnGameOver()
	{
		SocialManager.instance.AddScore((int)timerCount, SocialManager.MAIN_LEADERBOARD_ID);
		isActive = false;
	}
}