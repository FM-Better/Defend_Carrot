using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

/// <summary>
/// 常量数据类
/// </summary>
public static class Consts
{
    #region 路径相关
    // 关卡配置文件夹路径
    public static readonly string LevelDir = Application.dataPath + @"/Game/Config/Levels";
    // 地图资源文件夹路径
    public static readonly string MapDir = Application.dataPath + @"/Game/ArtRes/Maps";
    // 卡牌文件夹路径
    public static readonly string CardDir = Application.dataPath + @"/Game/ArtRes/Cards";
    #endregion

    #region Gizmos资源名相关
    // 可放置点的Gizmos资源名
    public const string HolderGizmos = "holder.png";
    // 路径起点的Gizmos资源名
    public const string StartGizmos = "start.png";
    // 路径终点的Gizmos资源名
    public const string EndGizmos = "end.png";
    #endregion

    #region 场景索引相关
    // 开始场景索引
    public const int StartIndex = 1;
    // 选关场景索引
    public const int SelectIndex = 2;
    // 关卡场景索引
    public const int LevelIndex = 3;
    // 通关场景索引
    public const int CompleteIndex = 4;
    #endregion

    #region 场景名相关
    // 选关场景
    public const string StartScene = "Start";
    // 选关场景
    public const string SelectScene = "Select";
    // 关卡场景
    public const string LevelScene = "Level";
    // 通关场景
    public const string CompleteScene = "Complete";
    #endregion

    #region 视图名相关
    // 开始场景的视图名
    public const string V_Start = "StartView";
    // 选关场景的视图名
    public const string V_Select = "SelectView";
    // 关卡场景的公告板的视图名
    public const string V_Board = "BoardView";
    // 关卡场景的倒计时的视图名
    public const string V_CountDown = "CountDownView";
    // 关卡场景的菜单的视图名
    public const string V_Menu = "MenuView";
    // 通关场景的视图名
    public const string V_Complete = "CompleteView";
    #endregion

    #region 其余UI名
    public const string UI_Level = "LevelCanvas";
    #endregion
}
