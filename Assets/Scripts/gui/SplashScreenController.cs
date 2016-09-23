using UnityEngine;
using System.Collections;

public class SplashScreenController : MonoBehaviour
{
	public AutoText autoText;
	public MainMenuLogic mainMenuLogic;

	public float splashTime = 3f;

	void Awake()
	{
		autoText.Initialize();
		autoText.SetUpConsoleForInput("");
		autoText.textForIntro = true;
	}

	void OnEnable()
	{
		StartCoroutine(StartAnimationSplash());
	}

	private IEnumerator StartAnimationSplash()
	{
		autoText.TypeText("", Utils.OrangeColor);
		yield return new WaitForSeconds(splashTime / 2);
		autoText.Clean();
		autoText.TypeText("TEAM COVEN", Utils.OrangeColor);
		yield return new WaitForSeconds(splashTime / 2);
		//FadersController.instance.FadeToBlack(1);
		mainMenuLogic.BackToMainMenu();
		
	}

	//private IEnumerator WaitForSplashScreen()
	//{
	//	yield return new WaitForSeconds(splashTime);
	//	FadersController.instance.FadeToBlack(1);
	//}

	void OnDisable()
	{
		StopAllCoroutines();
	}
}