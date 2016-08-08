using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PersonZonesProbability : MonoBehaviour
{
	//[Range(0.0f, 1.0f)]
	//public float headProbability;

	public List<ZoneProbability> probabilities;
	private Dictionary<Zone, ZoneProbability> probabilitiesDict;


	void Awake()
	{
		probabilitiesDict = new Dictionary<Zone, ZoneProbability>();
		foreach (ZoneProbability zoneProbability in probabilities)
		{
			if (!probabilitiesDict.ContainsKey(zoneProbability.zone))
			{
				probabilitiesDict[zoneProbability.zone] = zoneProbability;
			}
		}
	}

	public ZoneProbability GetZoneProbabilityByZone(Zone zone)
	{
		ZoneProbability res = null;

		res = probabilitiesDict[zone];

		return res; 
	}

#if UNITY_EDITOR
	void Reset()
	{
		probabilities = new List<ZoneProbability>();
		foreach (Zone zone in Enum.GetValues(typeof(Zone)))
		{
			ZoneProbability newZoneProbability = new ZoneProbability(zone);
			probabilities.Add(newZoneProbability);
		}

		probabilities[0].probability = 100;
	}

	public ZoneProbability GetZoneProbabilityInEditor(Zone zone)
	{
		ZoneProbability res = null;
		foreach(ZoneProbability zp in probabilities)
		{
			if(zp.zone == zone)
			{
				res = zp;
				break;
			}
		}

		return res;
	}

#endif
}