using UnityEngine;
using System.Collections;
using UnityEditor;

public class SetUpPeople : EditorWindow
{

	private string meshOriginalPath = "Assets/People/personMesh.asset";
	private string baseNameDuplicatedMesh = "Assets/People/personMesh";
	private string extension = ".asset";


	public Transform[] people;

	[MenuItem("Tools/SetUpPeople")]
	public static void Init()
	{
		SetUpPeople window = (SetUpPeople)EditorWindow.GetWindow(typeof(SetUpPeople));
		window.Show();
	}

	private void OnGUI()
	{
		ScriptableObject target = this;
		SerializedObject so = new SerializedObject(target);


		SerializedProperty peopleProperty = so.FindProperty("people");



		EditorGUILayout.PropertyField(peopleProperty, true);

		//EditorGUILayout.FloatField()
		if(GUILayout.Button("Generate meshes"))
		{
			GenerateMeshes();	
		}

		so.ApplyModifiedProperties();
	}

	private void GenerateMeshes()
	{
		for(int i = 0; i < people.Length; i++)
		{
			string newPath = baseNameDuplicatedMesh + i + extension;
			AssetDatabase.CopyAsset(meshOriginalPath, newPath);
		}
	}
}