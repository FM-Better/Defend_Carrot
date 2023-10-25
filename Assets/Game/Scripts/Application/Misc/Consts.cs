using System.Collections;
using System.Collections.Generic;
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
    #endregion

    #region Gizmos资源名相关
    // 可放置点的Gizmos资源名
    public const string HolderGizemos = "holder.png";
    // 路径起点的Gizmos资源名
    public const string StartGizemos = "start.png";
    // 路径终点的Gizmos资源名
    public const string EndGizemos = "end.png";
    #endregion

    #region 场景索引相关
    // 开始场景索引
    public const int StartIndex = 1;
    // 选关场景索引
    public const int SelectIndex = 2;
    #endregion


    #region 视图名相关
    // 开始场景的视图名
    public const string V_Start = "StartView";
    // 选关场景的视图名
    public const string V_Select = "SelectView";
    #endregion
}
