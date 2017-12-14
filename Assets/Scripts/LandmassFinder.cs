using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LandmassFinder : MonoBehaviour {

	List<Landmass> landmasses = new List<Landmass>();
	public static Landmass landmass;

	//Creates a new landmass and flood fills to work out all containing tiles
	public static void FloodFillLandmass(Tile startingTile)
	{
		landmass = new Landmass (FindTilesInLandmass (startingTile));
		
		foreach (Tile tile in Generator.tileGrid)
		{
			if (!landmass.GetTiles ().Contains(tile))
			{
				tile.ChangeTileType (Tile.Type.water);
			}
		}
	}

	//NOTES: ONLY RUN AFTER AN IF CHECK TO MAKE SURE THE STARTING TILE IS LAND
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
			
			Tile[] neighbouringLandTiles = Generator.GetNeighourTilesOfType (tilesToCheck.Dequeue (), Tile.Type.dirt);

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
