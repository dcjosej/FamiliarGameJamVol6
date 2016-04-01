using UnityEngine;
using System.Collections;

public class PersonController : MonoBehaviour
{
	private Material material;

	private DifferentPersonController differentPersonController;

	private SkinnedMeshRenderer skmr;

	[Header("Textures")]
	public Texture texDifferent1;

	void Update()
	{
		
	}

	void OnEnable()
	{
		//material.SetColor("_Color", Constants.standardPersonColor);
	}

	void Awake()
	{
		//material = GetComponent<Renderer>().material;
		differentPersonController = GetComponent<DifferentPersonController>();
		skmr = GetComponentInChildren<SkinnedMeshRenderer>();
	}

	public void Convert()
	{
		//material.SetColor("_Color", Constants.diferentPersonColor);

		//material.mainTexture = texDifferent1;
		//skmr.material.mainTexture = texDifferent1;

		differentPersonController.enabled = true;
		this.enabled = false;
	}
}