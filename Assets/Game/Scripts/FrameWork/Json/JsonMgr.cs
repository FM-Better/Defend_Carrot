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
public class JsonMgr
{
    private static JsonMgr instance = new JsonMgr();
    public static JsonMgr Instance => instance;

    private JsonMgr() { }

    /// <summary>
    /// 存储数据
    /// </summary>
    /// <param name="data"> 数据 </param>
    /// <param name="fileName"> 文件名 </param>
    /// <param name="type"> 选用的方法，默认采用LitJson</param>
    public void SaveData(object data, string fileName, JsonType type = JsonType.LitJson)
    {
        string path = Application.persistentDataPath + "/" + fileName + ".json";

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

        File.WriteAllText(path, jsonStr);
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="fileName"> 文件名 </param>
    /// <param name="type"> 选用的方法，默认为LitJson</param>
    /// <typeparam name="T"> 数据类型 </typeparam>
    /// <returns></returns>
    public T LoadData<T>(string fileName, JsonType type = JsonType.LitJson) where T : new()
    {
        // 首先从默认文件夹中（StreamingAsset）读取
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        // 如果没有 则从读写文件夹中读取
        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".json";
        // 如果还没有 则返回默认值
        if (!File.Exists(path))
            return new T();

        string jsonStr = File.ReadAllText(path);
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
}
