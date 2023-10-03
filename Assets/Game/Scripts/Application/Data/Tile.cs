using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    public int x;
    public int y;
    public bool canHold;
    public object data;

    public Tile(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
