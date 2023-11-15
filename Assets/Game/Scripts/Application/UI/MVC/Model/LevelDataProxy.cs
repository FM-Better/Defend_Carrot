using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelDataProxy : Proxy
{
    public new const string NAME = "LevelDataProxy";

    public LevelDataProxy() : base(NAME)
    {
        // 创建关卡数据
        LevelData levelData = new LevelData();
        levelData.speed = 1f;

        // 关联数据
        Data = levelData;
    }

    // 初始化关卡信息
    public void InitializeLevelInfo(int levelIndex)
    {
        GameData gameData = ((Facade.RetrieveProxy(GameDataProxy.NAME) as GameDataProxy).Data as GameData);
        (Data as LevelData).levelIndex = levelIndex;
        (Data as LevelData).money = gameData.levels[levelIndex].initMoney;
        (Data as LevelData).roundTotalNum = gameData.levels[levelIndex].rounds.Count;
    }

    // 进入下一个回合
    public void EnterNextRound()
    {
        (Data as LevelData).roundCurrentNum++;
    }

    // 暂停
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    // 继续
    public void Resume()
    {
        Time.timeScale = (Data as LevelData).speed;
    }

    // 2倍速
    public void SpeedUp()
    {
        (Data as LevelData).speed = 2f;
        Time.timeScale = (Data as LevelData).speed;
    }

    // 1倍速
    public void SlowDown()
    {
        (Data as LevelData).speed = 1f;
        Time.timeScale = (Data as LevelData).speed;
    }
}
