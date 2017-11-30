using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

	public int width = 50;
	public int height = 50;

	public static GameObject[,] tileGridGameObjects;
    public static Tile[,] tileGrid;

	public float seed;

	public Vector2 perlinData;
	public float perlinNoise;

	public GameObject dirtTile;
	public GameObject waterTile;

	public float waterChance = 1f;

    int xIndexPos;
    int yIndexPos;

	void Start ()
	{
		if (seed == 0f)
		{
			GenerateSeed ();
		}

		tileGridGameObjects = new GameObject[width, height];

		GenerateWorld ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			DestroyMap ();
			GenerateSeed ();
			GenerateWorld ();
            GetTileArray();
            GroupLandTiles();
		}
	}

	void GetTileArray ()
	{
        foreach(GameObject go in tileGridGameObjects)
        {
            Tile tile = go.GetComponent<Tile>();
            tileGrid[tile.x, tile.y] = tile;
        }
	}

	void DestroyMap ()
	{
		foreach(GameObject go in tileGridGameObjects)
		{
			Destroy (go);
		}
	}

	void GenerateSeed ()
	{
		seed = Random.Range (0f, 1f);
	}

    //NOTE: USE -1 IN ARRAYS
    private void GroupLandTiles()
    {
        for (int x = 0; x < tileGridGameObjects.GetLength(0); x++)
        {
            for (int y = 0; y < tileGridGameObjects.GetLength(1); y++)
            {
                //Sets the index positions in the array correctly, as GetLength will get the length, rather than last index. Increases readability
                xIndexPos = x--;
                yIndexPos = y--;
            }
        }
    }

    void GenerateWorld ()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				perlinNoise = Mathf.PerlinNoise (seed + (x * 500f), seed + (y * 500f));
				float thisWaterChance = waterChance;

				if (perlinNoise < thisWaterChance)
				{
					tileGridGameObjects[x, y] = Instantiate (waterTile, new Vector2 (x, y), Quaternion.identity);

                    Tile tile = tileGridGameObjects[x, y].GetComponent<Tile>();
                    tile.tileType = Tile.TileType.water;
                    tile.x = x;
                    tile.y = y;
				}
				else
				{
					tileGridGameObjects[x,y] = Instantiate (dirtTile, new Vector2 (x, y), Quaternion.identity);

                    Tile tile = tileGridGameObjects[x, y].GetComponent<Tile>();
                    tile.tileType = Tile.TileType.dirt;
                    tile.x = x;
                    tile.y = y;
                }
			}
		}
	}

    int GetNeighourTilesOfType(Tile tile, Tile.TileType tileType)
    {
        int neighourTilesOfCorrectType = 0;

        //

        return neighourTilesOfCorrectType;
    }

    Tile[] GetNeighourTiles(Tile tile)
    {
        List<Tile> neighbouringTilesList = new List<Tile>();

        Tile[] neighbouringTilesArray = new Tile[neighbouringTilesList.Count];

        int arrayPos = 0;

        foreach (Tile t in neighbouringTilesList)
        {
            neighbouringTilesArray[arrayPos] = t;
            arrayPos++;
        }
    }
}