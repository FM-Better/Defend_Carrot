using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 常量数据类
/// </summary>
public static class Consts
{
    // 关卡配置文件夹路径
    public static readonly string LevelDir = Application.dataPath + @"/Game/Config/Levels";
    // 地图资源文件夹路径
    public static readonly string MapDir = Application.dataPath + @"/Game/ArtRes/Maps";

    // 可放置点的Gizmos资源名
    public static readonly string HolderGizemos = "holder.png";
    // 路径起点的Gizmos资源名
    public static readonly string StartGizemos = "start.png";
    // 路径起点的Gizmos资源名
    public static readonly string EndGizemos = "end.png";
}
