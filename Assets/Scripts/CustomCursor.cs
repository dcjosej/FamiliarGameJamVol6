using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CustomCursor : MonoBehaviour {

	private int cursorWidth = 64;
	private int cursorHeight = 64;

	public Texture2D cursorImage;

	void Update()
	{
		transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    }
}
