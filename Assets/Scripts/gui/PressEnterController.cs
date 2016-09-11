using UnityEngine;
using System.Collections;

public class PressEnterController : MonoBehaviour
{
	public MainMenuLogic mainMenuLogic;

	void OnEnable()
	{
		KeyboardManager.OnKeyPressed += OnKeyPressed;
	}

	void OnDisable()
	{
		KeyboardManager.OnKeyPressed -= OnKeyPressed;
	}

	private void OnKeyPressed(KeyCode keyCode)
	{
		if(keyCode == KeyCode.Return)
		{
			mainMenuLogic.GoToMainMenuButtons();
		}
	}

}