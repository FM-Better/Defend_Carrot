using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelViewCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        Mediator mediator = notification.Body as Mediator;
        if (mediator != null && mediator.ViewComponent != null)
        {
            // ����Ӧ����ͼȡ��
            mediator.ViewComponent = null;
        }
    }
}
