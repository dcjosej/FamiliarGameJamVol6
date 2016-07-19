using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class ScoreTimeController : MonoBehaviour
{
	public float timerCount;
	private TimeSpan timeSpan = new TimeSpan(0);

	public Text guiScoreTimer;


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
		timerCount += Time.deltaTime;

		float zz = Mathf.Floor((timerCount * 60)%60);
        float ss = Mathf.Floor(timerCount % 60);
		float mm = Mathf.Floor(timerCount / 60);
		float hh = Mathf.Floor(mm / 60);

		//Debug.Log("" + ss);

		guiScoreTimer.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", hh, mm, ss, zz);
	}

	private void OnGameOver()
	{
		SocialManager.instance.AddScore((int)timerCount, SocialManager.MAIN_LEADERBOARD_ID);
	}

	//--------------- TEST ---------------------
	public void TEST_METHOD()
	{
		Debug.Log("aaaaaaaaaaaaaa---11111111-321-312-321-3-123-1");
	}
}