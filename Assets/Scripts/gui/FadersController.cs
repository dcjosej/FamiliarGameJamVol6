using UnityEngine;
using System.Collections;

public class FadersController : MonoBehaviour {

	private static FadersController _instance;
	public static FadersController instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<FadersController>();
			}
			return _instance;
		}
	}

	public bool fading { get; set; }

	[Header("Faders")]
	public ScreenFadeInOut innerFader;
	public ScreenFadeInOut outerFader;


	public void BlackAndClear(float fadeTime, ScreenFadeInOut.FadeToBlackFinishedDelegate fadeToBlackCallback = null, bool isInnerFader = false)
	{
		ScreenFadeInOut selectedFader = isInnerFader ? innerFader : outerFader;
		selectedFader.BlackAndClear(fadeTime, fadeToBlackCallback);
	}

	public void FadeToBlack(int nextScene, bool isInnerFader = false)
	{
		ScreenFadeInOut selectedFader = isInnerFader ? innerFader : outerFader;
		selectedFader.FadeToBlackLoadScene(nextScene);
	}
}
