using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class AutoText : MonoBehaviour
{
	public string text;
	public float letterPause;

	public bool writing = false;

	public int lines = 0;

	private Text textComp;

	public TextAsset textAsset;
	public bool longText = true;

	private int initIndex = -1;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{

			StopAllCoroutines();

			textComp.text = textAsset.text;
			letterPause = 0;
		}
	}

	void Awake()
	{
		textComp = GetComponent<Text>();
		initIndex = textComp.text.Length - 1;
	}

	public void Clean()
	{
		textComp.text = "";
	}

	void Start()
	{
		if (longText)
		{
			StartCoroutine(IETypeText2(textAsset.text));
		}
	}

	public void TypeText(string textToType, string htmlColor)
	{
		if (!writing)
		{
			StartCoroutine(IETypeText(textToType, htmlColor));
		}
	}

	public void TypeTextGameOver()
	{

	}

	private IEnumerator IETypeText2(string text)
	{
		
		foreach (char letter in text.ToCharArray())
		{
			textComp.text += letter;
			yield return new WaitForSeconds(letterPause);
		}

	}

	private IEnumerator IETypeText(string text, string htmlColor)
	{
		writing = true;
		lines++;

		textComp.text += "<color='#" + htmlColor.ToString() + "'> </color>";
		initIndex += 9 + htmlColor.Length + 3;
		//initIndex += 17;

		if (lines > 2 && !GameLogic.instance.isGameOver)
		{
			DeleteFirstLine();
		}

		foreach (char letter in text.ToCharArray())
		{
			textComp.text = textComp.text.Insert(initIndex, ""+letter);
			//textComp.text.Insert(initIndex++, message);
			//textComp.text += letter;
			initIndex++;
			yield return new WaitForSeconds(letterPause);
		}

		if (!GameLogic.instance.isGameOver)
		{
			textComp.text += "\n";
			initIndex += 9;
        }
		writing = false;


	}

	private void DeleteFirstLine()
	{

		string pattern = @"(<color='[#\w+]+'>)([\w\s\\.\\:]*)<\/color>";
		Regex regex = new Regex(pattern);
		MatchCollection matches = regex.Matches(textComp.text);
        string substringColor = matches[0].Groups[1].ToString();
		string substringText = matches[0].Groups[2].ToString();
		//int indexColor = textComp.text.

		int index = textComp.text.IndexOf('\n');
		textComp.text = textComp.text.Substring(index + 1);
		if (substringText.Contains("\n"))
		{
			textComp.text = textComp.text.Insert(0, substringColor);
			initIndex -= (substringText.IndexOf('\n') + 1);
		}
		else
		{
			initIndex -= (matches[0].ToString().Length + 1);
        }

		
		
	}

	private IEnumerator Write()
	{
		foreach(char letter in text.ToCharArray())
		{
			textComp.text += letter;
			yield return new WaitForSeconds(letterPause);
		}
		textComp.text += "\n";


		yield return new WaitForSeconds(1f);
		int index = textComp.text.IndexOf('\n');
		textComp.text = textComp.text.Substring(index + 1);

		foreach (char letter in "Esta es la segunda linea".ToCharArray())
		{
			textComp.text += letter;
			yield return new WaitForSeconds(letterPause);
		}
		textComp.text += "\n";

		yield return new WaitForSeconds(1f);
	}
}