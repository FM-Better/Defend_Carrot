using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartViewMediator : Mediator
{
    public static new string NAME = "StartViewMediator";

    public StartViewMediator() : base(NAME) {}

    // ��д����֪ͨ�ķ���
    public override string[] ListNotificationInterests()
    {
        return new string[] {
            
        };
    }

    // ���´���֪ͨ�ķ���
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name) 
        {
            default:

            break;
        }
    }
}
