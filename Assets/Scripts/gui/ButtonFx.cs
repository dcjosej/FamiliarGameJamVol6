using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class ButtonFx : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
	{
		AudioManager.instance.PlayMouseClick();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		AudioManager.instance.PlayMouseHover();
	}
}