using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonsController : MonoBehaviour
{
	public CustomButton[] buttons;
	private int selectedButtonIndex = 0;


	void Awake()
	{
		foreach (CustomButton button in buttons)
		{
			button.Init();
		}
		buttons[selectedButtonIndex].Select(false);
	}

	void OnEnable()
	{
		KeyboardManager.OnKeyPressed += OnKeyPressed;
		selectedButtonIndex = 0;
		UpdateSelectedButton(false);
	}

	void OnDisable()
	{
		KeyboardManager.OnKeyPressed -= OnKeyPressed;
	}



	public void SelectButton(CustomButton selectedButton, BaseEventData baseEventData = null)
	{
		foreach(CustomButton button in buttons)
		{
			if(button == selectedButton)
			{
				continue;
			}

			button.Deselect();
		}
	}



	private void PreviousButton()
	{
		selectedButtonIndex--;
		if(selectedButtonIndex < 0)
		{
			selectedButtonIndex = buttons.Length - 1;
		}

		UpdateSelectedButton();

	}

	private void NextButton()
	{
		//buttons[selectedButtonIndex].
		selectedButtonIndex++;

		if(selectedButtonIndex >= buttons.Length)
		{
			selectedButtonIndex = 0;
		}

		UpdateSelectedButton();
	}

	private void UpdateSelectedButton(bool playSound = true)
	{

		buttons[selectedButtonIndex].Select(playSound);

		SelectButton(buttons[selectedButtonIndex]);
	}

	private void OnKeyPressed(KeyCode keyCode)
	{
		switch (keyCode)
		{
			case KeyCode.DownArrow:
				NextButton();
				break;
			case KeyCode.UpArrow:
				PreviousButton();
				break;
			case KeyCode.Return:
				buttons[selectedButtonIndex].ExectueFunction();
				break;
		}
	}
}