using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioSource loopCPU;
	public AudioSource conversionFail;
	public AudioSource conversionAccepted;
	public AudioSource noise;

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

	public void PlayNoise()
	{
		loopCPU.Stop();

		noise.loop = true;
		noise.Play();
	}
	
}
