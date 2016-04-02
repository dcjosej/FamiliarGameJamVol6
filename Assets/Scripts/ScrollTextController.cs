using UnityEngine;
using UnityEngine.UI;

public class ScrollTextController : MonoBehaviour {

	private ScrollRect scrollRect;

	void Awake()
	{
		scrollRect = GetComponent<ScrollRect>();
	}

	void Update()
	{
		scrollRect.verticalNormalizedPosition = 0.5f;
	}

}
