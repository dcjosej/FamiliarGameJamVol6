using UnityEngine;
using System.Collections;

public class KeyboardManager : MonoBehaviour
{
	private KeyCode keyCode;
	public delegate void OnKeyPressedCallback(KeyCode keyCode);
	public static OnKeyPressedCallback OnKeyPressed;

	void Update()
	{
		keyCode = KeyCode.None;
		if (Input.GetKeyDown(KeyCode.Return))
		{
			keyCode = KeyCode.Return;
			LaunchEvent();
        }

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			keyCode = KeyCode.Escape;
			LaunchEvent();
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			keyCode = KeyCode.DownArrow;
			LaunchEvent();
		}

		if (Input.GetKeyUp(KeyCode.UpArrow))
		{
			keyCode = KeyCode.UpArrow;
			LaunchEvent();
		}
	}

	private void LaunchEvent()
	{
		if(OnKeyPressed != null)
		{
			OnKeyPressed(keyCode);
		}
	}
}