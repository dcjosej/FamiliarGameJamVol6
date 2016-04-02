using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioSource conversionFail;
	public AudioSource conversionAccepted;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	public void PlayConversionFail()
	{
		conversionFail.Play();
	}

	public void PlayConversionAccepted()
	{
		conversionAccepted.Play();
	}
	
}
