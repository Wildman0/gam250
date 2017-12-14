using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public enum Type { dirt, water };
    public Type tileType;
    public Tile[] neighbourTiles = new Tile[4];
    public int x;
    public int y;

    public Tile neighbourTileUp;
    public Tile neighbourTileDown;
    public Tile neighbourTileLeft;
    public Tile neighbourTileRight;

	public bool checkedForLandmass;
	public bool checkedNeighbours;

	public void ChangeTileType(Type type)
	{
		switch (type)
		{
			case Type.water:
				gameObject.GetComponent<Renderer> ().material = Generator.terrainMaterials[0];
				break;

			case Type.dirt:
				gameObject.GetComponent<Renderer> ().material = Generator.terrainMaterials[1];
				break;
		}
	}

	//Sets the neighbour tiles array in all tiles
	public void SetNeighbourTiles ()
	{
		neighbourTiles = GetNeighourTiles ();
	}

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

	//returns the numer of tiles of the type being checked for 
	public int GetNeighourTilesOfType (Type tileType)
	{
		int neighourTilesOfCorrectType = 0;

		foreach (Tile t in neighbourTiles)
		{
			if (t.tileType == tileType)
			{
				neighourTilesOfCorrectType++;
			}
		}

		return neighourTilesOfCorrectType;
	}
}
