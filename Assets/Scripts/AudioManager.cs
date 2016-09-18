using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	
	[SerializeField]
	private AudioSource loopCPU;
	[SerializeField]
	private AudioSource conversionFail;
	[SerializeField]
	private AudioSource conversionAccepted;
	[SerializeField]
	private AudioSource noise;
	[SerializeField]
	private AudioSource dangerAlarm;
	[SerializeField]
	private AudioSource typeText;
	[SerializeField]
	private AudioSource threatDetected;
	[SerializeField]
	private AudioSource guiFx;

	[Header("Clips GUI")]
	[SerializeField]
	private AudioClip mouseHover;
	[SerializeField]
	private AudioClip mouseClick;


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
		if (!typeText.isPlaying)
		{
			typeText.loop = true;
			typeText.Play();
		}
	}

	public void StopPlayTypeText()
	{
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

	public void PlayLoopCPU()
	{
		loopCPU.loop = true;
		loopCPU.Play();
	}

	#region GUI FX
	public void PlayMouseHover()
	{
		guiFx.PlayOneShot(mouseHover);
	}

	public void PlayMouseClick()
	{
		guiFx.PlayOneShot(mouseClick);
	}
	#endregion

}