using UnityEngine;
using System.Collections;

public class AnimationSkyline : MonoBehaviour {

	public float velocity;
	private Vector3 velocityVector;
	private Vector3 respawnPosition;

	public Transform skyline1;
	public Transform skyline2;

	private Transform fontSkyline;

	void Start()
	{
		velocityVector = new Vector3(0f, 0f, -velocity);
		respawnPosition = new Vector3(0f, 0f, 330);
		fontSkyline = skyline1;
	}

	void Update ()
	{
		skyline1.localPosition += velocityVector * Time.deltaTime;
		skyline2.localPosition += velocityVector * Time.deltaTime;

		if (fontSkyline.localPosition.z <= -886)
		{
			if(fontSkyline.gameObject.name == "Skyline1")
			{
				skyline1.localPosition = respawnPosition;
				fontSkyline = skyline2;
			}
			else
			{
				skyline2.localPosition = respawnPosition;
				fontSkyline = skyline1;
			}
		}
	}
}
