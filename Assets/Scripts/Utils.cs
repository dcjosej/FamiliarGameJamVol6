using UnityEngine;
using System.Collections;

public static class Utils
{
	public static string OrangeColor = "FFA300";
	public static string RedColor = "FF0000";
	public static string GreenColor = "42C920FF";

	public static string SecondsTozzHHmmss(float seconds)
	{
		string res = "";

		float zz = Mathf.Floor((seconds * 100) % 100);
		float ss = Mathf.Floor(seconds % 60);
		float mm = Mathf.Floor(seconds / 60);
		float hh = Mathf.Floor(mm / 60);

		res = string.Format("{0:00}:{1:00}:{2:00}:{3:00}", hh, mm, ss, zz);
		return res;
	}


	#region COROUTINES
	public static IEnumerator WaitForRealSeconds(float time)
	{
		float start = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < start + time)
		{
			yield return null;
		}
	}
	
	#endregion




}