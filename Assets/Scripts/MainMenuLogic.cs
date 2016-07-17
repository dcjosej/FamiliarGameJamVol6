using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuLogic : MonoBehaviour {

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
		ScreenFadeInOut.instance.FadeToBlack(1);
		//SceneManager.LoadScene(1);
	}
}