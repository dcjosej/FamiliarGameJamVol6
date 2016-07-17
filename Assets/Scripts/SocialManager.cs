using UnityEngine;
using System.Collections;

public class SocialManager : MonoBehaviour
{

	public const int MAIN_LEADERBOARD_ID = 169398;

	private bool loggedLikeGuest;
	public string guestName;

	private static SocialManager _instance;
	public static SocialManager instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<SocialManager>();
			}
			return _instance;
		}
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	void Start()
	{
		//GameJolt.UI.Manager.Instance.ShowSignIn();
	}

	public void LogGuest(string guest)
	{
		Debug.Log("GUEST NAME: " + guest);
	}

	public void LoginWithGamejolt()
	{
		GameJolt.UI.Manager.Instance.ShowSignIn();
	}

	public void AddScore(int value, int tableId)
	{
		string text = "A ESTE TEXTO HAY QUE DARLE UNA VUERTESITA"; //TODO: CREAR TEXTO PARA LEADERBOARDS
		GameJolt.API.Scores.Add(value, text, tableId, "", (bool success) => 
		{
			Debug.Log("EXITO!");
		});
    }


	#region TESTING METHODS
	/*
	public void AddScore()
	{
		int scoreValue = 150;
		string scoreText = "150 seconds, Congrats!";
		int tableID = 0;
		string extraData = "";
		GameJolt.API.Scores.Add(scoreValue, scoreText, 0, "", null);
	}
	*/
	

#if UNITY_EDITOR
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.P))
		{
			//AddScore();
		}

		if (Input.GetKeyUp(KeyCode.O))
		{
			GameJolt.UI.Manager.Instance.ShowLeaderboards();
		}
	}
#endif

#endregion
}
