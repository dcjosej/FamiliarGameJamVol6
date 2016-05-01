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

	public void Click()
	{
		AudioManager.instance.PlayConversionFail();
	}

	public void Convert(int level)
	{

		if (!GameLogic.instance.isGameOver)
		{

			HUDController.instance.TypeThreatDetected();

			converted = true;

			switch (MaterialOrProp())
			{
				case 0:
					ChangeMaterial(level);
					break;
				case 1:
					ChangeLevel(level);
					break;
			}

			differentPersonController.enabled = true;
			differentPersonController.infectedLevel = level;
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
		if(levelTo > 0)
		{
			GetTransformFromLevel(levelTo).gameObject.SetActive(true);
		}
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

		if(levelTo > 0)
		{
			res = arraySelected[Random.Range(0, arraySelected.Length)];
		}
		else
		{
			res = matHuman_0;
		}

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
}