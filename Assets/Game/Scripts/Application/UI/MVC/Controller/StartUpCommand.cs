using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        // 加载开始场景
        SendNotification(MVCNotification.LOAD_SCENE, new LoadSceneArgs(Consts.StartIndex, () =>
        {
            // 进入开始场景
            SendNotification(MVCNotification.ENTER_SCENE, Consts.StartScene);
        }));
    }
}
