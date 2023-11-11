using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡数据类
/// </summary>
public class Level
{
    // 关卡ID
    public int levelID;
    // 是否被锁定
    public bool isLocked;
    // 关卡名
    public string name;
    // 卡牌名
    public string cardName;
    // 背景图的资源名字
    public string background;
    // 行走路径的资源名字
    public string road;
    // 初始金币
    public int initMoney;

    // 放置点
    public List<Point> holders = new List<Point>();
    // 可行走路径
    public List<Point> path = new List<Point>();
    // 回合
    public List<Round> rounds = new List<Round>();
}
