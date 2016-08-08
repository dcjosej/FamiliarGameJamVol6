using UnityEngine;
using System.Collections;
using System;

public enum Zone
{
	HEAD, SHIRT, BLAZER, PANTS, SHOES, SUITCASE, GAFAS, HEAD_COMPLEMENTS
}

[System.Serializable]
public class ZoneProbability: IComparable<ZoneProbability>
{
	public Zone zone;

	[Range(0, 100)]
	public int probability;

	public ZoneProbability(Zone zone)
	{
		this.zone = zone;
	}

	public int CompareTo(ZoneProbability other)
	{
		int res = 0;
		if(probability.CompareTo(other.probability) != 0)
		{
			res = probability.CompareTo(other.probability);
        }

		return res;
	}
}