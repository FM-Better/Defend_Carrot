using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataProxy : Proxy
{
    public new const string NAME = "LevelDataProxy";

    public LevelDataProxy() : base(NAME)
    {
        // 创建一个Player数据
        LevelData levelData = new LevelData();
        levelData.speed = 1f;

        // 关联数据
        Data = levelData;
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
