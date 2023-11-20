using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class BinaryDataMgr : BaseManager<BinaryDataMgr>
{
    // Excel生成的2进制数据文件夹路径
    public static string BINARY_DATA_PATH = Application.streamingAssetsPath + "/Binary/";
    // 2进制数据存储的位置
    private static string SAVE_PATH = Application.persistentDataPath + "/Data/";

    // 存储所有Excel表数据的容器
    private Dictionary<string, object> tableDic = new Dictionary<string, object>();

    /// <summary>
    /// 以2进制格式存储数据
    /// </summary>
    /// <param name="data"> 要存储的数据 </param>
    /// <param name="fileName"> 存储到的文件名 </param> 
    public void Save(object data, string fileName)
    {
        if (!Directory.Exists(SAVE_PATH))
            Directory.CreateDirectory(SAVE_PATH);

        using (FileStream fs = new FileStream(SAVE_PATH + fileName + ".txt", FileMode.OpenOrCreate, FileAccess.Write))
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, data);
            fs.Dispose();
        }
    }

    /// <summary>
    /// 读取Excel表的2进制数据
    /// </summary>
    /// <typeparam name="T"> 容器类的类型 </typeparam>
    /// <typeparam name="K"> 数据结构类的类型</typeparam>
    /// <returns></returns>
    public void LoadTable<T, K>()
    {
        using (FileStream fs = File.Open(BINARY_DATA_PATH + typeof(K).Name + ".txt", FileMode.Open, FileAccess.Read))
        {
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);
            fs.Close();
            // 记录当前偏移量
            int nowOffset = 0;
            // 要读取的行数
            int rowCount = BitConverter.ToInt32(bytes, nowOffset);
            nowOffset += sizeof(int);

            // 读取key
            int keyLength = BitConverter.ToInt32(bytes, nowOffset);
            nowOffset += sizeof(int);
            string keyName = Encoding.UTF8.GetString(bytes, nowOffset, keyLength);
            nowOffset += keyLength;

            // 创建容器对象
            Type containerType = typeof(T);
            object containerObj = Activator.CreateInstance(containerType);
            // 得到数据结构类的Type
            Type classType = typeof(K);
            // 得到数据结构类的所有字段信息
            FieldInfo[] infos = classType.GetFields();

            // 读取每一行
            for (int i = 0; i < rowCount; ++i)
            {
                // 实例化数据结构类的对象
                object dataObj = Activator.CreateInstance(classType);
                // 遍历每个字段
                foreach (FieldInfo info in infos)
                {
                    if (info.FieldType == typeof(int))
                    {
                        info.SetValue(dataObj, BitConverter.ToInt32(bytes, nowOffset));
                        nowOffset += sizeof(int);
                    }
                    else if (info.FieldType == typeof(float))
                    {
                        info.SetValue(dataObj, BitConverter.ToSingle(bytes, nowOffset));
                        nowOffset += sizeof(float);
                    }
                    else if (info.FieldType == typeof(bool))
                    {
                        info.SetValue(dataObj, BitConverter.ToBoolean(bytes, nowOffset));
                        nowOffset += sizeof(bool);
                    }
                    else if (info.FieldType == typeof(string))
                    {
                        int length = BitConverter.ToInt32(bytes, nowOffset);
                        nowOffset += sizeof(int);
                        info.SetValue(dataObj, Encoding.UTF8.GetString(bytes, nowOffset, length));
                        nowOffset += length;
                    }
                }

                // 得到容器对象中的字典
                object dicObj = containerType.GetField("dataDic").GetValue(containerObj);
                // 找到字典的Add方法
                MethodInfo methodInfo = dicObj.GetType().GetMethod("Add");
                // 得到Key
                object keyValue = classType.GetField(keyName).GetValue(dataObj);
                methodInfo.Invoke(dicObj, new object[] { keyValue, dataObj });
            }

            // 记录读取完的表
            tableDic.Add(typeof(T).Name, containerObj);
        }
    }

    /// <summary>
    /// 得到一张表的数据
    /// </summary>
    /// <typeparam name="T"> 容器类名 </typeparam>
    /// <returns></returns>
    public T GetTable<T>() where T : class
    {
        string tableName = typeof(T).Name;
        if (tableDic.ContainsKey(tableName))
            return tableDic[tableName] as T;

        return null;
    }

    /// <summary>
    /// 读取2进制数据文件并转为数据结构类对象
    /// </summary>
    /// <typeparam name="T"> 数据结构类型 </typeparam>
    /// <param name="fileName"> 2进制数据文件名 </param>
    /// <returns></returns>
    public T Load<T>(string fileName) where T : class
    {
        if (!File.Exists(SAVE_PATH + fileName + ".txt"))
            return default(T);

        T obj;
        using (FileStream fs = File.Open(SAVE_PATH + fileName + ".txt", FileMode.Open, FileAccess.Read))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            obj = binaryFormatter.Deserialize(fs) as T;
            fs.Close();
        }
        return obj;
    }
}
