using UnityEngine;
using UnityEngine.UI;

public class SectorSliderController : MonoBehaviour
{
	public float velocityLooseSector = 0.0025f;
	public float incrementThreatRestored = 0.04f;
	public const int MAX_NUM_INFECTED = 20;
	public AnimationCurve threatnessCurve;

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
		float curveValue = Mathf.InverseLerp(0, MAX_NUM_INFECTED, Mathf.Clamp(GameLogic.instance.charactersInScene, 0, MAX_NUM_INFECTED));
		float sliderValue = threatnessCurve.Evaluate(curveValue);
		slider.value -= velocityLooseSector * sliderValue * Time.deltaTime;
		//slider.value = 1 - sliderValue;
	}

	public void IncreaseValue()
	{
		slider.value += incrementThreatRestored;
	}
}
