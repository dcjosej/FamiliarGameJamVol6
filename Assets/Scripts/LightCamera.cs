using UnityEngine;
using System.Collections;

public class LightCamera : MonoBehaviour {

	private Material lightOffMat;
	private MeshRenderer meshRenderer;

	void Start ()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		lightOffMat = meshRenderer.sharedMaterial;
	}
	
	public void TurnOn(Material lightOnMat)
	{
		meshRenderer.sharedMaterial = lightOnMat;
	}

	public void TurnOff()
	{
		meshRenderer.sharedMaterial = lightOffMat;
	}
}
