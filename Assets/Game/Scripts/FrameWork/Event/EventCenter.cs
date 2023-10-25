using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum E_Event_Name
{
    SceneLoadBegin,
    SceneLoading,
}

// 事件中心 用来注册、广播事件消息
public class EventCenter : BaseManager<EventCenter>
{
    #region 事件相关
    // 用里氏转换来装不同事件类型的空接口
    public interface IEventInfo { }

    // 不带参数的监听事件
    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }

    // 带参数的监听事件
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;

        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }

    // 带参数的监听事件
    public class EventInfo<T0, T1> : IEventInfo
    {
        public UnityAction<T0, T1> actions;

        public EventInfo(UnityAction<T0, T1> action)
        {
            actions += action;
        }
    }
    #endregion

    // 存储事件以及对应的操作
    private Dictionary<E_Event_Name, IEventInfo> eventDic = new Dictionary<E_Event_Name, IEventInfo>();

    /// <summary>
    /// 为事件添加一个监听
    /// </summary>
    /// <param name="eventName"> 事件名 </param>
    /// <param name="action"> 监听函数 </param>
    public void AddListener(E_Event_Name eventName, UnityAction action)
    {
        // 如果当前事件中心中已有该事件 直接添加该事件的该监听
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).actions += action;
        }
        // 否则添加该事件 并将监听传入
        else
        {
            eventDic.Add(eventName, new EventInfo(action));
        }
    }

    /// <summary>
    /// 为事件添加带一个参数的一个监听
    /// </summary>
    /// <param name="eventName"> 事件名 </param>
    /// <param name="action"> 监听函数 </param>
    /// <typeparam name="T"> 参数类型 </typeparam>
    public void AddListener<T>(E_Event_Name eventName, UnityAction<T> action)
    {
        // 如果当前事件中心中已有该事件 直接添加该事件的该监听
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).actions += action;
        }
        // 否则添加该事件 并将监听传入
        else
        {
            eventDic.Add(eventName, new EventInfo<T>(action));
        }
    }

    /// <summary>
    /// 为事件添加带两个参数的一个监听
    /// </summary>
    /// <param name="eventName"> 事件名 </param>
    /// <param name="action"> 监听函数 </param>
    /// <typeparam name="T"> 参数类型 </typeparam>
    public void AddListener<T0, T1>(E_Event_Name eventName, UnityAction<T0, T1> action)
    {
        // 如果当前事件中心中已有该事件 直接添加该事件的该监听
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T0, T1>).actions += action;
        }
        // 否则添加该事件 并将监听传入
        else
        {
            eventDic.Add(eventName, new EventInfo<T0, T1>(action));
        }
    }

    /// <summary>
    /// 为事件移除一个监听
    /// </summary>
    /// <param name="eventName"> 事件 </param>
    /// <param name="action"> 监听函数 </param>
    public void RemoveListener(E_Event_Name eventName, UnityAction action)
    {
        // 如果当前事件中心中已有该事件 直接移除该事件的该监听
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).actions -= action;
        }
    }

    /// <summary>
    /// 为事件移除带一个参数的一个监听
    /// </summary>
    /// <param name="eventName"> 事件名</param>
    /// <param name="action"> 监听函数 </param>
    /// <typeparam name="T"> 参数类型 </typeparam>
    public void RemoveListener<T>(E_Event_Name eventName, UnityAction<T> action)
    {
        // 如果当前事件中心中已有该事件 直接移除该事件的该监听
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).actions -= action;
        }
    }

    /// <summary>
    /// 为事件移除带两个参数的一个监听
    /// </summary>
    /// <param name="eventName"> 事件名</param>
    /// <param name="action"> 监听函数 </param>
    /// <typeparam name="T"> 参数类型 </typeparam>
    public void RemoveListener<T0, T1>(E_Event_Name eventName, UnityAction<T0, T1> action)
    {
        // 如果当前事件中心中已有该事件 直接移除该事件的该监听
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T0, T1>).actions -= action;
        }
    }

    /// <summary>
    /// 广播事件
    /// </summary>
    /// <param name="eventName"> 事件名 </param>
    public void BroadCast(E_Event_Name eventName)
    {
        // 如果当前事件中心中已有该事件 触发（调用）该事件的所有监听
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo).actions?.Invoke();
        }
    }

    /// <summary>
    /// 广播带一个参数的事件
    /// </summary>
    /// <param name="eventName"> 事件名 </param>
    /// <param name="info"> 参数 </param>
    /// <typeparam name="T"> 参数类型 </typeparam>
    public void BroadCast<T>(E_Event_Name eventName, T info)
    {
        // 如果当前事件中心中已有该事件 触发（调用）该事件的所有监听
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T>).actions?.Invoke(info);
        }
    }

    /// <summary>
    /// 广播带两个个参数的事件
    /// </summary>
    /// <param name="eventName"> 事件名 </param>
    /// <param name="info"> 参数 </param>
    /// <typeparam name="T"> 参数类型 </typeparam>
    public void BroadCast<T0, T1>(E_Event_Name eventName, T0 info0, T1 info1)
    {
        // 如果当前事件中心中已有该事件 触发（调用）该事件的所有监听
        if (eventDic.ContainsKey(eventName))
        {
            (eventDic[eventName] as EventInfo<T0, T1>).actions?.Invoke(info0, info1);
        }
    }

    /// <summary>
    /// 清空事件中心 用于切换场景等
    /// </summary>
    public void Clear()
    {
        eventDic.Clear();
    }
}
