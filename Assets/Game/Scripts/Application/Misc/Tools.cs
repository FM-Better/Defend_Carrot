using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 工具类
/// </summary>
public static class Tools
{
    // 读取关卡的配置文件
    public static List<FileInfo> GetLevelFiles()
    {
        List<FileInfo> files = new List<FileInfo>();

        return files;
    }

    // 填充关卡
    public static void FillLevel()
    {

    }

    // 保存关卡信息
    public static void SaveLevel()
    {

    }

    // 加载图片
    public static IEnumerator LoadImage(string url, SpriteRenderer renderer)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sp = Sprite.Create(
                                texture,
                                new Rect(0, 0, texture.width, texture.height),
                                Vector2.one * 0.5f
                            );
                renderer.sprite = sp;
            }
            else
            {
                Debug.Log("下载失败: " + webRequest.error);
            }
        }
    }
}
