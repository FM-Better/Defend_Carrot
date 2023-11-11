using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuViewMediator : Mediator
{
    public static new string NAME = "MenuViewMediator";

    public MenuViewMediator() : base(NAME) { }

    /// <summary>
    ///  将View关联上
    /// </summary>
    public void SetView(MenuView view)
    {
        ViewComponent = view;

        // 监听按钮逻辑
        view.btnChoose.onClick.AddListener(() =>
        {
            SendNotification(MVCNotification.LOAD_SCENE, new LoadSceneArgs(Consts.SelectIndex, () =>
            {
                // 进入选关场景
                SendNotification(MVCNotification.ENTER_SCENE, Consts.SelectScene);
            }));
        });

        view.btnContinue.onClick.AddListener(() =>
        {
            view.gameObject.SetActive(false);
        });

        view.btnRestart.onClick.AddListener(() =>
        {
            SendNotification(MVCNotification.LOAD_SCENE, new LoadSceneArgs(Consts.LevelIndex, () =>
            {
                // 进入关卡场景
                SendNotification(MVCNotification.ENTER_SCENE, Consts.LevelScene);
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
        switch (notification)
        {
            default:
                break;
        }
    }
}
