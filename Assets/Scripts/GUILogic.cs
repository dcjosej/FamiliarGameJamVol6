using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUILogic : MonoBehaviour {

	public Text numbersOfCharacters;

	void Update()
	{
		UpdateNumbersOfCharacters();
	}

	public void UpdateNumbersOfCharacters()
	{
		numbersOfCharacters.text = GameLogic.instance.charactersInScene.ToString();
	}
}