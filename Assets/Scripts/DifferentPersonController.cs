using System.Collections;
using UnityEngine;

public class DifferentPersonController : MonoBehaviour
{
	private PersonController personController;

	private SkinnedMeshRenderer skmr;

	public float timeToConvert = 2f;

	void Awake()
	{
		skmr = GetComponentInChildren<SkinnedMeshRenderer>();
	}

	void Start()
	{



		personController = GetComponent<PersonController>();

		StartCoroutine("ConvertPerson");


		if (GameLogic.instance == null)
		{
			Debug.Log("Esto es una puta mierda");
		}


		GameLogic.instance.charactersInScene++;
	}

	void OnEnable()
	{
		/*
		StartCoroutine("ConvertPerson");

		
		if(GameLogic.instance == null)
		{
			Debug.Log("Esto es una puta mierda");
		}


		GameLogic.instance.charactersInScene++;
		*/
	}

	void OnDisable()
	{
		GameLogic.instance.charactersInScene--;
	}

	void OnMouseDown()
	{
		StopCoroutine("ConvertPerson");
		personController.enabled = true;


		this.enabled = false;
	}

	private IEnumerator ConvertPerson()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeToConvert);
			GameLogic.instance.GetRandomPerson().Convert();
		}
	}
}