using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data class for landmasses
/// </summary>

public class Landmass {

	private List<Tile> tiles = new List<Tile> ();	//List of tiles in this landmass. Remains private to prevent interference

	//Constructor which takes an array of tiles to start off
	public Landmass (Tile[] initTiles = null)
	{
		if (initTiles != null)
		{
			tiles.AddRange (initTiles);
		}
	}

	//Adds a tile to the list of tiles on the landmass
	public void AddTile (Tile tile)
	{
		tiles.Add (tile);
	}

	//Returns the number of tiles in the landmass
	public int GetLandMassSize ()
	{
		return tiles.Count;
	}

	//Returns an array of tiles in the landmass
	public Tile[] GetTiles ()
	{
		return tiles.ToArray ();
	}
}