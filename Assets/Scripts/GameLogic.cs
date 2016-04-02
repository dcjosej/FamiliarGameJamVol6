using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
	public int charactersInScene = 0;
	public int charactersToLoose = 5;

	public static GameLogic instance;

	public Transform[] spawnPoints;
	public GameObject[] standardPeopleInScene;

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
		if(charactersInScene >= charactersToLoose)
		{
			PlayerPrefs.SetInt("Winner", 0);
			//SceneManager.LoadScene(2);
		}else if(charactersInScene <= 0)
		{
			PlayerPrefs.SetInt("Winner", 1);
			//SceneManager.LoadScene(2);
		}
	}

	public Transform GetRandomRespawnZone(Transform originRespawnZone)
	{
		Transform res = null;

		while(res == null || originRespawnZone == res)
		{
			res = spawnPoints[Random.Range(0, spawnPoints.Length)];
		}

		return res;
	}
}