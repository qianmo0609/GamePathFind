using System.Collections.Generic;
using UnityEngine;

public class DataNode
{
    public Vector2Int pos;
    public DataNode parent;

    //A*ʹ��
    public int gCost = 999999999;
    public int hCost;
    public int fCost;

    public DataNode(Vector2Int pos, DataNode parent)
    {
        this.pos = pos;
        this.parent = parent;
    }
    
    //A*ʹ�ü���F
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}

