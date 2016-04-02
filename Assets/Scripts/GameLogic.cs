﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
	public int charactersInScene = 0;
	public int charactersToLoose = 5;

	public static GameLogic instance;

	public SpawnZoneController[] spawnZones;
	public GameObject[] standardPeopleInScene;

	public bool isGameOver = false;

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

	}

	private void ActiveGameOver()
	{
		if (!isGameOver)
		{
			StopAllCoroutines();
			HUDController.instance.ActiveGameOver();
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