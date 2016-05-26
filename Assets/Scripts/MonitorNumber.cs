using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonitorNumber : MonoBehaviour
{
	public int monitorNumber;
	private Text text;
	
	void Awake()
	{
		text = GetComponent<Text>();
	}

	void OnEnable()
	{
		UpdateNumber();
	}

	public void UpdateNumber()
	{
		text.text = monitorNumber.ToString();
	}

}
