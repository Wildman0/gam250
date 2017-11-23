using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

	public int width = 50;
	public int height = 50;

	public GameObject[,] tileGrid;

	public float seed;

	public Vector2 perlinData;
	public float perlinNoise;

	public GameObject dirtTile;
	public GameObject waterTile;

	public float waterChance = 1f;

	void Start ()
	{
		if (seed == 0f)
		{
			GenerateSeed ();
		}

		tileGrid = new GameObject[width, height];

		GenerateWorld ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			DestroyMap ();
			GenerateSeed ();
			GenerateWorld ();
		}
	}

	void SetScale ()
	{

	}

	void DestroyMap ()
	{
		foreach(GameObject go in tileGrid)
		{
			Destroy (go);
		}
	}

	void GenerateSeed ()
	{
		seed = Random.Range (0f, 1f);
	}

	void GenerateWorld ()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				perlinNoise = Mathf.PerlinNoise (seed + (x * width/25), seed + (y * height/25));
				float thisWaterChance = waterChance;

				if (x < width / 5)
				{
					if (x != 0)
					{
						thisWaterChance = waterChance * ((x + 1) / x);
					}
					else
					{
						thisWaterChance = 1f;
					}
				}
				else if (y < width / 5)
				{
					if (y != 0)
					{ 
						thisWaterChance = waterChance * ((y + 1) / y);
					}
					else
					{
						thisWaterChance = 1f;
					}
				}
				else if (x > width - (width / 5))
				{
					
				}
				else if (y > width - (width / 5))
				{
					
				}

				if (perlinNoise < thisWaterChance)
				{
					tileGrid[x, y] = Instantiate (waterTile, new Vector2 (x, y), Quaternion.identity);
				}
				else
				{
					tileGrid[x,y] = Instantiate (dirtTile, new Vector2 (x, y), Quaternion.identity);
				}
			}
		}
	}
}