﻿using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinViewMediator : Mediator
{
    public static new string NAME = "WinViewMediator";

    public WinViewMediator() : base(NAME) { }

    /// <summary>
    ///  将View关联上
    /// </summary>
    public void SetView(WinView view)
    {
        ViewComponent = view;

        // 监听按钮逻辑

    }

    // 重写监听通知的方法
    public override string[] ListNotificationInterests()
    {
        return new string[] {

        };
    }

    // 重新处理通知的方法
    public override void HandleNotification(INotification notification)
    {
        switch (notification)
        {
            default:
                break;
        }
    }
}
