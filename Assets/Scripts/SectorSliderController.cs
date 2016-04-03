using UnityEngine;
using UnityEngine.UI;

public class SectorSliderController : MonoBehaviour
{
	public float velocityLooseSector = 0.001f;
	public float incrementThreatRestored = 0.01f;

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
		
		slider.value -= velocityLooseSector * GameLogic.instance.charactersInScene * Time.deltaTime;
	}

	public void IncreaseValue()
	{
		slider.value += incrementThreatRestored;
	}
}
