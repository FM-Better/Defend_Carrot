using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownViewMediator : Mediator
{
    public static new string NAME = "CountDownViewMediator";

    public CountDownViewMediator() : base(NAME) { }

    /// <summary>
    ///  将View关联上
    /// </summary>
    public void SetView(CountDownView view)
    {
        ViewComponent = view;

        // 开始倒计时
        view.CountDown();
    }

    // 重写监听通知的方法
    public override string[] ListNotificationInterests()
    {
        return new string[] {
            MVCNotification.COUNTDOWN_OVER,
        };
    }

    // 重新处理通知的方法
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name) 
        {
            case MVCNotification.COUNTDOWN_OVER:
                // 倒计时完成将自己关闭且移除
                (ViewComponent as CountDownView).gameObject.SetActive(false);
                SendNotification(MVCNotification.CANCEL_VIEW, this);
                break;
            default: 
                break;
        }
    }
}
