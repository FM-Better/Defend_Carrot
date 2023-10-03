using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 格子数据类
/// </summary>
public class Tile
{
    // 横坐标
    public int x;
    // 纵坐标
    public int y;
    // 能否放置炮台
    public bool canHold;
    // 存放的数据
    public object data;

    public Tile(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
