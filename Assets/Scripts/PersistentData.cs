using UnityEngine;
using System.Collections;

public class PersistentData : MonoBehaviour
{
	private static PersistentData _instance;
	public static PersistentData instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<PersistentData>();
			}
			return _instance;
		}
	}


	private string candidateSectors = "ACDEFGHIJKLMNPRSTUVWXYZ";
	public int currentSector { get; set; }
	public string currentLetter { get; set; }

	void Awake()
	{
		InitRandomData();
	}

	public void InitRandomData()
	{
		int indexRandomCharacter = Random.Range(0, candidateSectors.Length);

		currentLetter = candidateSectors[indexRandomCharacter].ToString();
		currentSector = Random.Range(1, 100);
	}
}