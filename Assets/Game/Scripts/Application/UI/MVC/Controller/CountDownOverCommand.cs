using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownOverCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        SendNotification(MVCNotification.LOAD_SCENE, new LoadSceneArgs(Consts.CompleteIndex, ()=>
        {
            SendNotification(MVCNotification.ENTER_SCENE, Consts.CompleteScene);
        }));
    }
}
