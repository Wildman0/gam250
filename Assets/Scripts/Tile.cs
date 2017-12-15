using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data class for tiles with instance specific methods
/// </summary>

public class Tile : MonoBehaviour {

    public enum Type { dirt, water };
    public Type tileType;	//The type of tile this is
    public Tile[] neighbourTiles = new Tile[4];	//An array of this tile's neighbours
    public int x;
    public int y;	//Coordinates

    public Tile neighbourTileUp;
    public Tile neighbourTileDown;
    public Tile neighbourTileLeft;
    public Tile neighbourTileRight;	//Individual neighbour tiles

	public bool checkedForLandmass;	//Has this tile checked for the landmass it's a part of?
	public bool checkedNeighbours;	//Has this tile checked if it's neighbours are a part of the same landmass?

	//Changes the tiles type and material to the desired type
	public void ChangeTileType(Type type)
	{
		switch (type)
		{
			case Type.water:
				GetComponent<Renderer> ().material = Generator.terrainMaterials[0];
				break;

			case Type.dirt:
				GetComponent<Renderer> ().material = Generator.terrainMaterials[1];
				break;
		}

		tileType = type;
	}

	//Sets the neighbour tiles array
	public void SetNeighbourTiles ()
	{
		neighbourTiles = GetNeighourTiles ();
	}

	//Gets the neighbours of a tile, setting them in an array as well as in a single variable (for readability later down the line)
	public Tile[] GetNeighourTiles ()
	{
		List<Tile> neighbouringTilesList = new List<Tile> ();

		foreach (Tile t in Generator.tileGrid)
		{
			if (neighbouringTilesList.Count <= 4)
			{
				if (t.x == x && t.y == y + 1)
				{
					neighbourTileUp = t;
					neighbouringTilesList.Add (t);
				}

				else if (t.x == x + 1 && t.y == y)
				{
					neighbourTileLeft = t;
					neighbouringTilesList.Add (t);
				}

				else if (t.x == x && t.y == y - 1)
				{
					neighbourTileDown = t;
					neighbouringTilesList.Add (t);
				}

				else if (t.x == x - 1 && t.y == y)
				{
					neighbourTileRight = t;
					neighbouringTilesList.Add (t);
				}
			}
		}

		Tile[] neighbouringTilesArray = neighbouringTilesList.ToArray ();

		return neighbouringTilesArray;
	}

	//returns tiles of the type being checked for 
	public Tile[] GetNeighourTilesOfType (Type tileType)
	{
		List<Tile> tiles = new List<Tile>();

		foreach (Tile t in neighbourTiles)
		{
			if (t.tileType == tileType)
			{
				tiles.Add (t);
			}
		}

		return tiles.ToArray();
	}

	//returns the numer of tiles of the type being checked for 
	public int GetNeighourTilesOfTypeNumber (Type tileType)
	{
		int correctType = 0;

		foreach (Tile t in neighbourTiles)
		{
			if (t.tileType == tileType)
			{
				correctType++;
			}
		}

		return correctType;
	}

	//Produces a ratio between 0 and 1 of how close this tile is to the center of the map
	public float DistanceToCenterRatio ()
	{
		int width = Generator.width;
		int height = Generator.height;

		Tile centerTile = Generator.tileGrid[Mathf.RoundToInt (width / 2), Mathf.RoundToInt (height / 2)];
		
		float distanceFromCenterX = x - centerTile.x;
		float distanceFromCenterY = y - centerTile.y;

		//Inverts negative values
		if (distanceFromCenterX < 0)
		{
			distanceFromCenterX = distanceFromCenterX * -1;
		}
		if (distanceFromCenterY < 0)
		{
			distanceFromCenterY = distanceFromCenterY * -1;
		}
		
		return (distanceFromCenterX + distanceFromCenterY) / ((width + height) / 2);
	}

	//Produces a ratio between 0 and 1 of how close this tile is to the center of the map on the x axis
	public float XDistanceToCenterRatio ()
	{
		Tile centerTile = Generator.tileGrid[Mathf.RoundToInt (Generator.width / 2), Mathf.RoundToInt (Generator.height / 2)];
		
		float distanceFromCenterX = x - centerTile.x;

		//Inverts negative values
		if (distanceFromCenterX < 0)
		{
			distanceFromCenterX = distanceFromCenterX * -1;
		}
		
		return (distanceFromCenterX) / ((Generator.width) / 2);
	}

	//Produces a ratio between 0 and 1 of how close this tile is to the center of the map on the y axis
	public float YDistanceToCenterRatio ()
	{Tile centerTile = Generator.tileGrid[Mathf.RoundToInt (Generator.width / 2), Mathf.RoundToInt (Generator.height / 2)];
		
		float distanceFromCenterY = y - centerTile.y;

		//Inverts negative values
		if (distanceFromCenterY < 0)
		{
			distanceFromCenterY = distanceFromCenterY * -1;
		}
		
		return (distanceFromCenterY) / ((Generator.height) / 2);
	}
}