﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class MB_ChangePerson : MonoBehaviour {
    public MB3_MeshBaker meshBaker;

	public Renderer[] defaultAppearence;

    //public Renderer[] clothingAndBodyPartsBareTorso;
    //public Renderer[] clothingAndBodyPartsBareTorsoDamagedArm;
    //public Renderer[] clothingAndBodyPartsHoodie;

    // Update is called once per frame

	void Start()
	{
		SetDefaultAppearence();
	}

	private void SetDefaultAppearence()
	{
		ChangeOutfit(defaultAppearence);
	}

	//void OnGUI () {
	//    if (GUILayout.Button("Wear Hoodie"))
	//    {
	//        ChangeOutfit(clothingAndBodyPartsHoodie);
	//    }
	//    if (GUILayout.Button("Bare Torso"))
	//    {
	//        ChangeOutfit(clothingAndBodyPartsBareTorso);
	//    }
	//    if (GUILayout.Button("Damaged Arm"))
	//    {
	//        ChangeOutfit(clothingAndBodyPartsBareTorsoDamagedArm);
	//    }
	//}

	void ChangeOutfit(Renderer[] outfit)
	{
		//collect the meshes we will be removing
		List<GameObject> objectsWeAreRemoving = new List<GameObject>();
		foreach (GameObject item in meshBaker.meshCombiner.GetObjectsInCombined())
		{
			Renderer r = item.GetComponent<Renderer>();
			bool foundInOutfit = false;
			for (int i = 0; i < outfit.Length; i++)
			{
				if (r == outfit[i])
				{
					foundInOutfit = true;
					break;
				}
			}
			if (!foundInOutfit)
			{
				objectsWeAreRemoving.Add(r.gameObject);
				Debug.Log("Removing " + r.gameObject);
			}
		}

		//Now collect the meshes we will be adding
		List<GameObject> objectsWeAreAdding = new List<GameObject>();
		for (int i = 0; i < outfit.Length; i++)
		{
			if (!meshBaker.meshCombiner.GetObjectsInCombined().Contains(outfit[i].gameObject))
			{
				objectsWeAreAdding.Add(outfit[i].gameObject);
				Debug.Log("Adding " + outfit[i].gameObject);
			}
		}

		meshBaker.AddDeleteGameObjects(objectsWeAreAdding.ToArray(), objectsWeAreRemoving.ToArray(), true);
		meshBaker.Apply();
	}
}
