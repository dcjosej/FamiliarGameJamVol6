using UnityEngine;
using System.Collections;

public class MainCameraRTInputReceiver : MonoBehaviour
{
	public Camera cameraRT;
	public MonitorNumber currentMonitorNumber;

	/// <summary>
	/// Persona a la que estamos apuntando
	/// </summary>
	private PersonController currentPersonController;


	//public Texture2D cursorTexture;
	//private CursorMode cursorMode = CursorMode.Auto;
	//private Vector2 hotSpot = Vector2.one * 0.5f;

	

	void OnMouseDown()
	{
		Debug.Log("Pinchando en la pantalla!");
		//RayRenderTexture();
		if(currentPersonController != null)
		{
			currentPersonController.Click();
		}
	}
	/*
	void OnMouseOver()
	{
		Debug.Log("ON MOUSE OVER!!!!!!");
		//GameLogic.instance.UpdateCursor(CursorType.NORMAL);
		StartCoroutine(WaitEndFrameToChangeCursor());
	}
	*/

	private IEnumerator WaitEndFrameToChangeCursor()
	{
		yield return new WaitForEndOfFrame();
		//GameLogic.instance.UpdateCursor(CursorType.NORMAL);
	}

	void OnMouseEnter()
	{
		Debug.Log("DEBERIA CAMBIAR MI CURSOR!!");
		GameLogic.instance.UpdateCursor(CursorType.NORMAL);
	}

	void OnMouseOver()
	{
		Cursor.visible = false;
		CheckRaycast();
	}


	void OnMouseExit()
	{
		GameLogic.instance.UpdateCursor(CursorType.OUTSIDE_SCREEN);
	}

	public void SetCamera(Camera camera)
	{
		cameraRT = camera;
	}


	private void CheckRaycast()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;


		if (Physics.Raycast(ray, out hit) && !GameLogic.CURSOR_CHANGING)
		{
			Debug.Log("Primer ray: " + hit.collider.name);


			ray = cameraRT.ViewportPointToRay(hit.textureCoord);

			if (Physics.Raycast(ray, out hit))
			{
				Debug.Log("Chocando con cosas del mundo real por dios!!: " + hit.collider.tag);

				if (hit.collider.tag == "StandardPerson")
				{
					currentPersonController = hit.collider.GetComponent<PersonController>();
					GameLogic.instance.UpdateCursor(CursorType.HOVER);
					//GameLogic.instance.UpdateCursor(currentPersonController.converted ? CursorType.CORRECT : CursorType.FAIL);
				}
				else
				{
					GameLogic.instance.UpdateCursor(CursorType.NORMAL);
				}
			}
			else
			{
				GameLogic.instance.UpdateCursor(CursorType.NORMAL);
			}
		}
	}
	

	private void RayRenderTexture()
	{

		/*
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;


		if (Physics.Raycast(ray, out hit))
		{
			Debug.Log("Primer ray: " + hit.collider.name);
			ray = cameraRT.ViewportPointToRay(hit.textureCoord);

			if (Physics.Raycast(ray, out hit))
			{
				Debug.Log("Chocando con cosas del mundo real por dios!!: " + hit.collider.tag);
				if (hit.collider.tag == "StandardPerson")
				{
					PersonController pc = hit.collider.GetComponent<PersonController>();
					//DifferentPersonController dpc = hit.collider.GetComponent<DifferentPersonController>();

					//if (pc.enabled)
					//{
						pc.Click();
						GameLogic.instance.ChangeCursorInGame(pc.converted ? CursorType.CORRECT : CursorType.FAIL);
					//}
					//else
					//{
					//	dpc.Click();
					//	GameLogic.instance.ChangeCursorInGame(CursorType.CORRECT);
					//}
				}
			}
			
		}
		*/
	}
}