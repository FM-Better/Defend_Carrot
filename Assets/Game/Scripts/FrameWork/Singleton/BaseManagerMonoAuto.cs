using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManagerMonoAuto<T> : MonoBehaviour where T : MonoBehaviour
{
    [Header("是否禁止销毁")]
    [SerializeField] private bool dontDestory;

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject();
                instance = go.AddComponent<T>();
                go.name = typeof(T).Name;
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (dontDestory)
            GameObject.DontDestroyOnLoad(gameObject);
    }
}
