using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum CursorType
{
	OUTSIDE_SCREEN,
	NORMAL,
	HOVER,
	CORRECT,
	FAIL
}


public class GameLogic : MonoBehaviour
{





	//Events
	public delegate void OnGameOverCallback();
	public static event OnGameOverCallback OnGameOver;



	public static bool CURSOR_CHANGING = false;



	public int charactersInScene { get; set; }


	[Header("DEBUG (QUITAR ESTE HEDAER EN LA RELEASE)")]
	public int globalInfectionLevel;


	#region GAMEPLAY VARIABLES
	[Header("Game Variables")]
	//public AnimationCurve timeToConvert;


	/// <summary>
	/// Minima velocidad de conversion (Cada X segundos transformamos a una persona).
	/// </summary>
	[Tooltip("Minima velocidad de conversion (Cada X segundos transformamos a una persona)")]
	public float minTimeToNextCharacterConversion = 15;

	/// <summary>
	/// Maxima velocidad de conversion (Cada X segundos transformamos a una persona).
	/// </summary>
	[Tooltip("Maxima velocidad de conversion (Cada X segundos transformamos a una persona)")]
	public float maxTimeToNextCharacterConversion = 4;

	[Tooltip("Cuantos infectados le corresponden a una unidad de peligro ('Un trozo del slider')")]
	public int numberOfCharacterToDangerousUnit = 5;

	public int maxNumCharactersToReachMinimunTime = 15;
	[Tooltip("Tiempo para la conversion de la siguiente zona del infectado (Cabeza, cuerpo, zapatos...)")]
	public int timeToNextZone = 5;


	public PersonZonesProbability zonesProbabilities;


	

	//public float minReward;
	//public float maxReward;
	//public float maxLoseVelocity;
	//public int maxCharacterToMaxLoseVelocity = 10;
	public AnimationCurve infectionVelocityCurve;

	[Range(0, 10)]
	public int dangerousThreashold;
	private bool dangerousAdvertisementShowed = false;

	#endregion

	[Header("Materiales comunes")]
	public Material humanConversionMaterial;
	public Material humanAtlas;

	[Header("Others")]
	[Tooltip("Segundos que permanece el cursor cambiado de Color cuando hemos pulsado sobre una persona")]
	public float secondsToRestablishCursor = 0.5f;


	[Header("GUI References")]
	public SectorSliderController sectorSliderController;

	[Header("")]

	private float nextTimeToConvert;
	private int currentLevel = 0;

	private const int THREASHOLD_LEVEL0 = 1;
	private const int THREASHOLD_LEVEL1 = 5;
	private const int THREASHOLD_LEVEL2 = 10;


	public static GameLogic instance;

	public SpawnZoneController[] spawnZones;
	public GameObject[] standardPeopleInScene;



	public bool isGameOver = false;
	//public GameState gameState;
	public GameObject people;


	public Camera mainCameraRT;


	//public ScreenFadeInOut fader;



	public SecurityCameraButton camera1;
	public SecurityCameraButton camera2;
	public SecurityCameraButton camera3;
	public SecurityCameraButton camera4;


	[Header("Cursores")]
	public Image imageCursor;
	public Texture2D outsideScreenCursor;
	public Sprite normalCursor;
	public Sprite failCursor;
	public Sprite correctCursor;
	public Sprite hoverCursor;

	private CursorMode cursorMode = CursorMode.Auto;
	private Vector2 hotSpot = Vector2.one * 32f;
	public CursorType cursorSelected;

	[Header("Tiempo de servicio")]
	public ScoreTimeController scoreTimeController;

	public void UpdateCursor(CursorType cursorSelected)
	{
		
		switch (cursorSelected)
		{
			case CursorType.OUTSIDE_SCREEN:
				Cursor.SetCursor(outsideScreenCursor, hotSpot, cursorMode);
				imageCursor.gameObject.SetActive(false);
				Cursor.visible = true;
				break;
			case CursorType.NORMAL:
				//Cursor.SetCursor(normalCursor, hotSpot, cursorMode);
				imageCursor.gameObject.SetActive(true);
				Cursor.visible = false;
				imageCursor.sprite = normalCursor;
				break;
			case CursorType.CORRECT:
				//Cursor.SetCursor(correctCursor, hotSpot, cursorMode);
				imageCursor.gameObject.SetActive(true);
				Cursor.visible = false;
				imageCursor.sprite = correctCursor;
				break;
			case CursorType.FAIL:
				//Cursor.SetCursor(wrongCursor, hotSpot, cursorMode);
				imageCursor.gameObject.SetActive(true);
				Cursor.visible = false;
				imageCursor.sprite = failCursor;
				break;
			case CursorType.HOVER:
				imageCursor.gameObject.SetActive(true);
				Cursor.visible = false;
				imageCursor.sprite = hoverCursor;
				break;
		}
		this.cursorSelected = cursorSelected;
		
	}


	void Awake()
	{


		if(instance == null)
		{
			instance = this;
		}

		//Cursor.SetCursor(outsideScreenCursor, hotSpot, cursorMode);
		UpdateCursor(CursorType.OUTSIDE_SCREEN);
	}



	//public void SetState(GameState gameState)
	//{
	//	switch (gameState)
	//	{
	//		case GameState.IN_GAME:
	//			StartGame();
	//			break;
	//	}
	//}

