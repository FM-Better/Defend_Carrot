using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// 场景管理器 在基础的场景管理器上封装了一些功能
public static class SceneMgr
{
    // 异步卸载场景
    public static void UnloadSceneAsync(string sceneName) => SceneManager.UnloadSceneAsync(sceneName);

    public static void UnloadSceneAsync(int sceneBuildIndex) => SceneManager.UnloadSceneAsync(sceneBuildIndex);

    // 根据场景名获取场景
    public static Scene GetSceneByName(string name) => SceneManager.GetSceneByName(name);

    // 获取当前活跃场景
    public static Scene GetActiveScene() => SceneManager.GetActiveScene();

    // 设置当前活跃场景
    public static void SetActiveScene(Scene scene) => SceneManager.SetActiveScene(scene);

    /// <summary>
    /// 同步加载场景
    /// </summary>
    /// <param name="sceneName"> 场景名称 </param>
    /// <param name="callBack"> 场景加载完毕要执行的回调函数 </param>
    public static void LoadScene(string sceneName, UnityAction callBack = null)
    {
        
        SceneManager.LoadScene(sceneName);
        callBack?.Invoke();
    }

    // 同步加载场景
    public static void LoadScene(int sceneBuildIndex, UnityAction callBack = null)
    {
        SceneManager.LoadScene(sceneBuildIndex);
        callBack?.Invoke();
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName"> 场景名称 </param>
    /// <param name="isDisplay"> 是否立即显示 </param>
    /// <param name="callBack"> 场景加载完毕要执行的回调函数 </param>

    public static void LoadSceneAsync(string sceneName, UnityAction callBack = null)
    {
        MonoMgr.Instance.StartCoroutine(LoadSceneCoroutine(sceneName, LoadSceneMode.Single, true, callBack));
    }

    public static void LoadSceneAsync(string sceneName,bool isDisplay = true, UnityAction callBack = null)
    {
        MonoMgr.Instance.StartCoroutine(LoadSceneCoroutine(sceneName, LoadSceneMode.Single, isDisplay, callBack));
    }

    public static void LoadSceneAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, bool isDisplay = true, UnityAction callBack = null)
    {
        MonoMgr.Instance.StartCoroutine(LoadSceneCoroutine(sceneName, mode, isDisplay, callBack));
    }

    // 异步加载场景
    public static void LoadSceneAsync(int sceneBuildIndex, UnityAction callBack = null)
    {
        MonoMgr.Instance.StartCoroutine(LoadSceneCoroutine(sceneBuildIndex, callBack));
    }

    // 配合异步加载的协程
    static IEnumerator LoadSceneCoroutine(string sceneName, LoadSceneMode mode = LoadSceneMode.Single, bool isDisplay = true, UnityAction callBack = null)
    {
        AsyncOperation ao;
        ao = SceneManager.LoadSceneAsync(sceneName, mode);
        ao.allowSceneActivation = isDisplay;

        while (!ao.isDone)
        {
            // 事件中心 广播场景加载事件 并传递加载进度
            EventCenter.Instance.BroadCast<string, float>(E_Event_Name.SceneLoading, sceneName, ao.progress);
            yield return ao.progress;
        }
        // 场景加载完毕后调用回调函数
        callBack?.Invoke();
    }

    // 配合异步加载的协程
    static IEnumerator LoadSceneCoroutine(int sceneBuildIndex, UnityAction callBack = null)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneBuildIndex);
        while (!ao.isDone)
        {
            // 事件中心 广播场景加载事件 并传递加载进度
            EventCenter.Instance.BroadCast<float>(E_Event_Name.SceneLoading, ao.progress);
            yield return ao.progress;
        }
        // 场景加载完毕后调用回调函数
        callBack?.Invoke();
    }
}
