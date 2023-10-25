using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartViewMediator : Mediator
{
    public static new string NAME = "StartViewMediator";

    public StartViewMediator() : base(NAME) {}

    // 重写监听通知的方法
    public override string[] ListNotificationInterests()
    {
        return new string[] {
            
        };
    }

    // 重新处理通知的方法
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name) 
        {
            default:

            break;
        }
    }
}
