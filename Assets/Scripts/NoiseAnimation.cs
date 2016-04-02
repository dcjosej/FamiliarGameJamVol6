using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NoiseAnimation : MonoBehaviour
{
	public Sprite[] sprites;

	private Image image;

	void Awake()
	{
		image = GetComponent<Image>();
	}

	void OnEnable()
	{
		StartCoroutine(Animate());
	}

	private IEnumerator Animate()
	{
		while (true)
		{
			for (int i = 0; i < sprites.Length; i++)
			{
				image.sprite = sprites[i];
				yield return new WaitForSeconds(0.03f);
			}
		}
	}
}
