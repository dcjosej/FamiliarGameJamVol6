using System;
using UnityEngine;
using UnityEngine.UI;

public class SectorSliderController : MonoBehaviour
{
	private Slider slider;
	
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
		slider.value = slider.maxValue - GameLogic.instance.GetSliderValue();
	}

	public void ApplyReward(float reward)
	{
		slider.value += reward;
	}

	/*
	public void IncreaseValue()
	{
		throw new NotImplementedException();
	}
	*/
}