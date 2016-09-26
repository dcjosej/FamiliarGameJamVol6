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

	public float fadeTime = 2f;
	public float alphaDst = 0.6f;

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
			guiScoreTimer.text = Utils.SecondsTozzHHmmss(timerCount);
        }
	}

	private void OnGameOver()
	{
		SaveScore();
		StartCoroutine(FadeAnimation());
	}

	private IEnumerator FadeAnimation()
	{
		float t = 0;
		float time = 0f;
		Color srcColor = guiScoreTimer.color;
		Color dstColor = srcColor;
		dstColor.a = alphaDst;
        while (t < 1f)
		{
			t = time / fadeTime;
			time += Time.deltaTime;
			guiScoreTimer.color = Color.Lerp(srcColor, dstColor, t);

			if(t >= 1f)
			{
				t = 0;
				time = 0;
				Color aux = srcColor;
				srcColor = dstColor;
				dstColor = aux;
			}

			yield return null;

		}
	}

	public void SaveScore()
	{
		if (SocialManager.instance != null)
		{
			if (timerCount > PersistentData.instance.bestScore)
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