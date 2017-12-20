using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapGenSettings : EditorWindow
{
	static string mapWidthField;
	static string mapHeightField;
	static string waterSpawnChanceField;
	static string edgeCullingFactorField;
	static string mapSmoothingFactorField;

	static int mapWidth;
	static int mapHeight;
	static float waterSpawnChance;
	static int edgeCullingFactor;
	static int mapSmoothingFactor;

	[MenuItem ("Tools/Map Generator Settings")]
	public static void ShowEditorWindow ()
	{
		GetWindow<MapGenSettings> ("Map Generator Settings");
		SetValuesToDefault ();
	}
	
	//Sets values in the generator to those input here
	static void SetValues ()
	{
		int.TryParse (mapWidthField, out mapWidth);
		int.TryParse (mapHeightField, out mapHeight);
		float.TryParse (waterSpawnChanceField, out waterSpawnChance);
		int.TryParse (edgeCullingFactorField, out edgeCullingFactor);
		int.TryParse (mapSmoothingFactorField, out mapSmoothingFactor);

		Debug.Log (mapWidth + "/" + mapHeight + "/" + waterSpawnChance);

		Generator.width = mapWidth;
		Generator.height = mapHeight;
		Generator.waterChance = waterSpawnChance;
		Generator.edgeWaterFactor = edgeCullingFactor;
		Generator.smoothFactor = mapSmoothingFactor;
	}

	//Resets generator values to default
	static void SetValuesToDefault ()
	{
		Generator.width = 50;
		Generator.height = 50;
		Generator.waterChance = 0.5f;
		Generator.edgeWaterFactor = 5;
		Generator.smoothFactor = 5;

		mapWidthField = Generator.width.ToString ();
		mapHeightField = Generator.height.ToString ();
		waterSpawnChanceField = Generator.waterChance.ToString ();
		edgeCullingFactorField = Generator.edgeWaterFactor.ToString ();
		mapSmoothingFactorField = Generator.smoothFactor.ToString ();
	}

	//Draws GUI
	void OnGUI ()
	{
//		GUILayout.Label ("Generator Settings (SET WIDTH & HEIGHT BEFORE FIRST GENERATION)", EditorStyles.boldLabel);
//
//		mapWidthField = EditorGUILayout.TextField ("Map Width", mapWidthField);
//		mapHeightField = EditorGUILayout.TextField ("Map Height", mapHeightField);
//		waterSpawnChanceField = EditorGUILayout.TextField ("Water Spawn Chance", waterSpawnChanceField);
//		edgeCullingFactorField = EditorGUILayout.TextField ("Edge Culling Factor", edgeCullingFactorField);
//		mapSmoothingFactorField = EditorGUILayout.TextField ("Map Smoothing Factor", mapSmoothingFactorField);

//		if (GUILayout.Button ("Set Values"))
//		{
//			SetValues ();
//		}

//		if (GUILayout.Button ("Set Values To Default"))
//		{
//			SetValuesToDefault ();
//		}
	}
}
