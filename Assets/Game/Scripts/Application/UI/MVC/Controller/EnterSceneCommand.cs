using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 进入场景
/// </summary>
public class EnterSceneCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        string sceneName = notification.Body as string;

        switch (sceneName)
        {
            case Consts.StartScene:
                // 设置开始视图
                SendNotification(MVCNotification.SET_VIEW, Consts.V_Start);
                break;
            case Consts.SelectScene:
                // 设置选关视图
                SendNotification(MVCNotification.SET_VIEW, Consts.V_Select);
                (Facade.RetrieveMediator(SelectViewMediator.NAME) as SelectViewMediator).InitCard();
                break;
            case Consts.LevelScene:
                // 注册初始化关卡的命令
                if (!Facade.HasCommand(MVCNotification.INIT_LEVEL))
                {
                    Facade.RegisterCommand(MVCNotification.INIT_LEVEL, () =>
                    {
                        return new InitLevelCommand();
                    });
                }
                break;
            case Consts.CompleteScene:
                // 设置通过视图
                SendNotification(MVCNotification.SET_VIEW, Consts.V_Complete);
                break;
            default:
                break;
        }
    }
}
