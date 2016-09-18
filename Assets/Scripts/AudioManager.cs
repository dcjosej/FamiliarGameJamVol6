using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	
	[SerializeField]private AudioSource loopCPU;
	[SerializeField]private AudioSource conversionFail;
	[SerializeField]private AudioSource conversionAccepted;
	[SerializeField]private AudioSource noise;
	[SerializeField]private AudioSource dangerAlarm;
	[SerializeField]private AudioSource typeText;
	[SerializeField]private AudioSource threatDetected;


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

	public void PlayTypeText()
	{
		if (!dangerAlarm.isPlaying)
		{
			StartCoroutine(PlayTypeTextIE());
		}
	}

	private IEnumerator PlayTypeTextIE()
	{

		typeText.loop = true;
		typeText.Play();
		while (HUDController.instance.consoleAutoText.writing)
		{
			yield return null;
		}

		typeText.loop = false;
	}


	public void PlayDangerAlarm()
	{
		if (!dangerAlarm.isPlaying)
		{
			StartCoroutine(PlayDangerAlarmIE());
		}
	}

	private IEnumerator PlayDangerAlarmIE()
	{
		dangerAlarm.loop = true;
		dangerAlarm.Play();
		yield return new WaitForSeconds(HUDController.instance.timeAnimationDanger / 2);
		dangerAlarm.loop = false;
	}

	public void PlayThreatDetected()
	{
		threatDetected.Stop();
		threatDetected.Play();
	}

}