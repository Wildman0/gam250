using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public enum TileType { dirt, water };
    public TileType tileType;
    public int x;
    public int y;
}
