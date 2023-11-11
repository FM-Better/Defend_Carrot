using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICard : MonoBehaviour, IPointerDownHandler
{
    public Image imgCard;
    public Image imgLock;
    public UnityAction<Card> onClick;

    private Card m_cardData;

    private bool m_isTransparent = false;

    public bool IsTransparent
    {
        get => m_isTransparent; 
        set
        {
            m_isTransparent = value;
            // 设置卡牌的透明度
            Color color = imgCard.color;
            color.a = m_isTransparent ? 0.5f : 1f;  
            imgCard.color = color;
            imgLock.color = color;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        onClick?.Invoke(m_cardData);
    }

    private void OnDestroy()
    {
        while(onClick!= null)
        {
            onClick -= onClick;
        }
    }

    /// <summary>
    /// 设置卡牌
    /// </summary>
    /// <param name="card"> 要关联的卡牌数据 </param>
    public void SetCard(Card card)
    {
        // 关联卡牌数据
        m_cardData = card;

        // 加载图片
        string cardPath = "file://" + Consts.CardDir + "/" + m_cardData.cardPath;
        StartCoroutine(Tools.LoadImage(cardPath, imgCard));

        // 是否锁定
        imgLock.gameObject.SetActive(m_cardData.isLocked);
    }
}
