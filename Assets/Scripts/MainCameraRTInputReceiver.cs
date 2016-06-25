using UnityEngine;
using System.Collections;

public class MainCameraRTInputReceiver : MonoBehaviour
{
	public Camera cameraRT;
	public MonitorNumber currentMonitorNumber;


	//public Texture2D cursorTexture;
	//private CursorMode cursorMode = CursorMode.Auto;
	//private Vector2 hotSpot = Vector2.one * 0.5f;

	void OnMouseDown()
	{
		Debug.Log("Pinchando en la pantalla!");
		RayRenderTexture();
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


	void OnMouseExit()
	{
		GameLogic.instance.UpdateCursor(CursorType.OUTSIDE_SCREEN);
	}

	public void SetCamera(Camera camera)
	{
		cameraRT = camera;
	}

	private void RayRenderTexture()
	{


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
					DifferentPersonController dpc = hit.collider.GetComponent<DifferentPersonController>();

					if (pc.enabled)
					{
						pc.Click();
					}
					else
					{
						dpc.Click();
					}
				}
			}
		}
	}
}