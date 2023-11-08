using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuViewMediator : Mediator
{
    public static new string NAME = "MenuViewMediator";

    public MenuViewMediator() : base(NAME) { }

    /// <summary>
    ///  ��View������
    /// </summary>
    public void SetView(MenuView view)
    {
        ViewComponent = view;

        // ������ť�߼�

    }

    // ��д����֪ͨ�ķ���
    public override string[] ListNotificationInterests()
    {
        return new string[] {

        };
    }

    // ���´���֪ͨ�ķ���
    public override void HandleNotification(INotification notification)
    {
        switch (notification)
        {
            default:
                break;
        }
    }
}
