using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏数据
/// </summary>
public class GameData
{
    // 当前通过的关卡数
    public int passedLevelNum;
    // 所有关卡信息
    public List<Level> levels;
    // 所有怪物消息
    public MonsterInfoContainer monsterInfoContainer;
}
