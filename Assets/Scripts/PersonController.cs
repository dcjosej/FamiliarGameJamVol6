using UnityEngine;
using System.Collections;

public class PersonController : MonoBehaviour
{
	private DifferentPersonController differentPersonController;

	private SkinnedMeshRenderer skmr;

	[Header("Textures")]
	public Material[] level1Materials;
	public Material[] level2Materials;
	public Material[] level3Materials;

	[Header("Props")]
	public Transform[] level1Transforms;
	public Transform[] level2Transforms;
	public Transform[] level3Transforms;

	private Transform[][] levelsTransform;
	public Material matHuman_0;

	public bool converted = false;


	void Update()
	{
		
	}

	void OnEnable()
	{
		if (GameLogic.instance != null && !GameLogic.instance.isGameOver)
		{
			skmr.material = matHuman_0;
			DisableLevelsProps();
		}
	}

	private void DisableLevelsProps()
	{
		foreach(Transform [] transformArray in levelsTransform)
		{
			foreach (Transform prop in transformArray)
			{
				prop.gameObject.SetActive(false);
			}
		}
	}

	void Awake()
	{
		//if (GameLogic.instance != null && !GameLogic.instance.isGameOver)
		//{
			differentPersonController = GetComponent<DifferentPersonController>();
			skmr = GetComponentInChildren<SkinnedMeshRenderer>();
			levelsTransform = new Transform[][] { level1Transforms, level2Transforms, level3Transforms };
		//}
	}

	public void Convert(int levelFrom)
	{

		if (!GameLogic.instance.isGameOver)
		{

			HUDController.instance.TypeThreatDetected();

			Debug.Log("Vengo del nivel " + levelFrom);

			converted = true;

			switch (MaterialOrProp())
			{
				case 0:
					ChangeMaterial(levelFrom + 1);
					break;
				case 1:
					ChangeLevel(levelFrom + 1);
					break;
			}

			differentPersonController.enabled = true;
			differentPersonController.infectedLevel++;
			this.enabled = false;
		}
	}

	/// <summary>
	/// 0 = Material
	/// 1 = Prop
	/// </summary>
	/// <returns></returns>
	private int MaterialOrProp()
	{
		int res = Random.Range(0, 2);
		return res;
	}

	private void ChangeMaterial(int levelTo)
	{
		if (!GameLogic.instance.isGameOver)
		{
			skmr.material = GetMaterialFromLevel(levelTo);
		}
	}

	private void ChangeLevel(int levelTo)
	{
		GetTransformFromLevel(levelTo).gameObject.SetActive(true);
	}


	private Material GetMaterialFromLevel(int levelTo)
	{
		Material res = null;

		Material[] arraySelected = null;

		switch (levelTo)
		{
			case 1:
				arraySelected = level1Materials;
				break;
			case 2:
				arraySelected = level2Materials;
				break;
			case 3:
				arraySelected = level3Materials;
				break;
		}

		res = arraySelected[Random.Range(0, arraySelected.Length)];

		return res;
	}

	private Transform GetTransformFromLevel(int levelTo)
	{
		Transform res = null;

		Transform[] arraySelected = null;

		switch (levelTo)
		{
			case 1:
				arraySelected = level1Transforms;
				break;
			case 2:
				arraySelected = level2Transforms;
				break;
			case 3:
				arraySelected = level3Transforms;
				break;
		}

		res = arraySelected[Random.Range(0, arraySelected.Length)];

		return res;
	}

	void OnMouseDown()
	{
		AudioManager.instance.PlayConversionFail();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "RespawnZone")
		{

			SpawnZoneController selectedRespawnZone = GameLogic.instance.GetRandomRespawnZone(other.GetComponent<SpawnZoneController>());

			Transform selectedSpawnPoint = selectedRespawnZone.GetRandomSpawnPoint();

			Quaternion rotation = Quaternion.LookRotation(selectedSpawnPoint.right);

			transform.rotation = rotation;


			Vector3 newPosition = selectedSpawnPoint.position;
			newPosition.y = transform.position.y;
			transform.position = newPosition;
		}
	}
}