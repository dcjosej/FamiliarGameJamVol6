using UnityEngine;
using System.Collections;

public class PersonController : MonoBehaviour
{
	private DifferentPersonController differentPersonController;

	private SkinnedMeshRenderer skmr;

	[Header("Textures")]
	public Material matHuman_3_1;
	public Material matHuman_0;

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

	public void Convert()
	{
		skmr.material = matHuman_3_1;

		differentPersonController.enabled = true;
		this.enabled = false;
	}
}