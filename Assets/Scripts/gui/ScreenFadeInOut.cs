using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFadeInOut : MonoBehaviour
{

	private static ScreenFadeInOut _instance;
	public static ScreenFadeInOut instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<ScreenFadeInOut>();
			}
			return _instance;
		}
	}


	public float fadeTime = 1f; //Tiempo de fade en segundos

	public bool sceneStarting = true;
	public bool fading { get; set; }
	private CanvasGroup canvasGroup;

	private Image fadeImage;

	public delegate void FadeToBlackFinishedDelegate();
	public FadeToBlackFinishedDelegate fadeToBlackFinishedCallback;



	void Awake()
	{
		fadeImage = GetComponent<Image>();
	}

	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();

		FadeToClear();
	}

	//public void FadeToBlack(float fadeTime)
	//{
	//	this.fadeTime = fadeTime;	
	//}

	public void BlackAndClear(float fadeTime)
	{
		this.fadeTime = fadeTime;
		StartCoroutine(BlackAndClearIE());
	}

	private IEnumerator BlackAndClearIE()
	{
		yield return StartCoroutine(IEFadeToBlack(-1));
		yield return StartCoroutine(IEFadeToClear());
	}

	public void FadeToBlackLoadScene(int nextScene)
	{
		StartCoroutine(IEFadeToBlack(nextScene));
	}

	public void FadeToClear()
	{
		StartCoroutine(IEFadeToClear());
	}

	private IEnumerator IEFadeToClear()
	{
		fadeImage.gameObject.SetActive(true);


		fading = true;
		canvasGroup.blocksRaycasts = fading;


		float time = 0f;
		float t = 0f;
		while (time <= fadeTime)
		{
			time += Time.deltaTime;
			t = time / fadeTime;
			fadeImage.color = Color.Lerp(Color.black, Color.clear, t);

			yield return null;
		}

		fading = false;
		canvasGroup.blocksRaycasts = fading;
		//fadeImage.gameObject.SetActive(false);
	}

	private IEnumerator IEFadeToBlack(int nextScene)
	{
		fadeImage.gameObject.SetActive(true);
		float time = 0f;
		float t = 0f;

		fading = true;
		canvasGroup.blocksRaycasts = fading;

		while (time <= fadeTime)
		{
			time += Time.deltaTime;
			t = time / fadeTime;
			fadeImage.color = Color.Lerp(Color.clear, Color.black, t);


			yield return null;
		}

		fading = false;
		canvasGroup.blocksRaycasts = fading;
		//fadeImage.gameObject.SetActive(false);


		if (fadeToBlackFinishedCallback != null)
		{
			fadeToBlackFinishedCallback();
		}

		if (nextScene > -1)
		{
			EndScene(nextScene);
		}
	}

	void EndScene(int nextScene)
	{
		SceneManager.LoadSceneAsync(nextScene);
	}
}