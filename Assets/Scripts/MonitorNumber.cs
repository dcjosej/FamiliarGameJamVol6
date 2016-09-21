using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MonitorNumber : MonoBehaviour
{
	public int monitorNumber;
	public Text text { get; set; }
	
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

	public void OnMouseOver()
	{
		if (text.color != Color.white)
		{
			AudioManager.instance.PlayMouseHover();
		}
			text.color = Color.white;
	}

	public void OnMouseExit()
	{
		text.color = new Color(1, 1, 1, 0.38f);
    }

}
