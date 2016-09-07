using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUILogin : MonoBehaviour
{

	public InputField guestName;

	public void LoginGameJolt()
	{
		SocialManager.instance.LoginWithGamejolt();
	}
}