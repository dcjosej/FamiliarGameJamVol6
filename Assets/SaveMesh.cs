using UnityEngine;
using System.Collections;
using UnityEditor;

public class SaveMesh : MonoBehaviour {

	[ContextMenu("SaveMesh")]
	private void SaveMeshAsset()
	{
		AssetDatabase.CreateAsset(GetComponent<SkinnedMeshRenderer>().sharedMesh, "Assets/MALLA_POTENTE.asset");
	}
}
