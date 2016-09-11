using UnityEngine;

public class TutorialScreenController : MonoBehaviour
{
	public AutoText autoText;
	public MainMenuLogic mainMenuLogic;

	[Header("Texts")]
	public TextAsset[] tutorialTexts;
	private int currentTextIndex = -1;

	void Awake()
	{
		autoText.Initialize();
	}

	void OnEnable()
	{
		KeyboardManager.OnKeyPressed += OnKeyPressed;
		TypeNextText();
	}

	private void TypeNextText()
	{
		currentTextIndex++;
		if (currentTextIndex >= tutorialTexts.Length)
		{
			mainMenuLogic.StartGame();
        }
		else
		{
			autoText.Clean();
			autoText.TypeText(tutorialTexts[currentTextIndex].text, Utils.OrangeColor);
		}
	}

	void OnDisable()
	{
		KeyboardManager.OnKeyPressed -= OnKeyPressed;
		currentTextIndex = -1;
	}

	private void OnKeyPressed(KeyCode keyCode)
	{
		switch (keyCode)
		{
			case KeyCode.Return:
				TypeNextText();
				break;
		}
	}
}