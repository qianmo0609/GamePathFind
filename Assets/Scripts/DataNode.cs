using System.Collections.Generic;
using UnityEngine;

public class DataNode
{
    public Vector2Int pos;
    public DataNode parent;

    //A*使用
    public int gCost = 999999999;
    public int hCost;
    public int fCost;

    public DataNode(Vector2Int pos, DataNode parent)
    {
        this.pos = pos;
        this.parent = parent;
    }
    
    //A*使用计算F
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}

