using System;
using UnityEngine;
using UnityEngine.UI;

public class SectorSliderController : MonoBehaviour
{
	private Slider slider;

	public Image fillImage;
	public Color safeColor;
	public Color dangerColor;
	
	void Awake()
	{
		slider = GetComponent<Slider>();
	}

	public float GetValueThreatness()
	{
		return slider.value;
	}

	void Update()
	{
		//slider.value = slider.maxValue - GameLogic.instance.GetSliderValue();
		//slider. = Color.Lerp(safeColor, dangerColor, slider.value / slider.maxValue);

		slider.value = GameLogic.instance.GetSliderValue();
		fillImage.color = Color.Lerp(safeColor, dangerColor, slider.value / slider.maxValue);
	}

	public void ApplyReward(float reward)
	{
		slider.value += reward;
	}
	public float GetMaxValue()
	{
		float res = slider.maxValue;
		return res;
	}

	/*
	public void IncreaseValue()
	{
		throw new NotImplementedException();
	}
	*/
}