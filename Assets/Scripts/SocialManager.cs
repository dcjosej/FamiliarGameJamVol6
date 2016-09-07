using UnityEngine;
using GameJolt.API.Objects;
using System.Collections;

public class SocialManager : MonoBehaviour
{

	public const int MAIN_LEADERBOARD_ID = 169398;

	private bool loggedLikeGuest;
	public string prefixTextScore = "Service time:";

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

	public void LoginWithGamejolt()
	{
		GameJolt.UI.Manager.Instance.ShowSignIn();
	}

	public void AddScore(int value, string serviceTime)
	{
		string text = "Service time:" + " " + serviceTime;
		GameJolt.API.Scores.Add(value, text, PersistentData.instance.employeeId, MAIN_LEADERBOARD_ID, "", AddScoreCallback);
    }

	public void GetScores(System.Action<Score[]> getScoresCallback)
	{
		GameJolt.API.Scores.Get(getScoresCallback, 169398, 3);
	}

	private void AddScoreCallback(bool success)
	{
		if (!success)
		{
			Debug.Log("######### NO SE HA PODIDO SUBIR LA PUNTUACION A LOS LEADERBOARDS Y DEBERIA SALIR ALGO POR LA CONSOLA #########");
		}
	}


}
