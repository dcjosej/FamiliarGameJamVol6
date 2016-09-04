using UnityEngine;
using System.Collections;

public class AnimationSkyline2 : MonoBehaviour {

	public float velocity;
	private Vector3 velocityVector;
	private Vector3 respawnPosition;


	void Start()
	{
		velocityVector = new Vector3(0f, 0f, -velocity);
		respawnPosition = new Vector3(0f, 0f, 609);
	}

	void Update ()
	{
		transform.localPosition += velocityVector * Time.deltaTime;

		if(transform.localPosition.z <= -259)
		{
			transform.localPosition = respawnPosition;
		}
	}
}
