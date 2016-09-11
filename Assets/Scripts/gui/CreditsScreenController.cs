using UnityEngine;

public class CreditsScreenController : MonoBehaviour
{

	public MainMenuLogic mainMenuLogic;
	public AutoText autoText;

	[Header("Texts")]
	public TextAsset creditsText;

	public void OnEnable()
	{
		KeyboardManager.OnKeyPressed += OnKeyPressed;
		autoText.Initialize();
		autoText.TypeText(creditsText.text, Utils.OrangeColor);
	}

	public void OnDisable()
	{
		KeyboardManager.OnKeyPressed -= OnKeyPressed;
	}

	private void OnKeyPressed(KeyCode keyCode)
	{
		switch (keyCode)
		{
			case KeyCode.Escape:
				mainMenuLogic.BackToMainMenu();
				break;
		}
	}
}