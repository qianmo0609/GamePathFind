using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FindPathAlgorithm
{
    protected int[,] mapData;
    protected int xCount;
    protected int zCount;
    protected Vector2Int halfLen;

    public FindPathAlgorithm(int[,] mapData,int xCount,int zCount) { 
        this.mapData = mapData;
        this.xCount = xCount;
        this.zCount = zCount;
        this.halfLen = new Vector2Int(xCount/2,zCount/2);
    }
    public abstract List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int goalPos);
}
