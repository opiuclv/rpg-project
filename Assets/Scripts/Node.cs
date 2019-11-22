using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

    public bool walkable;           // 此格可行走
    public Vector3 worldPosition;   // ....中心位置
    public int gridX;               // ....在二維陣列中編號
    public int gridY;

    public int gCost;               // 與起始位置的距離 gcost 上下左右 10 斜向 14 ( 斜向可能要小心其中一種狀況以免卡點 或拿來當能卡怪的點? )
    public int hCost;               // 與終點位置的距離 hcost 上下左右 10 斜向 14 ( 有看到只走4個方向 不考慮斜的 不確定影響多大 )
    public Node parent;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
