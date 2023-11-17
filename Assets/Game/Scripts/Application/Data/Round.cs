using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回合数据类
/// </summary>
public class Round
{
    // 怪物种类
    public int monsterType;
    // 怪物数量
    public int monsterNum;

    public Round()
    {

    }

    public Round(int type, int num)
    {
        monsterType = type;
        monsterNum = num;
    }
}
