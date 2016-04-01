using UnityEngine;
using UnityEngine.UI;

public class FinalMessageController : MonoBehaviour
{

	void Update()
	{
		if(PlayerPrefs.GetInt("Winner", 0) == 0)
		{
			GetComponent<Text>().text = "GAME OVER!";
		}
		else
		{
			GetComponent<Text>().text = "OLE OLE! HAS GANAO!";
		}
	}

}
