using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SectorSliderController : MonoBehaviour
{
	private float velocityLooseSector = 0.01f;

	private Slider slider;
	
	void Awake()
	{
		slider = GetComponent<Slider>();
	}

	void Update()
	{
		
		slider.value -= velocityLooseSector * GameLogic.instance.charactersInScene * Time.deltaTime;
	}
}
