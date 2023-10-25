using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        // ���뿪ʼ����
        SendNotification(MVCNotification.LOAD_SCENE, new LoadSceneArgs(Consts.StartIndex, () =>
        {
            // �򿪿�ʼ���
            SendNotification(MVCNotification.SET_VIEW, "StartView");
        }));
    }
}
