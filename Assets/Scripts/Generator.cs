using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main map generator class
/// </summary>

public class Generator : MonoBehaviour {

	public static int width = 50;	//Map width
	public static int height = 50;	//Map height

	public GameObject[,] tileGridGameObjects;	//An array of gameObjects in the map
    public static Tile[,] tileGrid;	//An array of the tile components stored on the above gameObjects

	public float seed;	//Random seed for the perlin noise	

	public Vector2 perlinData;
	public float perlinNoise;	//Store the perlin noise map and individual points on the map

	public GameObject dirtTile;	//Dirt tile prefab
	public GameObject waterTile;	//Water tile prefab

	public static float waterChance = 0.5f;	//How difficult it is for water to spawn, default 0.5 (Range 0-1)
    public static int edgeWaterFactor = 5;	//Number of passes made for culling land tiles around the outside edges of the map (Def. 5)
    public static int smoothFactor = 5;	//Number of passes made for smoothing out the map (Def. 5)

	public Material[] _terrainMaterials;
	public static Material[] terrainMaterials;	//Instanced and static versions of the materials for the terrain

	//Sets terrain materials based on inspector values
	void Awake ()
	{
		terrainMaterials = _terrainMaterials;
	}

    //Generates a new seed and instantiates arrays
	void Start ()
	{
		//Generates new seed if it's null
		if (seed == 0f)
		{
			GenerateSeed ();
		}
	}
	
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			GenerateMap ();	
		}
	}
    
	//Runs all of the methods used to generate a map in the correct order
	void GenerateMap ()
	{
		Debug.Log (width + "/" + height + "/" + waterChance + "/" + edgeWaterFactor + "/" + smoothFactor);
		CreateArrays ();
		GenerateSeed ();
		GenerateWorld ();
		AddWaterAtEdges ();
		FillTileArray ();
		GroupLandTiles (smoothFactor);
		CreateLandmass ();
	}

	//Creates new arrays to store gameObject/tiledata in
	void CreateArrays ()
	{
		if (tileGrid == null)
		{
			tileGridGameObjects = new GameObject[width, height];
			tileGrid = new Tile[width, height];
		}
	}

	//Creates the main landmass and culls other landmasses not in close proximity in order to prevent random noise
	void CreateLandmass ()
	{
		LandmassFinder.FloodFillLandmass (tileGrid[width/2, height/2]);
	}

    //Gets all of the tile components from the tiles and stores them in an array
	void FillTileArray ()
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
    
    //Generates a random seed
	void GenerateSeed ()
	{
        seed = System.DateTime.Now.Millisecond;

    }

    //Will change tile types based on the type of tiles surrounding it
    private void GroupLandTiles(int smoothness)
    {
        int xLength = tileGridGameObjects.GetLength(0);
        int yLength = tileGridGameObjects.GetLength(1);

		//Culls tiles based on their surrounding tiles, changing them to similar tiles
        for (int i = 0; i < smoothFactor; i++)
        {
            for (int x = 0; x < xLength; x++)
            {
                for (int y = 0; y < yLength; y++)
                {
                    if (tileGrid[x, y].GetNeighourTilesOfTypeNumber (Tile.Type.dirt) >= 3)
                    {
						tileGrid[x, y].ChangeTileType(Tile.Type.dirt);
                    }
                    else if (tileGrid[x, y].GetNeighourTilesOfTypeNumber (Tile.Type.water) == 4)
                    {
						tileGrid[x, y].ChangeTileType(Tile.Type.water);
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
                float tileDistanceFromCenter = tile.DistanceToCenterRatio();

                if (Random.Range(0f, 1.2f) < tileDistanceFromCenter)
                {
                    tile.ChangeTileType(Tile.Type.water);
                }
                else if (Random.Range(0f, 0.2f) < tileDistanceFromCenter)
                {
                    tile.ChangeTileType(Tile.Type.dirt);
                }

                float tileDistanceFromCenterX = tile.XDistanceToCenterRatio ();
                float tileDistanceFromCenterY = tile.YDistanceToCenterRatio ();

                if (Random.Range (0f, 1.2f) < tileDistanceFromCenterX)
                {
                    tile.ChangeTileType(Tile.Type.water);
                }
                else if (Random.Range(0f, 1.2f) < tileDistanceFromCenterY)
                {
                    tile.ChangeTileType(Tile.Type.water);
                }
            }
        }
    }
	
    //Generates a perlin noise map and places tiles corresponding to it
    void GenerateWorld ()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				perlinNoise = Mathf.PerlinNoise (seed + (x * 2f), seed + (y * 2f));

				if (perlinNoise < waterChance)
				{
                    GenerateTile(x, y, Tile.Type.water);
                }
				else
				{
					GenerateTile(x, y, Tile.Type.dirt);
                }
			}
		}
        
        SetNeighbourTiles();
	}

    //Generates a tile at the correct position and sets some of its data
    void GenerateTile(int x, int y, Tile.Type tileType)
    {
		if (tileGridGameObjects[x, y] == null)
		{
			switch (tileType)
			{
				case Tile.Type.water:
					tileGridGameObjects[x, y] = Instantiate (waterTile, new Vector2 (x, y), Quaternion.identity);
					break;

				case Tile.Type.dirt:
					tileGridGameObjects[x, y] = Instantiate (dirtTile, new Vector2 (x, y), Quaternion.identity);
					break;
			}

			Tile tile = tileGridGameObjects[x, y].GetComponent<Tile> ();
			tileGrid[x, y] = tile;

			tile.tileType = tileType;
			tile.x = x;
			tile.y = y;
		}
		else
		{
			//Changes tile data instead of destroying and reinstantiating
			if (tileGrid[x, y].tileType != tileType)
			{
				tileGrid[x, y].ChangeTileType (tileType);
			}
		}
    }
	
	//Calls the SetNeighbourTiles method on each tile
    void SetNeighbourTiles()
    {
        foreach (Tile tile in tileGrid)
        {
            tile.SetNeighbourTiles();
        }
    }
}