using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

	public int width = 50;
	public int height = 50;

	public GameObject[,] tileGridGameObjects;
    public Tile[,] tileGrid;

	public float seed;

	public Vector2 perlinData;
	public float perlinNoise;

	public GameObject dirtTile;
	public GameObject waterTile;

	public float waterChance = 1f;
    public int edgeWaterFactor = 5;
    public int smoothFactor = 5;

    public Material[] terrainMaterials;

    int xIndexPos;
    int yIndexPos;

    //Generates a new seed and instantiates arrays
	void Start ()
	{
		if (seed == 0f)
		{
			GenerateSeed ();
		}

		tileGridGameObjects = new GameObject[width, height];
        tileGrid = new Tile[width, height];

		GenerateWorld ();
	}

    //For demonstration purposes
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			DestroyMap ();
			GenerateSeed ();
			GenerateWorld ();
            AddWaterAtEdges();
            GetTileArray();
            GroupLandTiles(smoothFactor);
		}
	}
    
    //Gets all of the tile components from the tiles and stores them in an array
	void GetTileArray ()
	{
        int xLength = tileGridGameObjects.GetLength(0);
        int yLength = tileGridGameObjects.GetLength(1);

        for (int x = 0; x < xLength; x++)
        {
            for (int y = 0; y < yLength; y++)
            {
                Tile tile = tileGridGameObjects[x, y].GetComponent<Tile>();
                tileGrid[tile.x, tile.y] = tile;
            }
        }
	}
    
    //Destroys the map
	void DestroyMap ()
	{
		foreach(GameObject go in tileGridGameObjects)
		{
			Destroy (go);
		}
	}

    //Generates a random seed
	void GenerateSeed ()
	{
        //seed = Random.Range (0f, 1f);
        seed = System.DateTime.Now.Millisecond;

    }

    //Will change tile types based on the type of tiles surrounding it
    private void GroupLandTiles(int smoothness)
    {
        int xLength = tileGridGameObjects.GetLength(0);
        int yLength = tileGridGameObjects.GetLength(1);

        for (int i = 0; i < smoothFactor; i++)
        {
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    if (GetNeighourTilesOfType(tileGrid[x, y], Tile.TileType.dirt) >= 3)
                    {
                        ChangeTileType(tileGrid[x, y], Tile.TileType.dirt);
                    }
                    else if (GetNeighourTilesOfType(tileGrid[x, y], Tile.TileType.water) == 4)
                    {
                        ChangeTileType(tileGrid[x, y], Tile.TileType.water);
                    }
                }
            }
        }
    }

    //Makes it more likely for water to spawn at the edges of the map in order to give a more rounded shape.
    void AddWaterAtEdges()
    {
        for (int i = 0; i < edgeWaterFactor; i++)
        {
            foreach (Tile tile in tileGrid)
            {
                float tileDistanceFromCenter = DistanceToCenterRatio(tile);

                if (Random.Range(0f, 1.2f) < tileDistanceFromCenter)
                {
                    ChangeTileType(tile, Tile.TileType.water);
                }
                else if (Random.Range(0f, 0.2f) < tileDistanceFromCenter)
                {
                    ChangeTileType(tile, Tile.TileType.dirt);
                }

                float tileDistanceFromCenterX = XDistanceToCenterRatio(tile);
                float tileDistanceFromCenterY = YDistanceToCenterRatio(tile);

                if (Random.Range (0f, 1.2f) < tileDistanceFromCenterX)
                {
                    ChangeTileType(tile, Tile.TileType.water);
                }
                else if (Random.Range(0f, 1.2f) < tileDistanceFromCenterY)
                {
                    ChangeTileType(tile, Tile.TileType.water);
                }
            }
        }
    }

    //Produces a ratio between 0 and 1 of how close this tile is to the center of the map
    float DistanceToCenterRatio (Tile tile)
    {
        Tile centerTile = tileGrid[Mathf.RoundToInt(width / 2), Mathf.RoundToInt(height / 2)];
        
        float distanceToCenterRatio;
        float distanceFromCenterX = tile.x - centerTile.x;
        float distanceFromCenterY = tile.y - centerTile.y;

        //Inverts negative values
        if (distanceFromCenterX < 0)
        {
            distanceFromCenterX = distanceFromCenterX * -1;
        }
        if (distanceFromCenterY < 0)
        {
            distanceFromCenterY = distanceFromCenterY * -1;
        }

        distanceToCenterRatio = (distanceFromCenterX + distanceFromCenterY) / ((width + height) / 2);
        return distanceToCenterRatio;
    }

    //Produces a ratio between 0 and 1 of how close this tile is to the center of the map on the x axis
    float XDistanceToCenterRatio(Tile tile)
    {
        Tile centerTile = tileGrid[Mathf.RoundToInt(width / 2), Mathf.RoundToInt(height / 2)];

        float distanceToCenterRatio;
        float distanceFromCenterX = tile.x - centerTile.x;

        //Inverts negative values
        if (distanceFromCenterX < 0)
        {
            distanceFromCenterX = distanceFromCenterX * -1;
        }

        distanceToCenterRatio = (distanceFromCenterX) / ((width) / 2);
        return distanceToCenterRatio;
    }

    //Produces a ratio between 0 and 1 of how close this tile is to the center of the map on the y axis
    float YDistanceToCenterRatio(Tile tile)
    {
        Tile centerTile = tileGrid[Mathf.RoundToInt(width / 2), Mathf.RoundToInt(height / 2)];

        float distanceToCenterRatio;
        float distanceFromCenterY = tile.y - centerTile.y;

        //Inverts negative values
        if (distanceFromCenterY < 0)
        {
            distanceFromCenterY = distanceFromCenterY * -1;
        }

        distanceToCenterRatio = (distanceFromCenterY) / ((height) / 2);
        return distanceToCenterRatio;
    }

    //Generates a perlin noise map and places tiles corresponding to it
    void GenerateWorld ()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				perlinNoise = Mathf.PerlinNoise (seed + (x * 2f), seed + (y * 2f));
				float thisWaterChance = waterChance;

				if (perlinNoise < thisWaterChance)
				{
                    GenerateTile(x, y, Tile.TileType.water);
                }
				else
				{
					GenerateTile(x, y, Tile.TileType.dirt);
                }
			}
		}
        
        SetNeighbourTiles();
	}

    //Generates a tile at the correct position and sets some of its data
    void GenerateTile(int x, int y, Tile.TileType tileType)
    {
        switch (tileType) {
            case Tile.TileType.water:
            tileGridGameObjects[x, y] = Instantiate(waterTile, new Vector2(x, y), Quaternion.identity);
                break;

            case Tile.TileType.dirt:
                tileGridGameObjects[x, y] = Instantiate(dirtTile, new Vector2(x, y), Quaternion.identity);
                break;
        }

        Tile tile = tileGridGameObjects[x, y].GetComponent<Tile>();
        tileGrid[x, y] = tile;

        tile.tileType = tileType;
        tile.x = x;
        tile.y = y;
    }

    //Changes a tiles properties and material to the desired
    void ChangeTileType(Tile tile, Tile.TileType tileType)
    {
        tile.tileType = tileType;

        switch (tileType)
        {
            case Tile.TileType.water:
                tile.gameObject.GetComponent<Renderer>().material = terrainMaterials[0];
                break;

            case Tile.TileType.dirt:
                tile.gameObject.GetComponent<Renderer>().material = terrainMaterials[1];
                break;
        }
    }

    //Sets the neighbour tiles array in all tiles
    void SetNeighbourTiles()
    {
        foreach (Tile tile in tileGrid)
        {
            tile.neighbourTiles = GetNeighourTiles(tile);
        }
    }

    //returns the numer of tiles of the type being checked for 
    int GetNeighourTilesOfType(Tile tile, Tile.TileType tileType)
    {
        int neighourTilesOfCorrectType = 0;

        foreach (Tile t in tile.neighbourTiles)
        {
            if (t.tileType == tileType)
            {
                neighourTilesOfCorrectType++;
            }
        }

        return neighourTilesOfCorrectType;
    }

    //Gets the neighbours of a tile, setting them in an array as well as in a single variable (for readability later down the line)
    Tile[] GetNeighourTiles(Tile tile)
    {
        List<Tile> neighbouringTilesList = new List<Tile>();

        foreach (Tile t in tileGrid)
        {
            if (neighbouringTilesList.Count <= 4)
            {
                if (t.x == tile.x && t.y == tile.y + 1)
                {
                    tile.neighbourTileUp = t;
                    neighbouringTilesList.Add(t);
                }

                else if (t.x == tile.x + 1 && t.y == tile.y)
                {
                    tile.neighbourTileLeft = t;
                    neighbouringTilesList.Add(t);
                }

                else if (t.x == tile.x && t.y == tile.y - 1)
                {
                    tile.neighbourTileDown = t;
                    neighbouringTilesList.Add(t);
                }

                else if (t.x == tile.x - 1 && t.y == tile.y)
                {
                    tile.neighbourTileRight = t;
                    neighbouringTilesList.Add(t);
                }
            }
        }

        Tile[] neighbouringTilesArray = neighbouringTilesList.ToArray();

        return neighbouringTilesArray;
    }
}