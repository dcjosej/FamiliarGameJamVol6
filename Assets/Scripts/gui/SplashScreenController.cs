using UnityEngine;
using System.Collections;

public class SplashScreenController : MonoBehaviour
{
	public AutoText autoText;
	public MainMenuLogic mainMenuLogic;

	public float splashTime = 2f;

	void Awake()
	{
		autoText.Initialize();
	}

	void OnEnable()
	{
		autoText.TypeText("TEAM COVEN", Utils.OrangeColor);
		StartCoroutine(WaitForSplashScreen());
	}

	private IEnumerator WaitForSplashScreen()
	{
		yield return new WaitForSeconds(splashTime);
		FadersController.instance.FadeToBlack(1);
	}

	void OnDisable()
	{
		StopAllCoroutines();
	}
}