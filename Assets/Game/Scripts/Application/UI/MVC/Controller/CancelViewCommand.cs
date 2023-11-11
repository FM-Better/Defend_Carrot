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
            // 将对应的视图取消
            mediator.ViewComponent = null;
            // 将其从MVC中移除
            Facade.RemoveMediator(mediator.MediatorName);
        }
    }
}
