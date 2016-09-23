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



	private const string CITIZEN_PREFIX = "Citizen";
	private const string RANDOM_NUMBER = "0123456789";
	private const string RANDOM_LETTER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";


	//public int currentSector { get; set; }
	public string currentLetter { get; set; }
	
	public int numberOfDigitsIdEmployee = 6;
	public bool splashShowed { get; set; }

	#region PLAYER PREFS
	private const string KEY_BEST_SERVICES = "KEY_BEST_SERVICE";
	private const string KEY_EMPLOYEE_ID = "KEY_EMPLOYEE_ID";

	public string employeeId {
		get
		{
			return PlayerPrefs.GetString(KEY_EMPLOYEE_ID, "");
		}
		set
		{
			PlayerPrefs.SetString(KEY_EMPLOYEE_ID, value);
		}
	}

	public float bestScore
	{
		get
		{
			return PlayerPrefs.GetFloat(KEY_BEST_SERVICES, -1);
		}

		set
		{
			PlayerPrefs.SetFloat(KEY_BEST_SERVICES, value);
		}
	}



	#endregion

	void Awake()
	{
		//InitRandomData();

		if(employeeId == "")
		{
			InitEmployeeId();
		}
	}

	//public void InitRandomData()
	//{
	//	int indexRandomCharacter = Random.Range(0, candidateSectors.Length);

	//	currentLetter = candidateSectors[indexRandomCharacter].ToString();
	//	//currentSector = Random.Range(1, 100);
	//}

	private void InitEmployeeId()
	{
		string selectedNumber = "";
		
		for(int i = 0; i < numberOfDigitsIdEmployee; i++)
		{
			int randomIndex = Random.Range(0, RANDOM_NUMBER.Length);
			selectedNumber += RANDOM_NUMBER[randomIndex];	
		}

		string selectedLetter = "" + RANDOM_LETTER[Random.Range(0, RANDOM_LETTER.Length)];

		employeeId = CITIZEN_PREFIX + " " + selectedNumber + selectedLetter;
		//employeeId = "Employee 999999";



		Debug.Log("ERES EL EMPLEADO: " + employeeId);
	}
}