using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownView : MonoBehaviour
{
    public Image count;
    public Sprite[] numbers;

    private int time = 3;
    private WaitForSeconds second = new WaitForSeconds(1);

    public void CountDown()
    {
        StartCoroutine(Cou_CountDown());
    }

    IEnumerator Cou_CountDown()
    {
        while (time > 0)
        {
            count.sprite = numbers[3 - time];
            yield return second;
            time--;
        }

        GameFacade.Instance.SendNotification(MVCNotification.COUNTDOWN_OVER);
    }
}
