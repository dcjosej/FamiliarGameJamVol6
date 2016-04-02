using System.Collections;
using UnityEngine;

public class DifferentPersonController : MonoBehaviour
{
	private PersonController personController;

	private SkinnedMeshRenderer skmr;

	public float timeToConvert= 2f;
	public int infectedLevel = 1;

	

	void Awake()
	{
		skmr = GetComponentInChildren<SkinnedMeshRenderer>();
	}

	void Start()
	{
		personController = GetComponent<PersonController>();


		Debug.Log("Empezando corrutina!!! " + gameObject.name);

		if(infectedLevel < 3)
		{
			StartCoroutine("ConvertPerson");
		}


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
		while (true && infectedLevel < 3)
		{
			yield return new WaitForSeconds(timeToConvert);
			PersonController selectedPerson = GameLogic.instance.GetRandomPerson();
			if (!selectedPerson.converted)
			{
				selectedPerson.Convert(infectedLevel);
			}
		}
	}
}