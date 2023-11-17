using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardViewMediator : Mediator
{
    public static new string NAME = "BoardViewMediator";

    public BoardViewMediator() : base(NAME) { }

    private Spawner m_spawner;

    /// <summary>
    ///  将View关联上
    /// </summary>
    public void SetView(BoardView view)
    {
        m_spawner = GameObject.Find(Consts.Map).GetComponent<Spawner>();
        m_spawner.InitializeSpawner();
        ViewComponent = view;

        // 监听按钮逻辑
        view.btnPause.onClick.AddListener(() =>
        {
            view.btnPause.gameObject.SetActive(false);
            view.btnResume.gameObject.SetActive(true);
            view.imgRoundInfo.gameObject.SetActive(false);
            view.imgPauseInfo.gameObject.SetActive(true);
            SendNotification(MVCNotification.CHANGE_TIME, E_ChengTimeType.Pause);
        });

        view.btnResume.onClick.AddListener(() =>
        {
            view.btnPause.gameObject.SetActive(true);
            view.btnResume.gameObject.SetActive(false);
            view.imgRoundInfo.gameObject.SetActive(true);
            view.imgPauseInfo.gameObject.SetActive(false);
            SendNotification(MVCNotification.CHANGE_TIME, E_ChengTimeType.Resume);
        });

        view.btnSpeed1.onClick.AddListener(() =>
        {
            view.btnSpeed1.gameObject.SetActive(false);
            view.btnSpeed2.gameObject.SetActive(true);
            SendNotification(MVCNotification.CHANGE_TIME, E_ChengTimeType.SpeedUp);
        });

        view.btnSpeed2.onClick.AddListener(() =>
        {
            view.btnSpeed1.gameObject.SetActive(true);
            view.btnSpeed2.gameObject.SetActive(false);
            SendNotification(MVCNotification.CHANGE_TIME, E_ChengTimeType.SlowDown);
        });

        view.btnMenu.onClick.AddListener(() =>
        {
            // 打开菜单
            ((Facade.RetrieveMediator(MenuViewMediator.NAME) as MenuViewMediator).ViewComponent as MenuView).gameObject.SetActive(true);
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
            MVCNotification.COUNTDOWN_OVER,
        };
    }

    // 重新处理通知的方法
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case MVCNotification.COUNTDOWN_OVER:
                // 开始出怪
                m_spawner.StartSpawn();
                break;
            default:
                break;
        }
    }
}
