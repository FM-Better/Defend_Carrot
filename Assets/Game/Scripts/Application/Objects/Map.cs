using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
            StartCoroutine(Tools.LoadImage(value, renderer));
        }
    }

    public string RoadImage
    {
        set
        {
            SpriteRenderer renderer = transform.Find("Road").GetComponent<SpriteRenderer>();
            StartCoroutine(Tools.LoadImage(value, renderer));
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

    #region 方法
    // 计算尺寸大小
    private void CalculateSize()
    {
        // 左下角的点的世界坐标
        Vector3 leftDown = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
        // 右上角的点的世界坐标
        Vector3 rightUp = Camera.main.ViewportToWorldPoint(new Vector3(1, 1));

        // 地图大小
        mapWidth = rightUp.x - leftDown.x;
        mapHeight = rightUp.y - leftDown.y;

        // 格子大小
        tileWidth = mapWidth / ColumnCount;
        tileHeight = mapHeight / RowCount;
    }

    // 得到格子中心点的世界坐标
    private Vector3 GetPosition(Tile tile)
    {
        return new Vector3(
                    -mapWidth * 2 + (tile.x + 0.5f) * tileWidth,
                    -mapHeight * 2 + (tile.y + 0.5f) * tileHeight,
                    0f
                );
    }

    // 根据x、y轴的索引获取格子
    private Tile GetTile(int tileX, int tileY)
    {
        int index = tileX + tileY * ColumnCount;

        if (index < 0 || index >= m_grid.Count)
        {
            Debug.Log("索引越界！");
            return null;
        }

        return m_grid[index];
    }

    // 得到鼠标所在的格子
    private Tile GetTileUnderMouse()
    {
        Vector3 mouseWorldPos = GetMouseWolrdPosition();
        int col = (int)((mouseWorldPos.x + mapWidth / 2) / tileWidth);
        int row = (int)((mouseWorldPos.y + mapHeight / 2) / tileHeight);
        return GetTile(col, row);
    }

    // 得到鼠标的世界坐标
    private Vector3 GetMouseWolrdPosition()
    {
        Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        return worldPos;
    }
    #endregion

    #region Unity回调函数
    private void OnDrawGizmos()
    {
        if (!drawGizmos)
            return;

        // 计算地图和格子的大小
        CalculateSize();

        // 绘制格子
        Gizmos.color = Color.green;
        // 绘制行线
        for (int row = 0; row <= RowCount; ++row)
        {
            Vector2 from = new Vector2(-mapWidth / 2, -mapHeight / 2 + row * tileHeight);
            Vector2 to = new Vector2(mapWidth / 2, -mapHeight / 2 + row * tileHeight);
            Gizmos.DrawLine(from, to);
        }
        // 绘制列线
        for (int col = 0; col <= ColumnCount; ++col)
        {
            Vector2 from = new Vector2(-mapWidth / 2 + col * tileWidth, -mapHeight / 2);
            Vector2 to = new Vector2(-mapWidth / 2 + col * tileHeight, mapHeight / 2);
            Gizmos.DrawLine(from, to);
        }

        // 绘制可放置点
        foreach (var item in m_grid)
        {
            if (item.canHold)
            {
                Vector3 center = GetPosition(item);
                Gizmos.DrawIcon(center, Consts.HolderGizemos, true);
            }
        }

        // 绘制可行走路线
        for (int i = 0; i < m_road.Count; i++)
        {
            // 起点
            if (i == 0)
            {
                Vector3 center = GetPosition(m_road[i]);
                Gizmos.DrawIcon(center, Consts.StartGizemos, true);
            }
            // 终点
            if (i > 0 && i == m_road.Count - 1)
            {
                Vector3 center = GetPosition(m_road[i]);
                Gizmos.DrawIcon(center, Consts.EndGizemos, true);
            }
            // 路径
            if (i > 0)
            {
                Vector3 from = GetPosition(m_road[i]);
                Vector3 to = GetPosition(m_road[i - 1]);
                Gizmos.DrawLine(from, to);
            }
        }
    }


    #endregion
}
