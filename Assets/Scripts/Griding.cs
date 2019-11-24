using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griding : MonoBehaviour {
    
    public LayerMask unwalkableMask;        // 只管在這層layer上的碰撞
    public Vector2 gridWorldSize;           // 格子世界大小 ( 似乎只能用正方形 )
    public float nodeRadius;                // 格子半徑 ( 邊長 / 2 )
    Node[,] grid;                           // 格子 ( 二維陣列儲存每格資料 )

    public List<Node> path;
    public GameObject wall;
    private Transform walls3D;

    float nodeDiameter;                     // 格子直徑 ( 邊長 )
    int gridSizeX, gridSizeY;               // 兩軸中各有幾格格子

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);       // 計算兩軸格子數 RoundToInt => float to int
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();                                                       // 畫格子
        DrawUnwalkableArea();
    }

    protected bool DetectWalkable(Vector3 wPoint, out RaycastHit2D hit)     // out keyword causes arguments to be parsed by reference
    {
        hit = Physics2D.Linecast(wPoint, wPoint, unwalkableMask);           // 抓unwalkableMask Layer起點到終點撞到的物件 ( 詹時先用linecast )

        if (hit.transform == null)      // 不會碰撞 ( "地圖"上為可行走區域 )
            return true;
        else                            // 有碰撞
            return false;
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];      // 產生一個m*n的二維陣列
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        // 計算出左下角格子的中心點位置

        RaycastHit2D hit;                           // 判斷是否可行走用的 hit = null => 可行走
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                // 計算出當前格子中心點位置
                bool walkable = DetectWalkable(worldPoint, out hit);    // 判斷是否可行走
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeighbours(Node node)          // 抓neighbours
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <=1; x++)
        {
            for (int y = -1; y <= 1; x++)
            {
                if (x == 0 && y == 0)                   // 中心點是自己跳過
                    continue;

                int checkX = node.gridX + x;            // 周圍格子的實際編號 ( 中間格子+1 -1 )
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)     // 不能超出格子範圍
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    // 根據拿到的 worldPosition 回傳 grid 中那一個 Node
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        worldPosition = worldPosition - this.transform.position;
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);     // 限制在0~1
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    private void DrawUnwalkableArea()
    {
        walls3D = new GameObject("walls3D").transform;
        walls3D.transform.SetParent(this.transform);
        walls3D.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        if (grid != null)
        {
            foreach (Node n in grid)
            {
                if (!n.walkable)
                {
                    Vector3 wallPos = new Vector3(n.worldPosition.x, n.worldPosition.y, n.worldPosition.z);
                    GameObject clone = Instantiate(wall, wallPos, Quaternion.Euler(Vector3.zero));
                    
                    clone.transform.SetParent(walls3D);
                }
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? new Color(1, 1, 1, 0.5f) : Color.red;
                if (path != null)
                {
                    if (path.Contains(n))
                        Gizmos.color = Color.black;
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.right * (nodeDiameter - 0.1f) + Vector3.up * (nodeDiameter - 0.1f));
            }
        }
    }

}
