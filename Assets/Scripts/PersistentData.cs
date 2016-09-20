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



	private const string EMPLOYEE_PREFIX = "Employee";
	private const string RANDOM_NUMBER = "0123456789";


	//public int currentSector { get; set; }
	public string currentLetter { get; set; }
	public string employeeId { get; set; }
	public int numberOfDigitsIdEmployee = 6;

	void Awake()
	{
		//InitRandomData();
		InitEmployeeId();
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


		employeeId = EMPLOYEE_PREFIX + " " + selectedNumber;
		//employeeId = "Employee 999999";



		Debug.Log("ERES EL EMPLEADO: " + employeeId);
	}
}