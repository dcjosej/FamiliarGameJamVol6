using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
	public int charactersInScene = 0;
	public int charactersToLoose = 5;

	public static GameLogic instance;

	public PersonController[] standardPeopleInScene;


	void OnEnable()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	void Start()
	{
		standardPeopleInScene = FindObjectsOfType<PersonController>();
	}

	public PersonController GetRandomPerson()
	{
		PersonController res = null;

		res = standardPeopleInScene[Random.Range(0, standardPeopleInScene.Length)];

		return res;
	}

	void Update()
	{
		if(charactersInScene >= charactersToLoose)
		{
			SceneManager.LoadScene(2);
		}
	}
}