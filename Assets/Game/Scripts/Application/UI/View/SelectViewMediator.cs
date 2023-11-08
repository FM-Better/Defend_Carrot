using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectViewMediator : Mediator
{
    public static new string NAME = "SelectViewMediator";

    public SelectViewMediator() : base(NAME) {}

    /// <summary>
    ///  将View关联上
    /// </summary>
    public void SetView(SelectView view)
    {
        ViewComponent = view;

        // 监听按钮逻辑
        // 进入开始场景
        view.btnBack.onClick.AddListener(() =>
        {
            // 取消自己的视图
            SendNotification(MVCNotification.CANCEL_VIEW, this);

            SendNotification(MVCNotification.LOAD_SCENE, new LoadSceneArgs(Consts.StartIndex, () =>
            {
                // 设置开始视图
                SendNotification(MVCNotification.SET_VIEW, Consts.V_Start);
            }));
        });

        // 进入关卡场景
        view.btnPlay.onClick.AddListener(() =>
        {
            // 取消自己的视图
            SendNotification(MVCNotification.CANCEL_VIEW, this);

            SendNotification(MVCNotification.LOAD_SCENE, new LoadSceneArgs(Consts.LevelIndex, () =>
            {
                // 创建关卡数据
                

                // 设置公告板视图
                SendNotification(MVCNotification.SET_VIEW, Consts.V_Board);
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
