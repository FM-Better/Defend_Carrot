using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManagerMono<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("是否禁止销毁")]
    [SerializeField] private bool dontDestory;

    private static T instance;
    public static T Instance => instance;

    private void Awake()
    {
        instance = this as T;

        Init();
    }

    protected virtual void Init() 
    {
        if (dontDestory)
            GameObject.DontDestroyOnLoad(gameObject);
    }
}
