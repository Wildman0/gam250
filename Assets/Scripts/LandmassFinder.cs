using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finds the main landmass and destroys all others
/// </summary>

public class LandmassFinder : MonoBehaviour {
	
	public static Landmass landmass;	//Instance of landmass to be used to store the main landmass

	//Creates a new landmass and flood fills to work out all containing tiles
	public static void FloodFillLandmass(Tile startingTile)
	{
		ClearPreviousValues ();
		landmass = new Landmass (FindTilesInLandmass (startingTile));

		//Changes all tiles that aren't in the central or closer surrounding landmasses to water
		foreach (Tile tile in Generator.tileGrid)
		{
			if (!landmass.GetTiles ().Contains(tile))
			{
				tile.ChangeTileType (Tile.Type.water);
			}
		}
	}

	static void ClearPreviousValues ()
	{
		foreach (Tile tile in Generator.tileGrid)
		{
			tile.checkedForLandmass = false;
			tile.checkedNeighbours = false;
		}
	}

	//Flood fills the landmass and returns an array of tiles on that landmass
	public static Tile[] FindTilesInLandmass(Tile startingTile)
	{
		Queue<Tile> tilesToCheck = new Queue<Tile> ();
		List<Tile> tilesInLandmass = new List<Tile> ();

		tilesInLandmass.Add (startingTile);
		tilesToCheck.Enqueue (startingTile);

		for (int i = 0; i < 1; i++)
		{
			//Adds the current tile to the array (otherwise it's left out)
			Tile t = tilesToCheck.Peek ();
			if (t.tileType == Tile.Type.dirt)
			{
				if (!tilesInLandmass.Contains (t))
				{
					tilesInLandmass.Add (t);
				}

				t.checkedForLandmass = true;
				t.checkedNeighbours = true;
			}
			
			Tile[] neighbouringLandTiles = tilesToCheck.Dequeue ().GetNeighourTilesOfType (Tile.Type.dirt);

			foreach (Tile tile in neighbouringLandTiles)
			{
				if (tile.checkedForLandmass == false)
				{
					if (!tilesInLandmass.Contains (tile))
					{
						tilesInLandmass.Add (tile);
					}
					tile.checkedForLandmass = true;
				}

				if (tile.checkedNeighbours == false)
				{
					foreach (Tile tile2 in tile.neighbourTiles)
					{
						tilesToCheck.Enqueue (tile2);
						tile.checkedNeighbours = true;
					}
				}
			}

			if (tilesToCheck.Count != 0)
			{
				i--;
			}
		}

		return tilesInLandmass.ToArray ();
	}
}
