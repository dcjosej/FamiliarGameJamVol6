using UnityEngine;
using System.Collections;

public class SpawnZoneController : MonoBehaviour {

	public Transform[] spawnPoints;

	public Transform GetRandomSpawnPoint()
	{
		Transform res = null;

		res = spawnPoints[Random.Range(0, spawnPoints.Length)];

		return res;
	}
}
