using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Mono管理器
public class MonoMgr : BaseManager<MonoMgr>
{
    // Mono控制器
    internal class MonoController : MonoBehaviour
    {
        // Update需要执行的事件
        private UnityAction updateAction;

        private void Start()
        {
            // 禁止销毁
            DontDestroyOnLoad(this);
        }

        // 执行Update
        private void Update() => updateAction?.Invoke();

        // 添加Update的事件
        public void AddExcute(UnityAction action) => updateAction += action;
        // 移除Update的事件
        public void RemoveExcute(UnityAction action) => updateAction -= action;
    }

    // 管理的Mono管理器
    private MonoController controller;

    // 保证MonoController的唯一性
    public MonoMgr()
    {
        // 跟着MonoMgr一同创建
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
    }

    #region 为外界提供协程使用的接口
    /// <summary>
    /// 开启协程
    /// </summary>
    /// <param name="methodName"> 协程名字 </param>
    public void StartCoroutine(string methodName) => controller.StartCoroutine(methodName);

    /// <summary>
    /// 开启协程
    /// </summary>
    /// <param name="routine"> 协程 </param>
    public void StartCoroutine(IEnumerator routine) => controller.StartCoroutine(routine);
    #endregion

    #region 为外界提供Update使用的接口
    /// <summary>
    /// 添加Update一个执行事件
    /// </summary>
    /// <param name="action"> 执行事件 </param>
    public void AddUpdateExcute(UnityAction action) => controller.AddExcute(action);

    /// <summary>
    /// 移除Update的一个执行事件
    /// </summary>
    /// <param name="action"> 执行事件 </param>
    public void RemoveUpdateExcute(UnityAction action) => controller.RemoveExcute(action);
    #endregion
}
