using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bird : MonoBehaviour
{
    private Vector3 _endPosition;

    public float offsetY;
    public float duration;

    void Start()
    {
        _endPosition = transform.position + transform.up * offsetY;
        // 循环的来回移动 线性缓动
        transform.DOMove(_endPosition, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
}
