using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private Vector3 _endPosition;

    public float duration;

    void Start()
    {
        _endPosition = transform.position + transform.right * Screen.width * 1.2f;
        // 循环的从头移动 线性缓动
        transform.DOMove(_endPosition, duration)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
}
