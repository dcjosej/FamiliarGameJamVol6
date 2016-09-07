using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutosLogic : MonoBehaviour
{

	public TextAsset[] texts;
	private TextAsset currentText;
	private bool endReached;

	public AutoText autoText;
	

	public ScreenFadeInOut fader;

	private int textAssetIndex = 0;

	void Start()
	{
		currentText = texts[0];
		TypeTextAsset();
	}

	void Update()
	{
		CheckKeyboard();
	}

	public void TypeTextAsset()
	{
		autoText.Clean();
		autoText.TypeText(currentText.text, Utils.OrangeColor);
	}
	
	public void NextText()
	{
		textAssetIndex++;
		//textAssetIndex = textAssetIndex >= texts.Length ? texts.Length - 1 : textAssetIndex;

		endReached = textAssetIndex >= texts.Length - 1;
		currentText = texts[textAssetIndex];
	}
	
	public void ShowAllText()
	{
		autoText.ShowAllText(currentText.text);
	}	

	private void CheckKeyboard()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			//SceneManager.LoadScene(1);
			if (endReached && !autoText.writing)
			{
				fader.FadeToBlackLoadScene(2);
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (autoText.writing)
			{
				ShowAllText();
			}
			else
			{
				if (!endReached)
				{
					NextText();
					TypeTextAsset();
				}
			}
		}
	}
}
