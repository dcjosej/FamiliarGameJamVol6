using UnityEngine;
using System.Collections;

public class MainCameraRTInputReceiver : MonoBehaviour
{

	public Camera cameraRT;


	void OnMouseDown()
	{
		Debug.Log("Pinchando en la pantalla!");
		RayRenderTexture();
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

			//Debug.Log("AAAAAGGH!!! colisionando con cosas!!!");
			/*
			ray = secondCamera.ViewportPointToRay(new Vector3(hit.textureCoord));
			if (Physics.Raycast(ray, hit)		
			{
				// hit should now contain information about what was hit through secondCamera
			}
			*/
		}
	}
}