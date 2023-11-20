using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Excel;
using UnityEditor;
using UnityEngine;

public class ExcelTool
{
    // Excel文件夹路径
    private static string EXCEL_PATH = Application.dataPath + "/Game/Config/Excel/";
    // 生成的数据结构类文件夹路径
    private static string DATA_CLASS_PATH = Application.dataPath + "/Game/Scripts/ExcelData/DataClass/";
    // 生成的数据结构容器类文件夹路径
    private static string DATA_CONTAINER_PATH = Application.dataPath + "/Game/Scripts/ExcelData/Container/";
    // 数据内容开始索引
    private const int DATA_BEGIN_INDEX = 4;

    [MenuItem("GameTool/ExcelToInfo")]
    public static void GenerateExcelInfo()
    {
        // 创建Excel文件夹并得到
        DirectoryInfo directoryInfo = Directory.CreateDirectory(EXCEL_PATH);
        // 得到文件夹下的所有文件信息
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        // 数据表容器
        DataTableCollection tableCollection;
        string extension;
        for (int i = 0; i < fileInfos.Length; ++i)
        {
            // 得到文件后缀
            extension = fileInfos[i].Extension;
            // 如果不是Excel文件 则跳过
            if (extension != ".xlsx" && extension != ".xls")
                continue;

            // 打开Excel文件 得到其中所有表的数据
            using (FileStream fs = fileInfos[i].Open(FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                tableCollection = excelDataReader.AsDataSet().Tables;
                fs.Close();
            }

            // 遍历Excel的所有表
            foreach (DataTable table in tableCollection)
            {
                // 生成数据结构类
                GenerateDataClass(table);
                // 生成数据结构容器类
                GenerateContainer(table);
                // 生成2进制数据
                GenerateBinaryDataFile(table);
            }
        }
    }

    // 得到变量名的数据行
    private static DataRow GetVariableNameRow(DataTable table) => table.Rows[1];
    // 得到变量类型的数据行
    private static DataRow GetVariableTypeRow(DataTable table) => table.Rows[2];

    // 得到键的索引值
    private static int GetKeyIndex(DataTable table)
    {
        DataRow row = table.Rows[3];
        for (int i = 0; i < table.Columns.Count; ++i)
            if (row[i].ToString() == "key")
                return i;

        return 0;
    }

    // 根据表的信息 生成对应的数据结构类
    private static void GenerateDataClass(DataTable table)
    {
        // 变量名行
        DataRow rowName = GetVariableNameRow(table);
        // 变量类型行
        DataRow rowType = GetVariableTypeRow(table);

        // 判断是否存在数据结构类文件夹 若无则创建
        if (!Directory.Exists(DATA_CLASS_PATH))
            Directory.CreateDirectory(DATA_CLASS_PATH);

        // 进行数据结构类内容的书写
        string content = "public class " + table.TableName + "\n{\n";
        for (int i = 0; i < table.Columns.Count; ++i)
            content += "\tpublic " + rowType[i].ToString() + " " + rowName[i].ToString() + ";\n";
        content += "}\n";

        // 将内容写入到cs脚本中
        File.WriteAllText(DATA_CLASS_PATH + table.TableName + ".cs", content);
        // 刷新Project窗口
        AssetDatabase.Refresh();
    }

    // 根据表的信息 生成对应的数据结构容器类
    private static void GenerateContainer(DataTable table)
    {
        // 得到键的索引
        int keyIndex = GetKeyIndex(table);
        // 变量类型行
        DataRow rowType = GetVariableTypeRow(table);
        // 判断是否存在Excel数据结构容器类文件夹 若无则创建
        if (!Directory.Exists(DATA_CONTAINER_PATH))
            Directory.CreateDirectory(DATA_CONTAINER_PATH);

        // 进行数据结构类内容的书写
        string content = "using System.Collections.Generic;\n\n";
        content += "public class " + table.TableName + "Container\n{\n";
        content += "\tpublic Dictionary<" + rowType[keyIndex].ToString() + ", " + table.TableName + "> ";
        content += "dataDic = new Dictionary<" + rowType[keyIndex].ToString() + ", " + table.TableName + ">();\n}\n";

        // 将内容写入到cs脚本中
        File.WriteAllText(DATA_CONTAINER_PATH + table.TableName + "Container.cs", content);
        // 刷新Project窗口
        AssetDatabase.Refresh();
    }

    // 根据表的信息 生成对应的2进制数据文件
    private static void GenerateBinaryDataFile(DataTable table)
    {
        // 判断是否存在Excel2进制数据文件夹 若无则创建
        if (!Directory.Exists(BinaryDataMgr.BINARY_DATA_PATH))
            Directory.CreateDirectory(BinaryDataMgr.BINARY_DATA_PATH);

        // 创建一个2进制文件进行写入
        using (FileStream fs = new FileStream(BinaryDataMgr.BINARY_DATA_PATH + table.TableName + ".txt", FileMode.OpenOrCreate, FileAccess.Write))
        {
            // 从数据内容开始读取
            fs.Write(BitConverter.GetBytes(table.Rows.Count - DATA_BEGIN_INDEX), 0, sizeof(int));

            // 存储key的变量名
            string keyName = GetVariableNameRow(table)[GetKeyIndex(table)].ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(keyName);
            // 先写入字符串长度
            fs.Write(BitConverter.GetBytes(bytes.Length), 0, sizeof(int));
            // 再写入字符串
            fs.Write(bytes, 0, bytes.Length);

            // 记录每一行数据
            DataRow row;
            // 得到变量类型行
            DataRow rowType = GetVariableTypeRow(table);
            for (int i = DATA_BEGIN_INDEX; i < table.Rows.Count; ++i)
            {
                row = table.Rows[i];
                for (var j = 0; j < table.Columns.Count; ++j)
                {
                    switch (rowType[j].ToString())
                    {
                        case "int":
                            fs.Write(BitConverter.GetBytes(int.Parse(row[j].ToString())), 0, sizeof(int));
                            break;
                        case "float":
                            fs.Write(BitConverter.GetBytes(float.Parse(row[j].ToString())), 0, sizeof(float));
                            break;
                        case "bool":
                            fs.Write(BitConverter.GetBytes(bool.Parse(row[j].ToString())), 0, sizeof(bool));
                            break;
                        case "string":
                            bytes = Encoding.UTF8.GetBytes(row[j].ToString());
                            fs.Write(BitConverter.GetBytes(bytes.Length), 0, sizeof(int));
                            fs.Write(bytes, 0, bytes.Length);
                            break;
                    }
                }
            }

            fs.Dispose();
        }

        AssetDatabase.Refresh();
    }
}
