using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class ToolsWindow : EditorWindow
{

	public List<GameObject> list;
	public GameObject[] oldPeople;
	public Object newPersonPrefab;
	public Transform parentPeople;

	[MenuItem("Tools/Replace People")]
	public static void ShowWindow()
	{
		ToolsWindow window = GetWindow<ToolsWindow>();
		window.Show();
	}

	void OnInspectorUpdate()
	{
		Repaint();
	}

	void OnGUI()
	{
		
		SerializedObject serializedObject = new SerializedObject(this);

		SerializedProperty oldPeopleProperty = serializedObject.FindProperty("oldPeople");
		SerializedProperty newPersonPrefabProperty = serializedObject.FindProperty("newPersonPrefab");
		SerializedProperty parentPeopleProperty = serializedObject.FindProperty("parentPeople");

		EditorGUILayout.PropertyField(oldPeopleProperty, true);
		EditorGUILayout.PropertyField(newPersonPrefabProperty, true);
		EditorGUILayout.PropertyField(parentPeopleProperty, true);

		//EditorGUILayout.ObjectField((object)list, typeof(GameObject[]));
		//EditorGUILayout.

		if (GUILayout.Button("Replace All"))
		{
			foreach(GameObject go in oldPeople)
			{
				GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(newPersonPrefab);

				instance.transform.position = go.transform.position;
				instance.transform.rotation = go.transform.rotation;
				instance.transform.localScale = go.transform.localScale;
			}
		}

		serializedObject.ApplyModifiedProperties();

	}
}