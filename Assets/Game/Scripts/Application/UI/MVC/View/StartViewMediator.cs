using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartViewMediator : Mediator
{
    public static new string NAME = "StartViewMediator";

    public StartViewMediator() : base(NAME) {}

    /// <summary>
    ///  将View关联上
    /// </summary>
    public void SetView(StartView view)
    {
        ViewComponent = view;
        // 监听按钮逻辑
        // 进入选关场景
        view.btnAdventure.onClick.AddListener(() =>
        {
            // 取消自己的视图
            SendNotification(MVCNotification.CANCEL_VIEW, this);

            SendNotification(MVCNotification.LOAD_SCENE, new LoadSceneArgs(Consts.SelectIndex, () =>
            {
                SendNotification(MVCNotification.ENTER_SCENE, Consts.SelectScene);
            }));
        });
    }

    // 重写监听通知的方法
    public override string[] ListNotificationInterests()
    {
        return new string[] {
            
        };
    }

    // 重新处理通知的方法
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name) 
        {
            default:
                break;
        }
    }
}
