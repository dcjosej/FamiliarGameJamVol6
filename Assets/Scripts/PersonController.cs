using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PersonController : MonoBehaviour
{
	public ElementsZone[] elements;


	public bool converted = false;


	private int infectedLevel = 0;

	private List<GameObject> activeElements = new List<GameObject>();


	void OnEnable()
	{
		MindRestored();
	}

	private void MindRestored()
	{
		if (GameLogic.instance != null && !GameLogic.instance.isGameOver)
		{
			DisableActiveElements();
			activeElements.Clear();
			ActiveStandardElements();

			converted = false;
			infectedLevel = 0;
			GameLogic.instance.charactersInScene--;
			StopAllCoroutines();
		}
	}

	/// <summary>
	/// Activamos los elementos de la persona sin infección
	/// </summary>
	private void ActiveStandardElements()
	{
		foreach(ElementsZone ez in elements)
		{
			ez.elements[0].gameObject.SetActive(true);
			activeElements.Add(ez.elements[0]);
		}
	}

	/// <summary>
	/// Desactivamos todas las variaciones activas
	/// </summary>
	private void DisableActiveElements()
	{
		foreach(GameObject go in activeElements)
		{
			go.gameObject.SetActive(false);
		}
	}

	void Awake()
	{
		//skmr = GetComponentInChildren<SkinnedMeshRenderer>();
	}

	public void Click()
	{
		OnMouseDown();
		//AudioManager.instance.PlayConversionFail();
	}

	public void Convert()
	{

		if (!GameLogic.instance.isGameOver)
		{
			
			GameLogic.instance.charactersInScene++;

			HUDController.instance.TypeThreatDetected();

			converted = true;

			ChangeAppearence();

			StartCoroutine(CheckTimeToIncreaseInfectionLevels());
		}
	}

	private IEnumerator CheckTimeToIncreaseInfectionLevels()
	{
		//Vamos cambiando zonas cada cierto tiempo hasta tener la persona completamente cambiada
		while (infectedLevel < elements.Length - 1)
		{
			yield return new WaitForSeconds(GameLogic.instance.timeBetweenInfectionLevels);
			ChangeAppearence();
		}
	}

	/// <summary>
	/// Seleccionamos alguna de las zonas no aplicadas y activamos una variación
	/// </summary>
	private void ChangeAppearence()
	{
		ElementsZone notAppliedZone = Array.Find(elements, x => !x.applied);
		GameObject selectedElement = notAppliedZone.ActiveRandomElement();
		activeElements.Add(selectedElement);
		infectedLevel++;
	}

	/// <summary>
	/// 0 = Material
	/// 1 = Prop
	/// </summary>
	///// <returns></returns>
	//private int MaterialOrProp()
	//{
	//	int res = Random.Range(0, 2);

	//	return res;
	//}

	//private void ChangeMaterial(int levelTo)
	//{
	//	if (!GameLogic.instance.isGameOver)
	//	{
	//		skmr.material = GetMaterialFromLevel(levelTo);
	//	}
	//}

	//private void ChangeLevel(int levelTo)
	//{
	//	if(levelTo > 0)
	//	{
	//		GetTransformFromLevel(levelTo).gameObject.SetActive(true);
	//	}
	//}


	//private Material GetMaterialFromLevel(int levelTo)
	//{
	//	Material res = null;

	//	Material[] arraySelected = null;

	//	switch (levelTo)
	//	{
	//		case 1:
	//			//arraySelected = level1Materials;
	//			break;
	//		case 2:
	//			//arraySelected = level2Materials;
	//			break;
	//		case 3:
	//			//arraySelected = level3Materials;
	//			break;
	//	}

	//	if(levelTo > 0)
	//	{
	//		res = arraySelected[Random.Range(0, arraySelected.Length)];
	//	}
	//	else
	//	{
	//		//res = matHuman_0;
	//	}

	//	return res;
	//}

	//private Transform GetTransformFromLevel(int levelTo)
	//{
	//	Transform res = null;

	//	Transform[] arraySelected = null;

	//	switch (levelTo)
	//	{
	//		case 1:
	//			//arraySelected = level1Transforms;
	//			break;
	//		case 2:
	//			//arraySelected = level2Transforms;
	//			break;
	//		case 3:
	//			//arraySelected = level3Transforms;
	//			break;
	//	}

	//	res = arraySelected[Random.Range(0, arraySelected.Length)];

	//	return res;
	//}

	void OnMouseDown()
	{
		if (converted)
		{
			StopCoroutine("ConvertPerson");
			//personController.enabled = true;

			HUDController.instance.TypeCleaningCompleted();
			AudioManager.instance.PlayConversionAccepted();

			HUDController.instance.IncreaseBar();

			//this.enabled = false;
			MindRestored();
        }
		else
		{
			AudioManager.instance.PlayConversionFail();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "RespawnZone")
		{

			//SpawnZoneController selectedRespawnZone = GameLogic.instance.GetRandomRespawnZone(other.GetComponent<SpawnZoneController>());

			//Transform selectedSpawnPoint = selectedRespawnZone.GetRandomSpawnPoint();

			SpawnPointController spawnPointController = other.GetComponent<SpawnPointController>().nextSpawnPoint;


			Quaternion rotation = Quaternion.LookRotation(spawnPointController.transform.right);

			transform.rotation = rotation;

			Vector3 newPosition = spawnPointController.transform.position;
			newPosition.y = transform.position.y;
			transform.position = newPosition;
		}
	}

	[System.Serializable]
	public class ElementsZone
	{
		//[HideInInspector]
		public bool applied;
		public GameObject[] elements;

		public GameObject ActiveRandomElement()
		{
			elements[0].SetActive(false);

			int randomIndex = UnityEngine.Random.Range(1, elements.Length);
			elements[randomIndex].SetActive(true);

			applied = true;

			return elements[randomIndex];
		}
	}
}