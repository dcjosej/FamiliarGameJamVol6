using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
	public int charactersInScene = 1;
	public int charactersToLoose = 5;

	public static GameLogic instance;

	public SpawnZoneController[] spawnZones;
	public GameObject[] standardPeopleInScene;

	public bool isGameOver = false;

	public Camera mainCameraRT;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	void Start()
	{
		standardPeopleInScene = GameObject.FindGameObjectsWithTag("StandardPerson");
		Debug.Log("dawdwdaw");
	}

	public PersonController GetRandomPerson()
	{
		PersonController res = null;

		res = standardPeopleInScene[Random.Range(0, standardPeopleInScene.Length)].GetComponent<PersonController>();

		return res;
	}

	void Update()
	{

		//RayRenderTexture();

		float threatnessLevel = HUDController.instance.sectorSliderController.GetValueThreatness();

		/*
		if(charactersInScene >= charactersToLoose)
		{
			PlayerPrefs.SetInt("Winner", 0);
			//SceneManager.LoadScene(2);
		}else if(charactersInScene <= 0)
		{
			PlayerPrefs.SetInt("Winner", 1);
			//SceneManager.LoadScene(2);
		}
		*/

		if(threatnessLevel <= 0f)
		{
			ActiveGameOver();
		}

		if (isGameOver)
		{
			CheckKeyBoardGameOver();
		}

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