using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
	public AutoText consoleAutoText;

	public string threatDetected = "Threat!";
	public string cleaningCompleted = "Cleaning!";

	//public string gameOverText = "Este es el texto de game over, que ocupa varias lineas y es mas largo que los demas!";
	//public string gameOverText2 = " ¿Intentarlo otra vez (Y, N)?";
	public GameObject gameOverEmergencia;

	public SectorSliderController sectorSliderController;

	public GameObject gameOverNoise;

	public static HUDController instance;

	[Header("GUI References")]
	public Text textCurrentDate;
	public Text textCurrentSector;
	public Text textCurrentTime;

	[Header("Danger")]
	public Text textDanger;
	public AnimationCurve textDangerAlphaAnimationCurve;
	public float timeAnimationDanger = 4f;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	void OnEnable()
	{
		UpdateGUI();

		StartCoroutine(AnimateDangerText());
	}

	void Update()
	{
		textCurrentTime.text = DateTime.Now.ToString("HH:mm");
	}

	private void UpdateGUI()
	{
		DateTime currentDate = DateTime.Now;
		textCurrentDate.text = currentDate.ToString("dd/MM") + "/3048";
		textCurrentSector.text = PersistentData.instance.currentSector.ToString("00") + PersistentData.instance.currentLetter;
    }

	public void ActiveGameOver()
	{
		gameOverNoise.SetActive(true);
		gameOverEmergencia.SetActive(true);


		consoleAutoText.Clean();
		consoleAutoText.allowKeyboardTyping = true;
		consoleAutoText.maxNumLines = 10;
		consoleAutoText.TypeText(AutoText.PLAY_AGAIN_TEXT, Utils.OrangeColor);
		//TypeMessageGameOver();
	}

	public void TypeMessageGameOver()
	{
		//consoleAutoText.TypeTextGameOver(gameOverText, gameOverText2);
	}

	public void TypeThreatDetected()
	{
		consoleAutoText.TypeText(threatDetected + ": " + GameLogic.instance.charactersInScene, Utils.RedColor);
	}
	
	public void TypeCleaningCompleted()
	{
		consoleAutoText.TypeText(cleaningCompleted, Utils.GreenColor);
	}

	public void ConsoleReponse(string msg, string hexColor, bool repeatMessage)
	{
		consoleAutoText.ConsoleResponse(msg, hexColor, repeatMessage);
	}

	public void CleanConsole()
	{
		consoleAutoText.Clean();
	}

	private IEnumerator AnimateDangerText()
	{
		float time = 0f;
		float t = 0;
		Color colorFrom = textDanger.color;
        bool animating = true;
		textDanger.gameObject.SetActive(true);
		while (animating)
		{
			float curveValue = Mathf.Lerp(0f, 1f, t);
			colorFrom.a = textDangerAlphaAnimationCurve.Evaluate(curveValue);
			textDanger.color = colorFrom;
			time += Time.deltaTime;
			t = time / timeAnimationDanger;
			yield return null;
		}
		textDanger.gameObject.SetActive(false);
	}
}