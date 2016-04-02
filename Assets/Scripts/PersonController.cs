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
	public Material matHuman_0;

	public bool converted = false;


	void Update()
	{
		
	}

	void OnEnable()
	{
		skmr.material = matHuman_0;
	}

	void Awake()
	{
		differentPersonController = GetComponent<DifferentPersonController>();
		skmr = GetComponentInChildren<SkinnedMeshRenderer>();
	}

	public void Convert(int levelFrom)
	{

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
		skmr.material = GetMaterialFromLevel(levelTo);
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
}