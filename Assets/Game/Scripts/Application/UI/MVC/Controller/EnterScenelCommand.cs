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
                // 创建关卡数据
                if (!Facade.HasProxy(LevelDataProxy.NAME))
                {
                    LevelDataProxy levelDataProxy = new LevelDataProxy();
                    Facade.RegisterProxy(levelDataProxy);
                }
                // 注册倒计时完成的命令
                if (!Facade.HasCommand(MVCNotification.COUNTDOWN_OVER))
                {
                    Facade.RegisterCommand(MVCNotification.COUNTDOWN_OVER, () =>
                    {
                        return new CountDownOverCommand();
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
