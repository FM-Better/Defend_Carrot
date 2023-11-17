using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;

public class SelectViewMediator : Mediator
{
    public static new string NAME = "SelectViewMediator";

    // 当前选择的卡牌索引
    private int m_selectIndex = -1;

    private List<Card> cards = new List<Card>();
    private UICard[] uiCards;

    public SelectViewMediator() : base(NAME) {}

    /// <summary>
    ///  将View关联上
    /// </summary>
    public void SetView(SelectView view)
    {
        ViewComponent = view;

        // 监听按钮逻辑
        view.btnBack.onClick.AddListener(() =>
        {
            // 取消自己的视图
            SendNotification(MVCNotification.CANCEL_VIEW, this);

            // 加载开始场景
            SendNotification(MVCNotification.LOAD_SCENE, new LoadSceneArgs(Consts.StartIndex, () =>
            {
                // 进入开始场景
                SendNotification(MVCNotification.ENTER_SCENE, Consts.StartScene);
            }));
        });

        view.btnPlay.onClick.AddListener(() =>
        {
            // 取消自己的视图
            SendNotification(MVCNotification.CANCEL_VIEW, this);

            // 加载关卡场景
            SendNotification(MVCNotification.LOAD_SCENE, new LoadSceneArgs(Consts.LevelIndex, () =>
            {
                // 进入关卡场景
                SendNotification(MVCNotification.ENTER_SCENE, Consts.LevelScene);

                // 初始化关卡信息
                SendNotification(MVCNotification.INIT_LEVEL, m_selectIndex);
            }));
        });
    }

    // 初始化卡牌
    public void InitCard()
    {
        // 进入选关场景后加载所有卡牌信息
        LoadAllCardInfo();
        UpdateSelectCard(0); // 且默认选择第一张卡牌
    }

    // 加载所有卡牌信息
    public void LoadAllCardInfo()
    {
        List<Level> levels = ((Facade.RetrieveProxy(GameDataProxy.NAME) as GameDataProxy).Data as GameData).levels;
        int passedLevelNum = ((Facade.RetrieveProxy(GameDataProxy.NAME) as GameDataProxy).Data as GameData).passedLevelNum;
        foreach (Level level in levels)
        {
            Card card = new Card();
            card.levelID = level.levelID;
            card.cardPath = level.cardName;
            card.isLocked = level.levelID - 1 > passedLevelNum;
            cards.Add(card);
        }

        uiCards = (ViewComponent as SelectView).uiCards;
        foreach (UICard uicard in uiCards)
        {
            uicard.onClick += (card) => UpdateSelectCard(card.levelID - 1);
        }
    }

    // 更新当前选择的卡牌
    public void UpdateSelectCard(int cardIndex)
    {
        if (m_selectIndex == cardIndex)
            return;

        m_selectIndex = cardIndex;
        int leftIndex = cardIndex - 1;
        int currentIndx = cardIndex;
        int rightIndex = cardIndex + 1;

        UICard leftCard = uiCards[0];
        UICard selectCard = uiCards[1];
        UICard rightCard = uiCards[2];

        if (leftIndex < 0)
        {
            leftCard.gameObject.SetActive(false);
        }
        else
        {
            leftCard.gameObject.SetActive(true);
            leftCard.IsTransparent = true;
            leftCard.SetCard(cards[leftIndex]);
        }

        selectCard.gameObject.SetActive(true);
        selectCard.IsTransparent = false;
        selectCard.SetCard(cards[currentIndx]);
        (ViewComponent as SelectView).btnPlay.gameObject.SetActive(!cards[cardIndex].isLocked);

        if (rightIndex >= cards.Count)
        {
            rightCard.gameObject.SetActive(false);
        }
        else
        {
            rightCard.gameObject.SetActive(true);
            rightCard.IsTransparent = true;
            rightCard.SetCard(cards[rightIndex]);
        }
    }
}
