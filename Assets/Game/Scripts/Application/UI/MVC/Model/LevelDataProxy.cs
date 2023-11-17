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
        GameObject gameObject = GameObject.Find(Consts.Map);
        // 初始化Map的Level
        GameObject.Find(Consts.Map).GetComponent<Map>().LoadLevel(gameData.levels[levelIndex]);
        // 设置公告板视图
        SendNotification(MVCNotification.SET_VIEW, Consts.V_Board);
        // 更新公告板面板
        SendNotification(MVCNotification.UPDATE_BOARD, Data);
    }

    // 回合开始
    public void RunRound(int roundNum) => (Data as LevelData).roundCurrentNum = roundNum;

    // 暂停
    public void Pause() => Time.timeScale = 0f;

    // 继续
    public void Resume() => Time.timeScale = (Data as LevelData).speed;

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
