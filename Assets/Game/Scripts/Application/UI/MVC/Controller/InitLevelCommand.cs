using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLevelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        // 注册倒计时完成的命令
        if (!Facade.HasCommand(MVCNotification.COUNTDOWN_OVER))
        {
            Facade.RegisterCommand(MVCNotification.COUNTDOWN_OVER, () =>
            {
                return new CountDownOverCommand();
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
        // 注册回合开始的命令
        if (!Facade.HasCommand(MVCNotification.RUN_ROUND))
        {
            Facade.RegisterCommand(MVCNotification.RUN_ROUND, () =>
            {
                return new RunRoundCommand();
            });
        }
        // 注册更新公告板的命令
        if (!Facade.HasCommand(MVCNotification.UPDATE_BOARD))
        {
            Facade.RegisterCommand(MVCNotification.UPDATE_BOARD, () =>
            {
                return new UpdateBoardCommand();
            });
        }

        // 设置倒计时视图
        SendNotification(MVCNotification.SET_VIEW, Consts.V_CountDown);
        // 创建并注册关卡数据
        if (!Facade.HasProxy(LevelDataProxy.NAME))
        {
            LevelDataProxy levelDataProxy = new LevelDataProxy();
            Facade.RegisterProxy(levelDataProxy);
        }
        (Facade.RetrieveProxy(LevelDataProxy.NAME) as LevelDataProxy).InitializeLevelInfo((int)notification.Body);
        // 设置菜单视图
        SendNotification(MVCNotification.SET_VIEW, Consts.V_Menu);
    }
}
