using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuLogic : MonoBehaviour {

	void Start()
	{
		ScreenFadeInOut.instance.FadeToClear();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			StartGame();
		}
	}

	public void StartGame()
	{
		SceneManager.LoadScene(2);
	}
}