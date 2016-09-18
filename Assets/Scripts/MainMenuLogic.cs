using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class MainMenuLogic : MonoBehaviour {

	public float fadeTimeTransition = 1.4f;
	public float fadeOutTime;
	public float fadeInTime;

	public bool transitioning { get; set; }

	private CanvasGroup currentCanvasGroup;

	[Header("GUI References")]
	public CanvasGroup cgHighScoreScreen;
	public CanvasGroup cgMainMenuScreen;
	public CanvasGroup cgTutorialScreen;
	public CanvasGroup cgCreditsScreen;
	//public CanvasGroup cgSplashScreen;
    public CanvasGroup cgPressEnter;
	public CanvasGroup cgMainMenuButtons;
	
	public Texture2D normalCursor;


	void Start()
	{
		Cursor.SetCursor(normalCursor, Vector2.one * 32f, CursorMode.Auto);
		currentCanvasGroup = cgMainMenuScreen;
	}

	public void StartGame()
	{
		//ScreenFadeInOut.instance.FadeToBlackLoadScene(1);
		FadersController.instance.FadeToBlack(2);
	}

	#region BUTTON FUNCTIONS
	public void NewGame()
	{
		//StartGame();
		if (!transitioning && !FadersController.instance.fading)
		{
			FadersController.instance.BlackAndClear(fadeInTime, ShowTutorialScreen, true);
			//ScreenFadeInOut.instance.BlackAndClear(fadeTimeTransition);
			//ScreenFadeInOut.instance.fadeToBlackFinishedCallback = ShowTutorialScreen;
			currentCanvasGroup.interactable = false;
		}
	}

	public void HighScores()
	{
		if (!transitioning && !FadersController.instance.fading)
		{
			FadersController.instance.BlackAndClear(fadeTimeTransition, ShowHighScoresScreen, true);
			//ScreenFadeInOut.instance.BlackAndClear(fadeTimeTransition);
			//ScreenFadeInOut.instance.fadeToBlackFinishedCallback = ShowHighScoresScreen;
			currentCanvasGroup.interactable = false;
		}
	}

	public void Credits()
	{
		if(!transitioning && !FadersController.instance.fading)
		{
			FadersController.instance.BlackAndClear(fadeTimeTransition, ShowCreditsScreen, true);
			currentCanvasGroup.interactable = false;
		}
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void BackToMainMenu()
	{
		if (!transitioning && !FadersController.instance.fading)
		{
			FadersController.instance.BlackAndClear(fadeTimeTransition, ShowMainMenu, true);
			//ScreenFadeInOut.instance.BlackAndClear(fadeTimeTransition);
			//ScreenFadeInOut.instance.fadeToBlackFinishedCallback = ShowMainMenu;
			currentCanvasGroup.interactable = false;
		}
	}

	public void GoToMainMenuButtons()
	{
		TransitionTo(cgPressEnter, cgMainMenuButtons);
	}

	private IEnumerator Fade(CanvasGroup canvasGroup, bool fadeIn)
	{
		float time = 0f;
		float t = 0f;

		float fadeTime = fadeIn ? fadeInTime : fadeOutTime;
		float alphaSrc = fadeIn ? 0 : 1;
		float alphaDst = 1 - alphaSrc;

		canvasGroup.interactable = false;
		if (fadeIn)
		{
			canvasGroup.gameObject.SetActive(true);
		}
		//src.interactable = false;

		//dst.gameObject.SetActive(true);
		//dst.interactable = false;

		while (t <= 1)
		{
			time += Time.deltaTime;
			t = time / fadeTime;

			canvasGroup.alpha = Mathf.Lerp(alphaSrc, alphaDst, t);
			//dst.alpha = Mathf.Lerp(0, 1, t);
			yield return null;
		}

		if (fadeIn)
		{
			transitioning = false;
		}

		canvasGroup.interactable = fadeIn;
		canvasGroup.gameObject.SetActive(fadeIn);

	}

	private void TransitionTo(CanvasGroup src, CanvasGroup dst)
	{
		if (!transitioning)
		{
			transitioning = true;
			StartCoroutine(Fade(src, false));
			StartCoroutine(Fade(dst, true));
		}
	}

	private void ShowHighScoresScreen()
	{
		currentCanvasGroup.gameObject.SetActive(false);
		cgHighScoreScreen.gameObject.SetActive(true);
		cgHighScoreScreen.interactable = true;
		currentCanvasGroup = cgHighScoreScreen;
    }

	private void ShowMainMenu()
	{
		currentCanvasGroup.gameObject.SetActive(false);
		cgMainMenuScreen.gameObject.SetActive(true);
		cgMainMenuScreen.interactable = true;
		currentCanvasGroup = cgMainMenuScreen;
	}

	private void ShowTutorialScreen()
	{
		currentCanvasGroup.gameObject.SetActive(false);
		cgTutorialScreen.gameObject.SetActive(true);
		cgTutorialScreen.interactable = true;
		currentCanvasGroup = cgTutorialScreen;
	}

	private void ShowCreditsScreen()
	{
		currentCanvasGroup.gameObject.SetActive(false);
		cgCreditsScreen.gameObject.SetActive(true);
		cgCreditsScreen.interactable = true;
		currentCanvasGroup = cgCreditsScreen;
	}

	#endregion
}