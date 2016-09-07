using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuLogic : MonoBehaviour {

	public float fadeTimeTransition = 1.4f;

	[Header("GUI References")]
	public CanvasGroup cgHighScoreScreen;
	public CanvasGroup cgMainMenuScreen;


	void Start()
	{
		//ScreenFadeInOut.instance.FadeToClear();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			StartGame();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void StartGame()
	{
		ScreenFadeInOut.instance.FadeToBlackLoadScene(1);
		//SceneManager.LoadScene(1);
	}


	#region BUTTON FUNCTIONS
	public void NewGame()
	{
		StartGame();
	}

	public void HighScores()
	{

		ScreenFadeInOut.instance.BlackAndClear(fadeTimeTransition);
		ScreenFadeInOut.instance.fadeToBlackFinishedCallback = ShowHighScoresScreen;
		cgMainMenuScreen.interactable = false;

		//GameJolt.API.Scores.Get(GetScoresCallback, 169398, 3);
	}

	public void BackToMainMenu()
	{
		ScreenFadeInOut.instance.BlackAndClear(fadeTimeTransition);
		ScreenFadeInOut.instance.fadeToBlackFinishedCallback = ShowMainMenu;
		cgMainMenuScreen.interactable = false;
	}

	private void ShowHighScoresScreen()
	{
		cgMainMenuScreen.gameObject.SetActive(false);
		cgHighScoreScreen.gameObject.SetActive(true);
    }

	private void ShowMainMenu()
	{
		cgMainMenuScreen.gameObject.SetActive(true);
		cgHighScoreScreen.gameObject.SetActive(false);
	}

	//public void GetScoresCallback(GameJolt.API.Objects.Score[] scores)
	//{
	//	foreach(GameJolt.API.Objects.Score score in scores)
	//	{
	//		Debug.Log("Value: " + score.Value + " Name: " + score.GuestName);
	//	}
	//}
	#endregion
}