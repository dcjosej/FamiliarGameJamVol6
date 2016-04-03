using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TutosLogic : MonoBehaviour
{
	void Update()
	{

		CheckKeyboard();
	}	

	private void CheckKeyboard()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			SceneManager.LoadScene(1);
		}
	}
}
