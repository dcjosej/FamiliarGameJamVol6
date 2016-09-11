using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class FadeAnimation : MonoBehaviour {

	public float fadeMin;
	public float fadeMax;
	public float animationTime;

	private CanvasGroup canvasGroup;
	

	void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		StartCoroutine(AnimationFadeIE());
	}

	private IEnumerator AnimationFadeIE()
	{

		float time = 0f;
		float t = 0;

		float alphaSrc = fadeMin;
		float alphaDst = fadeMax;

		while (true)
		{
			time += Time.deltaTime;
			t = time / animationTime;

			canvasGroup.alpha = Mathf.Lerp(alphaSrc, alphaDst, t);


			if(t >= 1)
			{
				float aux = alphaSrc;
				alphaSrc = alphaDst;
				alphaDst = aux;

				t = 0;
				time = 0;
			}

			yield return null;
		}
	}

}
