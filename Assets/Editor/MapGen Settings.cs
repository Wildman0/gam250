using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapGenSettings : EditorWindow
{
	[MenuItem ("Tools/Map Generator Settings")]
	public static void ShowEditorWindow ()
	{
		GetWindow<MapGenSettings> ("Map Generator Settings");
	}

	void OnGUI ()
	{
		GUILayout.Label ("Generator Settings", EditorStyles.boldLabel);

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
