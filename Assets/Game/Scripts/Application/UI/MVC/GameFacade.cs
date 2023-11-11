using PureMVC.Patterns.Facade;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFacade : Facade
{
    // 为了方便使用Facade单例 自己写一个属性
    public static GameFacade Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameFacade();
            }
            return instance as GameFacade;
        }
    }

    /// <summary>
    /// 初始化控制层
    /// </summary>
    protected override void InitializeController()
    {
        base.InitializeController();
        // 将命令和通知绑定
        RegisterCommand(MVCNotification.START_UP, () =>
        {
            return new StartUpCommand();
        });

        RegisterCommand(MVCNotification.LOAD_SCENE, () =>
        {
            return new LoadSceneCommand();
        });

        RegisterCommand(MVCNotification.ENTER_SCENE, () =>
        {
            return new EnterSceneCommand();
        });

        RegisterCommand(MVCNotification.SET_VIEW, () =>
        {
            return new SetViewCommand();
        });

        RegisterCommand(MVCNotification.CANCEL_VIEW, () =>
        {
            return new CancelViewCommand();
        });
    }

    /// <summary>
    /// 启动
    /// </summary>
    public void StartUp()
    {
        SendNotification(MVCNotification.START_UP);
    }
}
