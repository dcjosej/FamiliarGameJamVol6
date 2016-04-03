﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoText : MonoBehaviour
{
	public string text;
	public float letterPause;

	public bool writing = false;

	public int lines = 0;

	private Text textComp;

	void Awake()
	{
		textComp = GetComponent<Text>();
	}

	void Start()
	{
		//StartCoroutine("Write");
	}

	public void TypeText(string textToType)
	{
		if (!writing)
		{
			StartCoroutine(IETypeText(textToType));
		}
	}

	public void TypeTextGameOver()
	{

	}

	private IEnumerator IETypeText(string text)
	{
		writing = true;
		lines++;

		if (lines > 16 && !GameLogic.instance.isGameOver)
		{
			DeleteFirstLine();
		}

		foreach (char letter in text.ToCharArray())
		{
			textComp.text += letter;
			yield return new WaitForSeconds(letterPause);
		}

		if (!GameLogic.instance.isGameOver)
		{
			textComp.text += "\n";
		}
		writing = false;


	}

	private void DeleteFirstLine()
	{
		int index = textComp.text.IndexOf('\n');
		textComp.text = textComp.text.Substring(index + 1);
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