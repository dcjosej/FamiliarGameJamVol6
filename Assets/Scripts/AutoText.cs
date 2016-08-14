﻿using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class AutoText : MonoBehaviour
{
	public float timeBetweenCharacters;
	public bool longText = true;
	//public TextAsset textAsset;
	public int maxNumLines = 10;

	public bool writing { get; set; }
	public int lines { get; set; }
	public bool alowKeyboardTyping = false;

	private Text textComp;



	private int initIndex = -1;


	/// <summary>
	/// Text that is being shown in terminal in this moment
	/// </summary>
	private string textInConsole = "";
	private string textInConsoleWithoutMarker = "";
	private string textMarker = "<size=10>_</size>";



	//COROUTINES
	private Coroutine typeTextCoroutine;

	public void ShowAllText(string text)
	{
		StopAllCoroutines();

		textComp.text = text;
		//timeBetweenCharacters = 0;

		writing = false;
	}

	void Update()
	{

		if (alowKeyboardTyping)
		{
			if (Input.GetKeyDown(KeyCode.Y))
			{
				TypeText("Y", Utils.OrangeColor);
			}
		}

		/*
		if (Input.GetKeyDown(KeyCode.Space))
		{

			StopAllCoroutines();

			textComp.text = textAsset.text;
			timeBetweenCharacters = 0;
		}
		*/
	}

	void Awake()
	{
		textComp = GetComponent<Text>();
		initIndex = textComp.text.Length - 1;
		StartCoroutine(AnimateText());
	}

	public void Clean()
	{
		textComp.text = "";
		textInConsole = "";
		textInConsoleWithoutMarker = "";
		textMarker = "<size=10>_</size>";
		initIndex = -1;
	}

	void Start()
	{
		lines = 0;
		//if (longText)
		//{
		//	//StartCoroutine(IETypeText2(textAsset.text));
		//}
	}

	public void TypeText(string textToType, string htmlColor)
	{
		if (!writing)
		{
			//StopAllCoroutines();
			if(typeTextCoroutine != null)
			{
				StopCoroutine(typeTextCoroutine);
			}
			typeTextCoroutine = StartCoroutine(IETypeText(textToType, htmlColor));
		}
	}

	public void TypeTextGameOver()
	{

	}

	
	//TODO: UTILIZAR SOLO UNA FUNCION DE ESCRIBIR
	private IEnumerator IETypeText2(string text)
	{
		writing = true;

		foreach (char letter in text.ToCharArray())
		{
			textComp.text += letter;
			yield return new WaitForSeconds(timeBetweenCharacters);			
		}
		writing = false;
	}
	

	private IEnumerator IETypeText(string text, string htmlColor)
	{
		writing = true;
		lines++;

		if(textInConsoleWithoutMarker != "")
		{
			textComp.text = textInConsoleWithoutMarker;
		}
		textComp.text += "<color='#" + htmlColor.ToString() + "'> </color>";


		initIndex += 9 + htmlColor.Length + 3;
		//initIndex += 17;
		//initIndex++;
		if (lines > maxNumLines && !GameLogic.instance.isGameOver)
		{
			DeleteFirstLine();
			UpdateStringFields(false);
			textComp.text = textInConsole;
		}

		foreach (char letter in text.ToCharArray())
		{
			textComp.text = textComp.text.Insert(initIndex, ""+letter);
			//textComp.text.Insert(initIndex++, message);
			//textComp.text += letter;
			initIndex++;
			UpdateStringFields(false);
			yield return new WaitForSeconds(timeBetweenCharacters);
		}

		writing = false;

		//if (!GameLogic.instance.isGameOver )
		//{
			UpdateStringFields(true);
			textComp.text = textInConsoleWithoutMarker;
			//textComp.text += "\n";
			textComp.text += textMarker;
			textInConsole = textComp.text;
			
			initIndex += 9;
        //}
	}

	private void CheckTextMarker()
	{
		if (!textComp.text.Contains(textMarker))
		{
			textComp.text += textMarker;
		}
	}

	private void UpdateStringFields(bool newLine)
	{
		textInConsole = textComp.text;
		if (!textInConsole.Contains(textMarker))
		{
			textInConsole += textMarker;
		}
		//textInConsoleWithoutMarker = textInConsole + textMarker;

		if (newLine)
		{
			textInConsoleWithoutMarker = textInConsole.Substring(0, textInConsole.Length - textMarker.Length);
			textInConsoleWithoutMarker += "\n";
		}
		else
		{
			textInConsoleWithoutMarker = textInConsole.Substring(0, textInConsole.Length - textMarker.Length);
		}
	}

	private IEnumerator AnimateText()
	{
		while (true)
		{
			if (textInConsoleWithoutMarker != "")
			{
				textComp.text = textInConsoleWithoutMarker;
				yield return new WaitForSeconds(0.5f);
				textComp.text = textInConsole;
				yield return new WaitForSeconds(0.5f);
			}
			else
			{
				yield return new WaitForSeconds(0.5f);
			}
		}
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

	/*
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
	*/
}