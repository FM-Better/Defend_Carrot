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
                // 注册倒计时完成的命令
                if (!Facade.HasCommand(MVCNotification.COUNTDOWN_OVER))
                {
                    Facade.RegisterCommand(MVCNotification.COUNTDOWN_OVER, () =>
                    {
                        return new CountDownOverCommand();
                    });
                }
                // 注册回合开始的命令
                if (!Facade.HasCommand(MVCNotification.RUN_ROUND))
                {
                    Facade.RegisterCommand(MVCNotification.RUN_ROUND, () =>
                    {
                        return new RunRoundCommand();
                    });
                }
                // 注册改变时间流速的命令
                if (!Facade.HasCommand(MVCNotification.CHANGE_TIME))
                {
                    Facade.RegisterCommand(MVCNotification.CHANGE_TIME, () =>
                    {
                        return new ChangeTimeCommand();
                    });
                }

                // 设置倒计时视图
                SendNotification(MVCNotification.SET_VIEW, Consts.V_CountDown);
                // 设置公告板视图
                SendNotification(MVCNotification.SET_VIEW, Consts.V_Board);
                // 设置菜单视图
                SendNotification(MVCNotification.SET_VIEW, Consts.V_Menu);
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
