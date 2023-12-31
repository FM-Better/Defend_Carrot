﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

[CustomEditor(typeof(Map))]
public class MapEditor : Editor
{
    public Map map = null;

    private List<FileInfo> m_files = new List<FileInfo>();
    // 当前选中的配置文件的索引
    private int m_currentIndex = -1;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (Application.isPlaying)
        {
            // 关联Mono脚本
            map = target as Map;

            EditorGUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            int currentIndex = EditorGUILayout.Popup(m_currentIndex, GetFileNames());
            if (currentIndex != m_currentIndex)
            {
                m_currentIndex = currentIndex;

                LoadLevel();
            }
            if (GUILayout.Button("读取关卡文件"))
            {
                LoadLevelFiles();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("清除放置点"))
            {
                map.ClearHolders();
            }

            if (GUILayout.Button("清除路径"))
            {
                map.ClearRoad();
            }

            if (GUILayout.Button("清除回合信息"))
            {
                map.Level.rounds.Clear();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("保存数据"))
            {
                SaveLevel();
            }
            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

    /// <summary>
    /// 读取所有的关卡配置文件
    /// </summary>
    private void LoadLevelFiles()
    {
        // 清除之前的数据
        Clear();
        m_files = Tools.GetLevelFiles();

        if(m_files.Count > 0)
        {
            m_currentIndex=0;
            LoadLevel();
        }
    }

    private void Clear()
    {
        m_files.Clear();
        m_currentIndex = -1;
    }

    /// <summary>
    /// 读取选中的关卡配置文件
    /// </summary>
    private void LoadLevel()
    {
        FileInfo file = m_files[m_currentIndex];
        Level level = new Level();
        Tools.FillLevel(file.FullName, ref level);
        map.LoadLevel(level);
    }

    /// <summary>
    /// 保存关卡数据
    /// </summary>
    private void SaveLevel()
    {
        // 得到当前编辑的Level
        Level level = map.Level;

        List<Point> holders = new List<Point>();
        // 保存放置点信息
        foreach(var tile in map.Grid)
        {
            if (tile.canHold)
            {
                holders.Add(new Point(tile.x, tile.y));
            }
        }
        level.holders = holders;

        List<Point> path = new List<Point>();
        // 保存路径信息
        foreach (var tile in map.Road)
        {
            path.Add(new Point(tile.x, tile.y));
        }
        level.path = path;

        level.rounds = map.Level.rounds;

        // 保存关卡
        Tools.SaveLevel(m_files[m_currentIndex].FullName, level);

        // 弹窗提示
        EditorUtility.DisplayDialog("保存关卡数据", "保存成功", "确认");
    }

    // 得到关卡配置文件名字
    string[] GetFileNames() 
    {
        List<string> fileNames = new List<string>();
        foreach ( FileInfo file in m_files)
        {
            fileNames.Add( file.Name );
        }
        return fileNames.ToArray();
    }
}
