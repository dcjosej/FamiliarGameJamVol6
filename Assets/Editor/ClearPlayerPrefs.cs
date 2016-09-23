using UnityEditor;
using UnityEngine;


public class ClearEditorPrefs : ScriptableObject
{
	[MenuItem("Tools/DeletePlayerPrefs")]
    public static void DoDeselect()
	{
		Debug.Log("PLAYER PREFS BORRADOS!");
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}
}