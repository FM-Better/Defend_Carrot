using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameDataProxy : Proxy
{
    public new const string NAME = "GameDataProxy";

    public GameDataProxy() : base(NAME)
    {
        // 创建游戏数据
        GameData gameData = new GameData();
        // 关联数据
        Data = gameData;

        // 加载所有关卡信息
        LoadAllLevelInfo();

        // 通关数
        PassedLevelNum passedLevelNum = new PassedLevelNum();
        // 如果还没有保存过通关数 则保存
        if (!JsonMgr.Instance.TryLoadData<PassedLevelNum>(Consts.PassedLevelNum, out passedLevelNum))
        {
            (Data as GameData).passedLevelNum = -1;

            SavePassedLevelNum(0);
        }
        else
        {
            (Data as GameData).passedLevelNum = passedLevelNum.passedLevelNum;
        }
    }

    // 加载所有关卡信息
    public void LoadAllLevelInfo()
    {
        List<FileInfo> levelInfos = Tools.GetLevelFiles();
        List<Level> levels = new List<Level>();
        foreach (FileInfo levelInfo in levelInfos)
        {
            Level level = new Level();
            Tools.FillLevel(levelInfo.FullName, ref level);
            levels.Add(level);
        }
        (Data as GameData).levels = levels;
    }

    // 存档已通关的关卡数
    public void SavePassedLevelNum(int levelIndex)
    {
        int passedNum = (Data as GameData).passedLevelNum;
        if (levelIndex > passedNum)
        {
            (Data as GameData).passedLevelNum = levelIndex;
            PassedLevelNum passedLevelNum = new PassedLevelNum();
            passedLevelNum.passedLevelNum = levelIndex;
            JsonMgr.Instance.SaveData(passedLevelNum, Consts.PassedLevelNum);
        }
    }
}
