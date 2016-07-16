using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUILogin : MonoBehaviour
{

	public InputField guestName;

	public void LoginGuest()
	{
		//Debug.Log("Hola " + guestName.text);
		SocialManager.instance.LogGuest(guestName.text);
		ScreenFadeInOut.instance.FadeToBlack(2);	
	}

	public void LoginGameJolt()
	{
		SocialManager.instance.LoginWithGamejolt();
	}
}