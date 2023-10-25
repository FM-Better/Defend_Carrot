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
        // ѭ���Ĵ�ͷ�ƶ� ���Ի���
        transform.DOMove(_endPosition, duration)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);
    }
}
