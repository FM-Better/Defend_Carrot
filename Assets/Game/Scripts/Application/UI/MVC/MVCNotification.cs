using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// MVC通知类 存储MVC中所有通知的名字
/// </summary>
public class MVCNotification
{
    // 启动
    public const string START_UP = "START_UP";
    // 加载场景
    public const string LOAD_SCENE = "LOAD_SCENE";
    // 设置视图
    public const string SET_VIEW = "SET_VIEW";
    // 取消视图
    public const string CANCEL_VIEW = "CANCEL_VIEW";

    // 进入场景
    public const string ENTER_SCENE = "ENTER_SCENE";
    // 初始化关卡
    public const string INIT_LEVEL = "INIT_LEVEL";
    // 倒计时结束
    public const string COUNTDOWN_OVER = "COUNTDOWN_OVER";
    // 退出场景
    public const string EXIT_SCENE = "EXIT_SCENE";
    // 改变时间流速
    public const string CHANGE_TIME = "CHANGE_TIME";
    // 回合开始
    public const string RUN_ROUND = "RUN_ROUND";
    // 更新公告板
    public const string UPDATE_BOARD = "UPDATE_BOARD";
}
