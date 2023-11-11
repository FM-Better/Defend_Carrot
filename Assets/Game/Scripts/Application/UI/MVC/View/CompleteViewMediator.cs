using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteViewMediator : Mediator
{
    public static new string NAME = "CompleteViewMediator";

    public CompleteViewMediator() : base(NAME) { }

    /// <summary>
    ///  将View关联上
    /// </summary>
    public void SetView(CompleteView view)
    {
        ViewComponent = view;
        // 监听按钮逻辑
        view.btnRestart.onClick.AddListener(() =>
        {
            SendNotification(MVCNotification.LOAD_SCENE, new LoadSceneArgs(Consts.StartIndex, () =>
            {
                SendNotification(MVCNotification.ENTER_SCENE, Consts.StartScene);
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
