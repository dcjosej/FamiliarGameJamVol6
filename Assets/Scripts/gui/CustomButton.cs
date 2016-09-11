using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
	public ButtonsController buttonsController;
	public Color hoverColor;
	public Color normalColor = Color.white;
	public UnityEvent clickFunction;

	private Graphic graphic;

	public void Init()
	{
		graphic = GetComponent<Graphic>();
	}

	public void Select()
	{
		graphic.color = hoverColor;
	}

	public void Deselect()
	{
		graphic.color = normalColor;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		buttonsController.SelectButton(this, eventData);
		graphic.color = hoverColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{}

	public void OnPointerClick(PointerEventData eventData)
	{
		clickFunction.Invoke();
	}

	public void ExectueFunction()
	{
		clickFunction.Invoke();
	}
}