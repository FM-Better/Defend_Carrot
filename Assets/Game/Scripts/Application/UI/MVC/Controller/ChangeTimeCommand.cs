using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_ChengTimeType
{
    Pause,
    Resume,
    SpeedUp,
    SlowDown
}

public class ChangeTimeCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        switch (notification.Body)
        {
            case E_ChengTimeType.Pause:
                (Facade.RetrieveProxy(LevelDataProxy.NAME) as LevelDataProxy).Pause();
                break;
            case E_ChengTimeType.Resume:
                (Facade.RetrieveProxy(LevelDataProxy.NAME) as LevelDataProxy).Resume();
                break;
            case E_ChengTimeType.SpeedUp:
                (Facade.RetrieveProxy(LevelDataProxy.NAME) as LevelDataProxy).SpeedUp();
                break;
            case E_ChengTimeType.SlowDown:
                (Facade.RetrieveProxy(LevelDataProxy.NAME) as LevelDataProxy).SlowDown();
                break;
            default:
                break;
        }
    }
}
