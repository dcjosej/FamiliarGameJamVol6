using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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



	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}

		
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
}