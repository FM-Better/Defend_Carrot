using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    #region 常量
    public const int RowCount = 8;
    public const int ColumnCount = 12;
    #endregion

    #region 私有字段
    private float mapWidth;
    private float mapHeight;
    private float tileWidth;
    private float tileHeight;

    private List<Tile> m_grid = new List<Tile>();
    private List<Tile> m_road = new List<Tile>();

    private Level m_level;

    public bool drawGizmos = true;
    #endregion

    #region 共有属性
    public Level level => m_level;

    public string BackgroundImage
    {
        set
        {
            SpriteRenderer renderer = transform.Find("Background").GetComponent<SpriteRenderer>();
            // 加载图片
        }
    }

    public string RoadImage
    {
        set
        {
            SpriteRenderer renderer = transform.Find("Road").GetComponent<SpriteRenderer>();
            // 加载图片
        }
    }

    public List<Tile> Grid => m_grid;
    public List<Tile> Road => m_road;

    // 行走路径
    public Vector3[] Path
    {
        get
        {
            List<Vector3> path = new List<Vector3>();
            foreach (var item in m_road)
            {
                Tile tile = item;
                //TODO: 根据Tile得到所在点
                Vector3 point = Vector3.zero;
                path.Add(point);
            }
            return path.ToArray();
        }
    }
    #endregion


}
