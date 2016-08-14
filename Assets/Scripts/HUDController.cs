using UnityEngine;

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

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	public void ActiveGameOver()
	{
		gameOverNoise.SetActive(true);
		gameOverEmergencia.SetActive(true);

		consoleAutoText.Clean();


		consoleAutoText.TypeText("Texto de game over", Utils.OrangeColor);
		//TypeMessageGameOver();
	}

	public void TypeMessageGameOver()
	{
		//consoleAutoText.TypeTextGameOver(gameOverText, gameOverText2);
	}

	public void TypeThreatDetected()
	{
		consoleAutoText.TypeText(threatDetected + ": " + GameLogic.instance.charactersInScene, ColorUtility.ToHtmlStringRGB(Color.red));
	}
	
	public void TypeCleaningCompleted()
	{
		consoleAutoText.TypeText(cleaningCompleted, ColorUtility.ToHtmlStringRGB(Color.green));
	}
}