	public void StartGame()
	{
		AutoText.OnInputReceived += OnInputReceived;


		globalInfectionLevel = 0;

		//fader.FadeToClear();


		charactersInScene = 0;
		nextTimeToConvert = GetTimeCurveValue();
		standardPeopleInScene = GameObject.FindGameObjectsWithTag("StandardPerson");
		StartCoroutine(ConvertPeople());
	}


	void Start()
	{
		StartGame();
	}

	#region GAMEPLAY
	public float GetSliderValue()
	{
		//int res = 0;
		//float t = Mathf.InverseLerp(0, maxNumCharactersToReachMinimunTime, charactersInScene);
		//float curveValue = infectionVelocityCurve.Evaluate(t);
		//float res = Mathf.Lerp(0, 4, curveValue);

		int res = charactersInScene / numberOfCharacterToDangerousUnit;
		return res;
	}

	public float GetProbabilityByZone(Zone zone)
	{
		float res = zonesProbabilities.GetZoneProbabilityByZone(zone).probability;
		return res;
	}
	
	#endregion

	public void PersonClicked(bool wasInfected)
	{
		StartCoroutine(ChangeCursor(wasInfected));
	}

	private IEnumerator ChangeCursor(bool wasInfected)
	{
		float time = 0;

		UpdateCursor(wasInfected ? CursorType.CORRECT : CursorType.FAIL);

		CURSOR_CHANGING = true;
        while (time <= secondsToRestablishCursor)
		{
			time += Time.deltaTime;
			yield return null;
		}
		CURSOR_CHANGING = false;
    }

	public PersonController GetRandomPerson()
	{
		PersonController res = null;

		res = standardPeopleInScene[Random.Range(0, standardPeopleInScene.Length)].GetComponent<PersonController>();

		return res;
	}

	void Update()
	{
		float threatnessLevel = HUDController.instance.sectorSliderController.GetValueThreatness();


		if(threatnessLevel >= dangerousThreashold && !dangerousAdvertisementShowed)
		{
			HUDController.instance.ShowDangerAdvertisement();
			dangerousAdvertisementShowed = true;
		}else
		{
			dangerousAdvertisementShowed = false;
		}




		if (threatnessLevel >= HUDController.instance.sectorSliderController.GetMaxValue() && !isGameOver)
		{
			ActiveGameOver();
		}

		if (isGameOver)
		{
			CheckKeyBoardGameOver();
		}

		CheckKeyBoard();
	}



	private void CheckKeyBoard()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			camera1.UpdateMainScreen();
		}else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			camera2.UpdateMainScreen();
		}else if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			camera3.UpdateMainScreen();
		}else if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			camera4.UpdateMainScreen();
		}

	}

	private IEnumerator ConvertPeople()
	{
		while (true)
		{
			Convert();
			yield return new WaitForSeconds(nextTimeToConvert);
		}
	}

	private float GetTimeCurveValue()
	{
		float xValueCurve = Mathf.InverseLerp(0, maxNumCharactersToReachMinimunTime, charactersInScene);
		float curveValue = infectionVelocityCurve.Evaluate(xValueCurve);
		float res = Mathf.Lerp(maxTimeToNextCharacterConversion, minTimeToNextCharacterConversion, curveValue);
		return res;
		//return 0.0f;
	}

	private void Convert()
	{
		/*CONVERTIR A ALGUIEN*/
		PersonController personController = GetRandomPerson();
		personController.Convert();

		nextTimeToConvert = GetTimeCurveValue();
	}


	private void CheckKeyBoardGameOver()
	{
		/*
		if (Input.GetKeyDown(KeyCode.Y))
		{
			SceneManager.LoadScene(1);
		}else if (Input.GetKeyDown(KeyCode.N))
		{
			SceneManager.LoadScene(0);
		}
		*/
	}

	private void ActiveGameOver()
	{
		if(OnGameOver != null)
		{
			OnGameOver();
		}
		StopAllCoroutines();

		people.SetActive(false);
		HUDController.instance.ActiveGameOver();
		AudioManager.instance.PlayNoise();


		
		isGameOver = true;
	}

	public SpawnZoneController GetRandomRespawnZone(SpawnZoneController spawnZone)
	{
		SpawnZoneController res = null;

		while(res == null || spawnZone.name == res.name)
		{
			res = spawnZones[Random.Range(0, spawnZones.Length)];
		}

		return res;
	}

	#region SUBSCRIPTION EVENTS
	private void OnInputReceived(string input)
	{
		if(input == "y")
		{
			HUDController.instance.ConsoleReponse(AutoText.GOOD_LUCK, Utils.GreenColor, false);
			//PersistentData.instance.InitRandomData();
			//ScreenFadeInOut.instance.FadeToBlackLoadScene(2);
			FadersController.instance.FadeToBlack(2);

			AutoText.OnInputReceived -= OnInputReceived;

		}
		else if(input == "n")
		{
			HUDController.instance.ConsoleReponse(AutoText.NOT_PLAY_AGAIN, Utils.RedColor, false);
			//ScreenFadeInOut.instance.FadeToBlackLoadScene(0);
			FadersController.instance.FadeToBlack(0);

			AutoText.OnInputReceived -= OnInputReceived;
		}
		else
		{
			HUDController.instance.ConsoleReponse(AutoText.WRONG_INPUT, Utils.RedColor, true);
		}
	}
	#endregion
}