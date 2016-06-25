using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MainScreenCursor : MonoBehaviour
{
	public CursorType cursor;

	void OnEnable()
	{
		
	}

	void OnMouseEnter()
	{
		Debug.Log("ENTRANDO!!!");
	}

	public void OnMouseEnter__()
	{
		//Debug.Log("POR FAVOR, LLAMATE :( :(");
		GameLogic.instance.UpdateCursor(cursor);
	}

	public void OnMouseExit__()
	{
		//Debug.Log("SALIENDO DE LA IMAGEN DE LA TELE");
		//GameLogic.instance.UpdateCursor(CursorSelected.OUTSIDE_SCREEN);
	}
}
