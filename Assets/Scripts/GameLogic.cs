﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


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

	public AnimationCurve timeToConvert;
	public int maxNumCharactersToReachMinimunTime = 15;
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
	public Texture2D outsideScreenCursor;
	public Texture2D normalCursor;
	public Texture2D overCursor;
	public Texture2D wrongCursor;
	public Texture2D correctCursor;

	private CursorMode cursorMode = CursorMode.Auto;
	private Vector2 hotSpot = Vector2.one * 32f;
	public CursorType cursorSelected;



	public void UpdateCursor(CursorType cursorSelected)
	{
		switch (cursorSelected)
		{
			case CursorType.OUTSIDE_SCREEN:
				Cursor.SetCursor(outsideScreenCursor, hotSpot, cursorMode);
				break;
			case CursorType.NORMAL:
				Cursor.SetCursor(normalCursor, hotSpot, cursorMode);
				break;
			case CursorType.CORRECT:
				Cursor.SetCursor(correctCursor, hotSpot, cursorMode);
				break;
			case CursorType.FAIL:
				Cursor.SetCursor(wrongCursor, hotSpot, cursorMode);
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

		Cursor.SetCursor(outsideScreenCursor, hotSpot, cursorMode);

	}

	void Start()
	{
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

		CheckRaycast();
	}

	private void CheckRaycast()
	{
		//if (cursorSelected != CursorType.OUTSIDE_SCREEN)
		//{
			/*
			PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
			pointerEventData.position = Input.mousePosition;

			List<RaycastResult> raycastResults = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointerEventData, raycastResults);

			Debug.Log("HIT: " + raycastResults.Count); 

			if (raycastResults.Count > 0)
			{
				if (raycastResults[0].gameObject.CompareTag("Terminal"))
				{
					UpdateCursor(CursorType.OUTSIDE_SCREEN);
				}

				if (raycastResults[0].gameObject.CompareTag("MainScreenImage"))
				{
					UpdateCursor(CursorType.NORMAL);
				}
			}
			*/
		//}
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
		if(charactersInScene >= THREASHOLD_LEVEL0 && charactersInScene < THREASHOLD_LEVEL1)
		{
			currentLevel = 1;
		}

		if(charactersInScene >= THREASHOLD_LEVEL1 && charactersInScene < THREASHOLD_LEVEL2)
		{
			currentLevel = 2;
		}else if(charactersInScene >= THREASHOLD_LEVEL2)
		{
			currentLevel = 3;
		}

		/*CONVERTIR A ALGUIEN*/
		PersonController personController = GetRandomPerson();
		personController.Convert(currentLevel);

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

	/// <summary>
	/// Cambiamos el cursor cuando hacemos click sobre un personaje.
	/// </summary>
	/// <param name="cursorType"></param>
	public void ChangeCursorInGame(CursorType cursorType)
	{
		StopAllCoroutines();
		StartCoroutine(ChangeCursor(cursorType));
	}

	private IEnumerator ChangeCursor(CursorType cursorType)
	{
		UpdateCursor(cursorType);
		yield return new WaitForSeconds(0.2f);
		UpdateCursor(CursorType.NORMAL);
	}
}