using UnityEngine;
using System.Collections;

public class SocialManager : MonoBehaviour
{
	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	void Start()
	{
		GameJolt.UI.Manager.Instance.ShowSignIn();
	}


	#region TESTING METHODS
	public void AddScore()
	{
		int scoreValue = 150;
		string scoreText = "150 seconds, Congrats!";
		int tableID = 0;
		string extraData = "";
		GameJolt.API.Scores.Add(scoreValue, scoreText, 0, "", null);
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.P))
		{
			AddScore();
		}
	}
	#endregion
}
