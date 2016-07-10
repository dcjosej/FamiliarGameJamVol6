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
	public int charactersInScene { get; set; }

	[Header("Game Variables")]
	public AnimationCurve timeToConvert;
	public int maxNumCharactersToReachMinimunTime = 15;
	public int timeBetweenInfectionLevels = 5;
	public int globalInfectionLevel { get; set; }

	private float nextTimeToConvert;
	private int currentLevel = 0;

	private const int THREASHOLD_LEVEL0 = 1;
	private const int THREASHOLD_LEVEL1 = 5;
	private const int THREASHOLD_LEVEL2 = 10;


	public static GameLogic instance;

	public SpawnZoneController[] spawnZones;
	public GameObject[] standardPeopleInScene;

	public bool isGameOver = false;



	public Camera mainCameraRT;


	public ScreenFadeInOut fader;



	public SecurityCameraButton camera1;
	public SecurityCameraButton camera2;
	public SecurityCameraButton camera3;
	public SecurityCameraButton camera4;


	[Header("Cursores")]
	public Image imageCursor;
	public Texture2D outsideScreenCursor;
	public Sprite normalCursor;
	public Sprite overCursor;
	public Sprite failCursor;
	public Sprite correctCursor;

	private CursorMode cursorMode = CursorMode.Auto;
	private Vector2 hotSpot = Vector2.one * 32f;
	public CursorType cursorSelected;



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

	void Start()
	{
		globalInfectionLevel = 0;

		fader.FadeToClear();


		charactersInScene = 0;
		nextTimeToConvert = GetTimeCurveValue();
		standardPeopleInScene = GameObject.FindGameObjectsWithTag("StandardPerson");
		StartCoroutine(ConvertPeople());
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

		if(threatnessLevel <= 0f)
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
		float res = timeToConvert.Evaluate(xValueCurve);
		return res;
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
		if (Input.GetKeyDown(KeyCode.Y))
		{
			SceneManager.LoadScene(1);
		}else if (Input.GetKeyDown(KeyCode.N))
		{
			SceneManager.LoadScene(0);
		}
	}

	private void ActiveGameOver()
	{
		if (!isGameOver)
		{
			StopAllCoroutines();
			HUDController.instance.ActiveGameOver();
			AudioManager.instance.PlayNoise();
			isGameOver = true;
		}
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
}