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
			GameLogic.instance.globalInfectionLevel -= infectedLevel;

			infectedLevel = 0;
			GameLogic.instance.charactersInScene--;
			StopAllCoroutines();
		}
	}

	private float CalculateNormalizedInfectionLevel()
	{
		float res = Mathf.InverseLerp(1, elements.Length, infectedLevel);
		return res;
	}

	/// <summary>
	/// Activamos los elementos de la persona sin infección
	/// </summary>
	private void ActiveStandardElements()
	{
		foreach(ElementsZone ez in elements)
		{
			if(ez.zone == Zone.GAFAS || ez.zone == Zone.HEAD_COMPLEMENTS)
			{
				continue;
			}
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
			yield return new WaitForSeconds(GameLogic.instance.timeToNextZone);
			ChangeAppearence();
		}
	}

	/// <summary>
	/// Seleccionamos alguna de las zonas no aplicadas y activamos una variación
	/// </summary>
	private void ChangeAppearence()
	{
		ElementsZone[] notAppliedZones = Array.FindAll(elements, x => !x.applied);

		ElementsZone selectedZone = SelectZoneWeightedRandom(notAppliedZones);

		if(selectedZone == null)
		{
			return;
		}

		GameObject selectedElement = selectedZone.ActiveRandomElement();
		activeElements.Add(selectedElement);
		infectedLevel++;
	}

	private ElementsZone SelectZoneWeightedRandom(ElementsZone[] notAppliedZones)
	{
		ElementsZone res = null;

		Array.Sort(notAppliedZones);

		float weightedIndex = UnityEngine.Random.Range(0f, 1f);

		foreach(ElementsZone ez in notAppliedZones)
		{

			float weightElement = GameLogic.instance.GetProbabilityByZone(ez.zone) / 100f;

			if (weightedIndex <= weightElement)
			{
				res = ez;
				break;
			}

			weightedIndex -= weightElement;

		}

		if(res == null)
		{
			Debug.Log("");
		}

		return res;
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

		GameLogic.instance.PersonClicked(converted);

		if (converted)
		{
			StopCoroutine("ConvertPerson");
			//personController.enabled = true;

			HUDController.instance.TypeCleaningCompleted();
			AudioManager.instance.PlayConversionAccepted();
			

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
			transform.position += transform.forward * 20;
		}
	}

	[System.Serializable]
	public class ElementsZone: IComparable<ElementsZone>
	{
		//[HideInInspector]
		public bool applied;
		public GameObject[] elements;
		public Zone zone;

		public GameObject ActiveRandomElement()
		{
			elements[0].SetActive(false);

			int randomIndex = UnityEngine.Random.Range(1, elements.Length);
			elements[randomIndex].SetActive(true);

			applied = true;

			GameLogic.instance.globalInfectionLevel++;

			return elements[randomIndex];
		}

		public int CompareTo(ElementsZone other)
		{
			int res = 0;
			if(GameLogic.instance.GetProbabilityByZone(zone) > GameLogic.instance.GetProbabilityByZone(other.zone))
			{
				res = -1;
			}
			else if(GameLogic.instance.GetProbabilityByZone(zone) < GameLogic.instance.GetProbabilityByZone(other.zone))
			{
				res = 1;
			}
			return res;
		}

		public override string ToString()
		{
			string res = zone + " " + GameLogic.instance.GetProbabilityByZone(zone);
			return res;
		}
	}
}