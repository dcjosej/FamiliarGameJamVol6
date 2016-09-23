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
	public TextAsset bestService;
	public TextAsset noScore;
	public TextAsset citizenId;

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
		autotext.TypeText("", Utils.OrangeColor);
		for(int i = 0; i < scores.Length; i++)
		{
			Score score = scores[i];
			autotext.TypeText(string.Format(entry.text, i+1, score.GuestName, Utils.SecondsTozzHHmmss(score.Value)), Utils.OrangeColor);
		}

		autotext.TypeText("", Utils.OrangeColor);
		autotext.TypeText(footer.text, Utils.OrangeColor);
		autotext.TypeText("", Utils.OrangeColor);

		float bestSore = PersistentData.instance.bestScore;
		string bestScoreFormatted = noScore.text;
		if(bestSore >= 0)
		{
			bestScoreFormatted = Utils.SecondsTozzHHmmss(bestSore);
		}

		autotext.TypeText(string.Format(citizenId.text, PersistentData.instance.employeeId), Utils.OrangeColor);
		autotext.TypeText(string.Format(bestService.text, bestScoreFormatted), Utils.OrangeColor);
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