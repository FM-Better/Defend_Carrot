using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataProxy : Proxy
{
    public new const string NAME = "LevelDataProxy";

    public LevelDataProxy() : base(NAME)
    {
        // ����һ��Player����
        LevelData levelData = new LevelData();
        levelData.speed = 1f;

        // ��������
        Data = levelData;
    }

    // ��ͣ
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    // ����
    public void Resume()
    {
        Time.timeScale = (Data as LevelData).speed;
    }

    // 2����
    public void SpeedUp()
    {
        (Data as LevelData).speed = 2f;
        Time.timeScale = (Data as LevelData).speed;
    }

    // 1����
    public void SlowDown()
    {
        (Data as LevelData).speed = 1f;
        Time.timeScale = (Data as LevelData).speed;
    }
}
