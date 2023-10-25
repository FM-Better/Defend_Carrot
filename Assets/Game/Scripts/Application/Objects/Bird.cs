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
        // ѭ���������ƶ� ���Ի���
        transform.DOMove(_endPosition, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }
}
