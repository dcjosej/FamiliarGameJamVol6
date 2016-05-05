using System.Collections;
using UnityEngine;

public class DifferentPersonController : MonoBehaviour
{
	private PersonController personController;

	private SkinnedMeshRenderer skmr;

	public float timeToConvert= 2f;
	public int infectedLevel = 1;
	public float timeBetweenInfections = 10f;

	

	void Awake()
	{
		skmr = GetComponentInChildren<SkinnedMeshRenderer>();
	}

	void Start()
	{
		personController = GetComponent<PersonController>();

		//HUDController.instance.TypeThreatDetected();


		Debug.Log("Empezando corrutina!!! " + gameObject.name);

		/*
		if(infectedLevel < 3)
		{
			//StartCoroutine("ConvertPerson");
		}
		*/


		/*
		if (GameLogic.instance == null)
		{
			Debug.Log("Esto es una puta mierda");
		}
		*/

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




	public void Click()
	{
		OnMouseDown();
	}

	void OnMouseDown()
	{
		if (enabled)
		{
			StopCoroutine("ConvertPerson");
			personController.enabled = true;

			HUDController.instance.TypeCleaningCompleted();
			AudioManager.instance.PlayConversionAccepted();

			HUDController.instance.IncreaseBar();

			this.enabled = false;
		}
	}

	/*
	private IEnumerator ConvertPerson()
	{
		while (true && infectedLevel < 3)
		{
			yield return new WaitForSeconds(timeToConvert);

			yield return new WaitForSeconds(Random.Range(2, timeBetweenInfections));

			PersonController selectedPerson = GameLogic.instance.GetRandomPerson();
			if (!selectedPerson.converted)
			{
				selectedPerson.Convert(infectedLevel);
			}
		}
	}
	*/

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "RespawnZone")
		{

			//SpawnZoneController selectedRespawnZone = GameLogic.instance.GetRandomRespawnZone(other.GetComponent<SpawnZoneController>());

			//Transform selectedSpawnPoint = selectedRespawnZone.GetRandomSpawnPoint();

			Transform selectedSpawnPoint = other.GetComponent<SpawnPointController>().nextSpawnPoint.transform;

			Quaternion rotation = Quaternion.LookRotation(selectedSpawnPoint.right);

			transform.rotation = rotation;


			Vector3 newPosition = selectedSpawnPoint.position + selectedSpawnPoint.right * 2f;
			newPosition.y = transform.position.y;

			transform.position = newPosition;
		}
	}
}