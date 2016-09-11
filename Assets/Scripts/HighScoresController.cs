using UnityEngine;
using GameJolt.API.Objects;
using System.Collections;

public class HighScoresController : MonoBehaviour
{

	public MainMenuLogic mainMenuLogic;

	[Header("GUI References")]
	public AutoText autotext;

	[Header("Texts")]
	public TextAsset header;
	public TextAsset entry;
	public TextAsset footer;

	void OnEnable()
	{
		KeyboardManager.OnKeyPressed += OnKeyPressed;

		autotext.Initialize();
		autotext.TypeText(header.text, Utils.OrangeColor);
		SocialManager.instance.GetScores(ShowHighscores);
	}

	void OnDisable()
	{
		KeyboardManager.OnKeyPressed -= OnKeyPressed;
	}

	private void ShowHighscores(Score[] scores)
	{
		autotext.TypeText("\n", Utils.OrangeColor);
		foreach(Score score in scores)
		{
			autotext.TypeText(string.Format(entry.text, score.GuestName, score.Value), Utils.OrangeColor);
		}

		autotext.TypeText("\n", Utils.OrangeColor);
		autotext.TypeText(footer.text, Utils.OrangeColor);
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