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

	private bool recordBeatenFlag = false;

	[Header("Texts")]
	public TextAsset recordBeaten;


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

			if(!recordBeatenFlag && PersistentData.instance.bestScore >= 0 && timerCount > PersistentData.instance.bestScore)
			{
				HUDController.instance.consoleAutoText.TypeText(recordBeaten.text, Utils.GreenColor);
				recordBeatenFlag = true;
            }

			//float zz = Mathf.Floor((timerCount * 100) % 100);
			//float ss = Mathf.Floor(timerCount % 60);
			//float mm = Mathf.Floor(timerCount / 60);
			//float hh = Mathf.Floor(mm / 60);

			//Debug.Log("" + ss);

			//guiScoreTimer.text = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", hh, mm, ss, zz);
			guiScoreTimer.text = Utils.SecondsTozzHHmmss(timerCount);
        }
	}

	private void OnGameOver()
	{
		if (SocialManager.instance != null)
		{
			if(timerCount > PersistentData.instance.bestScore)
			{
				PersistentData.instance.bestScore = timerCount;
			}
			SocialManager.instance.AddScore((int)timerCount, guiScoreTimer.text);
		}
		else
		{
			Debug.Log("NO HAS INICIADO EL JUEGO DESDE LA ESCENA 1!!");
		}
		isActive = false;
	}
}