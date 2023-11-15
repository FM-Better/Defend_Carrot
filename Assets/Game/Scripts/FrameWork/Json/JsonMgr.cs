using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

// 使用Json时采用的方法类型
public enum JsonType
{
    JsonUtility,
    LitJson
}

// Json管理类
public class JsonMgr : BaseManager<JsonMgr>
{
    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="data"> 数据 </param>
    /// <param name="fileName"> 文件名 </param>
    /// <param name="type"> 选用的方法，默认采用LitJson </param>
    public void SaveData(object data, string fileName, JsonType type = JsonType.LitJson)
    {
        string jsonStr = "";
        switch (type)
        {
            case JsonType.JsonUtility:
                jsonStr = JsonUtility.ToJson(data);
                break;
            case JsonType.LitJson:
                jsonStr = JsonMapper.ToJson(data);
                break;
        }

        File.WriteAllText(fileName, jsonStr);
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="fileName"> 文件名 </param>
    /// <param name="type"> 选用的方法，默认为LitJson </param>
    /// <typeparam name="T"> 数据类型 </typeparam>
    /// <returns> 返回该数据 </returns>
    public T LoadData<T>(string fileName, JsonType type = JsonType.LitJson) where T : new()
    {
        // 如果还没有 则返回默认值
        if (!File.Exists(fileName))
            return new T();

        string jsonStr = File.ReadAllText(fileName);
        // 要返回的数据
        T data = default(T);
        switch (type)
        {
            case JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
        }

        return data;
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <typeparam name="T"> 数据类型 </typeparam>
    /// <param name="fileName"> 文件名 </param>
    /// <param name="data"> 读取到的数据 </param>
    /// <param name="type"> 选用的方法，默认为LitJson </param>
    /// <returns> 是否有该文件 </returns>
    public bool TryLoadData<T>(string fileName, out T data, JsonType type = JsonType.LitJson) where T : new()
    {
        // 如果还没有 则返回默认值
        if (!File.Exists(fileName))
        {
            data = new T();
            return false;
        }

        string jsonStr = File.ReadAllText(fileName);
        // 要返回的数据
        data = default(T);
        switch (type)
        {
            case JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(jsonStr);
                break;
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonStr);
                break;
        }

        return true;
    }
}
