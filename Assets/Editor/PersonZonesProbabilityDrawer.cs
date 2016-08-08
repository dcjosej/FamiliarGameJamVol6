using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;

[CustomEditor(typeof(PersonZonesProbability))]
public class PersonZonesProbabilityDrawer : Editor
{
	private PersonZonesProbability p;
	private Dictionary<Zone, int> probabilities = new Dictionary<Zone, int>();
	private SerializedProperty sp;

	void OnEnable()
	{
		p = (PersonZonesProbability)target;

		foreach (Zone zone in Enum.GetValues(typeof(Zone)))
		{
			probabilities[zone] = p.GetZoneProbabilityInEditor(zone).probability;
		}

	}

	public override void OnInspectorGUI()
	{

		foreach (ZoneProbability zoneProbability in p.probabilities)
		{

			int diff = probabilities[zoneProbability.zone] - zoneProbability.probability;
			int diffToDistribute = Mathf.Abs(diff);
			int i = 0;

			if (diff != 0)
			{
				if (diff > 0)
				{
					Debug.Log("REDUCIENDO!: " + diff);
					Ensure100Percent(zoneProbability, diff, false);
				}
				else
				{
					Debug.Log("AUMENTANDO!: " + diff);
					Ensure100Percent(zoneProbability, diff, true);
				}

				probabilities[zoneProbability.zone] = zoneProbability.probability;
			}
			zoneProbability.probability = EditorGUILayout.IntSlider(zoneProbability.zone + " probability", zoneProbability.probability, 0, 100);
		}
	}

	private void Ensure100Percent(ZoneProbability current, int diff, bool increasing)
	{
		int i = 0;

		int countRemaining = increasing ? (p.probabilities.FindAll(x => x.probability > 0.0f).Count - 1) : (p.probabilities.Count - 1);
        int diffAbs = Mathf.Abs(diff);
		List<ZoneProbability> sorted = new List<ZoneProbability>();

		
		sorted.AddRange(p.probabilities); 
		sorted.Sort();

		if (increasing)
		{
			sorted.Reverse();
		}

		while (diffAbs > 0)
		{
			

			ZoneProbability zpAux = sorted[i];

			

			if(zpAux == current)
			{
				i++;
				continue;
			}

			int amount = Mathf.CeilToInt(diffAbs * 1.0f / countRemaining);
			zpAux.probability =  increasing ? zpAux.probability - amount: zpAux.probability + amount;
			zpAux.probability = Mathf.Clamp(zpAux.probability, 0, 100);
			probabilities[zpAux.zone] = zpAux.probability; 
			diffAbs -= amount;
			countRemaining--;

			i++;
		}
	}
}