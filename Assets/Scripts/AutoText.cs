using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AutoText : MonoBehaviour
{
	public string text;
	public float letterPause = 0.1f;

	private Text textComp;

	void Awake()
	{
		textComp = GetComponent<Text>();
	}

	void Start()
	{
		StartCoroutine("Write");
	}

	private IEnumerator Write()
	{
		foreach(char letter in text.ToCharArray())
		{
			textComp.text += letter;
			yield return new WaitForSeconds(letterPause);
		}
		textComp.text += "\n";

		foreach (char letter in "Esta es la segunda linea".ToCharArray())
		{
			textComp.text += letter;
			yield return new WaitForSeconds(letterPause);
		}
		textComp.text += "\n";
	}

}
