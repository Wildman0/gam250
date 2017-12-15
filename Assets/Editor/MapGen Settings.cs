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
	}
	
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

	void OnGUI ()
	{
		GUILayout.Label ("Generator Settings", EditorStyles.boldLabel);

		mapWidthField = EditorGUILayout.TextField ("Map Width", mapWidthField);
		mapHeightField = EditorGUILayout.TextField ("Map Height", mapHeightField);
		waterSpawnChanceField = EditorGUILayout.TextField ("Water Spawn Chance", waterSpawnChanceField);
		edgeCullingFactorField = EditorGUILayout.TextField ("Edge Culling Factor", edgeCullingFactorField);
		mapSmoothingFactorField = EditorGUILayout.TextField ("Map Smoothing Factor", mapSmoothingFactorField);

		if (GUILayout.Button ("Set Values"))
		{
			SetValues ();
		}

		if (GUILayout.Button ("Set Values To Default"))
		{
			SetValuesToDefault ();
		}

		/*GUILayout.Label ("Template Generator", EditorStyles.boldLabel);

		//Casts the text field and allows the value to be shown on screen after being entered
		xLengthField = EditorGUILayout.TextField ("X Length", xLengthFieldValue.ToString ());
		yLengthField = EditorGUILayout.TextField ("Y Length", yLengthFieldValue.ToString ());

		//Parses the input into an int that is then used by the generator
		int.TryParse (xLengthField, out xLengthFieldValue);
		int.TryParse (yLengthField, out yLengthFieldValue);

		//When the generate button is pressed, it begins generating the map
		if (GUILayout.Button ("Generate Template"))
		{
			DestroyTemplate ();
			CreateColumn (xLengthFieldValue, yLengthFieldValue);
		}

		if (GUILayout.Button ("Destroy Template"))
		{
			DestroyTemplate ();
		}

		GUILayout.Space (20);

		GUILayout.Label ("Tile Painting Options", EditorStyles.boldLabel);

		if (GUILayout.Button ("Paint Tile"))
		{
			currentAction = CurrentAction.paintDefaultTile;
		}
		if (GUILayout.Button ("Add Base Tile"))
		{
			currentAction = CurrentAction.paintBaseTile;
		}
		if (GUILayout.Button ("Paint Faction"))
		{
			currentAction = CurrentAction.paintFaction;
		}*/
	}
}
