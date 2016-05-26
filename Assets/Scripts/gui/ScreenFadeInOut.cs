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
	public bool startFading = false;

	private Image fadeImage;

	void Awake()
	{
		fadeImage = GetComponent<Image>();
	}

	
	public void FadeToBlack()
	{
		StartCoroutine(IEFadeToBlack());
	}

	public void FadeToClear()
	{
		StartCoroutine(IEFadeToClear());
	}

	private IEnumerator IEFadeToClear()
	{
		float time = 0f;
		float t = 0f;
		while (time <= fadeTime)
		{
			fadeImage.color = Color.Lerp(Color.black, Color.clear, t);

			//Debug.Log("T: " + t);

			time += Time.deltaTime;
			t = time / fadeTime;
			yield return null;
		}
	}

	private IEnumerator IEFadeToBlack()
	{
		float time = 0f;
		float t = 0f;
		while (time <= fadeTime)
		{
			fadeImage.color = Color.Lerp(Color.clear, Color.black, t);

			time += Time.deltaTime;
			t = time / fadeTime;
			yield return null;
		}

		EndScene();
	}

	void EndScene()
	{
			SceneManager.LoadScene(1);
	}

}
