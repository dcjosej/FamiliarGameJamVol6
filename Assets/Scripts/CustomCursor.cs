using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CustomCursor : MonoBehaviour {

	private int cursorWidth = 64;
	private int cursorHeight = 64;
	private Vector3 cursorPosition = new Vector3();

	public Camera cursorCamera;

	void Update()
	{
		cursorPosition.Set(Input.mousePosition.x, Input.mousePosition.y, cursorCamera.nearClipPlane);
        transform.position = cursorCamera.ScreenToWorldPoint(cursorPosition);
	}
	
	void UpdatePosition(Vector3 worldPosition)
	{
		transform.position = worldPosition;
    }
}
