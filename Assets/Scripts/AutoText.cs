using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class AutoText : MonoBehaviour
{

	#region EVENTS
	public delegate void OnInputReceivedCallback(string input);
	public static event OnInputReceivedCallback OnInputReceived;
	#endregion



	public float timeBetweenCharacters;
	public bool longText = true;
	//public TextAsset textAsset;
	public int maxNumLines = 10;

	public bool writing { get; set; }
	public int lines { get; set; }
	public bool allowKeyboardTyping = false;
	[Range(10f, 30f)]
	public float markerSize = 10;
	public float markerAnimationTime = 0.5f;
	public bool markerToNextLine = true;

	private Text textComp;



	private int initIndex = -1;


	/// <summary>
	/// Text that is being shown in terminal in this moment
	/// </summary>
	private string textInConsole = "";
	private string textInConsoleWithoutMarker = "";
	private string textMarker = "<size=10>_</size>";

	#region CONSTANTES
	public const string PLAY_AGAIN_TEXT = "Has sido despedido. ¿Quieres ser reasignado a otro sector? (y/n): ";
	public const string WRONG_INPUT = "Invalid Input!";
	public const string GOOD_LUCK = "Good Luck!";
	public const string NOT_PLAY_AGAIN = "...";
	#endregion

	#region KEYBOARD TYPING

	private string previousConsoleMessage = "";

	private int maximunCharacters = 1;
	private string input = "";
	#endregion


	//COROUTINES
	private Coroutine typeTextCoroutine;



	private Queue<IEnumerator> queueCoroutines;
	private bool processingQueue = false;

	
	void OnDisable()
	{
		StopAllCoroutines();
	}

	public void ShowAllText(string text)
	{
		StopAllCoroutines();

		textComp.text = text;
		//timeBetweenCharacters = 0;

		writing = false;
	}

	void Update()
	{
		//Debug.Log("ENTRADA DE TECLADO: " + Input.inputString);
        if (allowKeyboardTyping && !writing)
		{
			if (input.Length < maximunCharacters)
			{

				string selectedCharacter = "";
				
				if(Input.inputString.Length > 0 && !Input.inputString.Contains("\r"))
				{
					selectedCharacter = Input.inputString[0].ToString();
                }
				
				input += selectedCharacter;
				//if (Input.GetKeyDown(KeyCode.Y))
				//{
				//	selectedCharacter = "y";
				//	input += selectedCharacter;
				//}

				//if (Input.GetKeyDown(KeyCode.N))
				//{
				//	selectedCharacter = "n";
				//	input += selectedCharacter;
				//}

				if(selectedCharacter != "")
				{
					initIndex -= 7;

					textInConsoleWithoutMarker = textInConsoleWithoutMarker.Insert(initIndex, selectedCharacter);
					textInConsole = textInConsole.Insert(initIndex, selectedCharacter);
					textComp.text = textInConsole;
				}

			}

			if (Input.GetKeyDown(KeyCode.Backspace) && input.Length > 0)
			{
				BackSpace();
			}

			if (Input.GetKeyDown(KeyCode.Return))
			{

				if(OnInputReceived != null)
				{
					OnInputReceived(input);
				}
				
				//if(input.Length > 0)
				//{
				//	initIndex += 8;
				//	input = "";
				//}
				//StartCoroutine(ConsoleError(WRONG_INPUT, Utils.RedColor));
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

	public void ConsoleResponse(string msg, string hexColor, bool repeatMessage)
	{
		if (input.Length > 0)
		{
			initIndex += 8;
			input = "";
		}
		StartCoroutine(ConsoleError(msg, hexColor, repeatMessage));
	}

	private IEnumerator ConsoleError(string msg, string htmlColor, bool repeatMessage = true)
	{
		allowKeyboardTyping = false;
		markerToNextLine = !allowKeyboardTyping;
		//yield return StartCoroutine(IETypeText("", htmlColor));
		//textInConsoleWithoutMarker += "\n";
		//initIndex++;
		//yield return StartCoroutine(IETypeText("", htmlColor));

		initIndex++;
		UpdateStringFields(true);

		yield return StartCoroutine(IETypeText(msg, htmlColor));

		if (repeatMessage)
		{
			allowKeyboardTyping = true;
			markerToNextLine = !allowKeyboardTyping;
			yield return StartCoroutine(IETypeText(PLAY_AGAIN_TEXT, Utils.OrangeColor));
		}
		//allowKeyboardTyping = true;
	}

	public void StopTyping()
	{
		StopCoroutine(typeTextCoroutine);
	}

	void Awake()
	{
		//if(typeTextCoroutine != null)
		//{
		//	StopCoroutine(typeTextCoroutine);
		//}
		//textComp = GetComponent<Text>();
		//initIndex = textComp.text.Length - 1;
		//StartCoroutine(AnimateText());
	}

	public void Initialize()
	{
		if (typeTextCoroutine != null)
		{
			StopCoroutine(typeTextCoroutine);
		}
		textComp = GetComponent<Text>();
		initIndex = textComp.text.Length - 1;
		StartCoroutine(AnimateText());

		lines = 0;
		queueCoroutines = new Queue<IEnumerator>();

		Clean();
	}

	public void Clean()
	{
		if (typeTextCoroutine != null)
		{
			StopCoroutine(typeTextCoroutine);
		}
		//StopAllCoroutines();
		textComp.text = "";
		textInConsole = "";
		textInConsoleWithoutMarker = "";
		textMarker = "<size=" + markerSize + ">_</size>";
		initIndex = -1;
		writing = false;
		processingQueue = false;
	}

	void Start()
	{
		//lines = 0;
		////if (longText)
		////{
		////	//StartCoroutine(IETypeText2(textAsset.text));
		////}
		//queueCoroutines = new Queue<IEnumerator>();
    }

	public void TypeText(string textToType, string htmlColor)
	{
		//if (!writing)
		//{
			//StopAllCoroutines();
			//if(typeTextCoroutine != null)
			//{
			//	StopCoroutine(typeTextCoroutine);
			//}

			queueCoroutines.Enqueue(IETypeText(textToType, htmlColor));
			if (!processingQueue)
			{
				typeTextCoroutine = StartCoroutine(ProcessStack());
			}
				//typeTextCoroutine = StartCoroutine(IETypeText(textToType, htmlColor));
		//}
	}

	private IEnumerator ProcessStack()
	{
		processingQueue = true;
		while(queueCoroutines.Count > 0)
		{
			IEnumerator coroutine = queueCoroutines.Dequeue();
			while (coroutine.MoveNext())
			{
				yield return coroutine.Current;
			}
		}
		processingQueue = false;
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
		textComp.text += "<color='#" + htmlColor.ToString() + "'></color>";


		initIndex += 9 + htmlColor.Length + 3;
		
		//initIndex += 17;
		//initIndex++;
		if (lines > maxNumLines)
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


		UpdateStringFields(markerToNextLine);

		if (allowKeyboardTyping)
		{
			initIndex--;
		}

		//if (!allowKeyboardTyping)
		//{
		//	UpdateStringFields(true);
		//}
		//else
		//{
		//	UpdateStringFields(false);
		//	initIndex--;
		//}


		//UpdateStringFields(!allowKeyboardTyping);
		textComp.text = textInConsoleWithoutMarker;
		//textComp.text += "\n";
		textComp.text += textMarker;
		textInConsole = textComp.text;


		initIndex += 9;
		initIndex--;
        //}
	}

	private void BackSpace()
	{
		
		textInConsoleWithoutMarker = textInConsoleWithoutMarker.Remove(initIndex, 1);
		textInConsole = textInConsole.Remove(initIndex, 1);
		textComp.text = textInConsole;

		
		input = input.Substring(0, input.Length - 1);
		initIndex += 7;
		//initIndex--;


	}

	//private void CheckTextMarker()
	//{
	//	if (!textComp.text.Contains(textMarker))
	//	{
	//		textComp.text += textMarker;
	//	}
	//}

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
				yield return new WaitForSeconds(markerAnimationTime);
				textComp.text = textInConsole;
				yield return new WaitForSeconds(markerAnimationTime);
			}
			else
			{
				yield return new WaitForSeconds(markerAnimationTime);
			}
		}
	}

	private void DeleteFirstLine()
	{
		string pattern = @"(<color='[#\w+]+'>)([\w\s\\.\\:\/\)\(\¿\?!]*)<\/color>";
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

		//if (allowKeyboardTyping)
		//{
		//	initIndex += 2;
		//}
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