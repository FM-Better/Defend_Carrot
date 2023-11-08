using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewMediator : Mediator
{
    public static new string NAME = "BoardViewMediator";

    public BoardViewMediator() : base(NAME) { }

    /// <summary>
    ///  将View关联上
    /// </summary>
    public void SetView(BoardView view)
    {
        ViewComponent = view;

        // 监听按钮逻辑
        view.btnPause.onClick.AddListener(() =>
        {
            (Facade.RetrieveProxy(LevelDataProxy.NAME) as LevelDataProxy).Pause();
            view.btnPause.gameObject.SetActive(false);
            view.btnResume.gameObject.SetActive(true);
            view.imgRoundInfo.gameObject.SetActive(false);
            view.imgPauseInfo.gameObject.SetActive(true);
        });

        view.btnResume.onClick.AddListener(() =>
        {
            (Facade.RetrieveProxy(LevelDataProxy.NAME) as LevelDataProxy).Resume();
            view.btnResume.gameObject.SetActive(false);
            view.btnPause.gameObject.SetActive(true);
            view.imgPauseInfo.gameObject.SetActive(false);
            view.imgRoundInfo.gameObject.SetActive(true);
        });

        view.btnSpeed1.onClick.AddListener(() =>
        {
            (Facade.RetrieveProxy(LevelDataProxy.NAME) as LevelDataProxy).SpeedUp();
            view.btnSpeed1.gameObject.SetActive(false);
            view.btnSpeed2.gameObject.SetActive(true);
        });

        view.btnSpeed2.onClick.AddListener(() =>
        {
            (Facade.RetrieveProxy(LevelDataProxy.NAME) as LevelDataProxy).SlowDown();
            view.btnSpeed2.gameObject.SetActive(false);
            view.btnSpeed1.gameObject.SetActive(true);
        });
    }

    /// <summary>
    /// 设置总的回合数
    /// </summary>
    /// <param name="totalRoundNum"></param>
    public void SetTotalRoundInfo(int totalRoundNum)
    {
        (ViewComponent as BoardView).txtTotal.text = totalRoundNum.ToString("D2"); // 始终保持2位数
    }

    /// <summary>
    /// 更新回合信息
    /// </summary>
    /// <param name="currentRoundNum"> 当前回合数 </param>
    public void UpdataRoundInfo(int currentRoundNum)
    {
        (ViewComponent as BoardView).txtCurrent.text = currentRoundNum.ToString("D2"); // 始终保持2位数
    }

    /// <summary>
    /// 更新金币
    /// </summary>
    /// <param name="money"></param>
    public void UpdateMoney(int money)
    {
        (ViewComponent as BoardView).txtMoney.text = money.ToString(); // 始终保持2位数
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
