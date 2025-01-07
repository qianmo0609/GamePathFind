using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPathAlgorithm : FindPathAlgorithm
{
    public TestPathAlgorithm(int[,] mapData, int xCount, int zCount) : base(mapData, xCount, zCount){}

    public override List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int goalPos)
    {
        return new List<Vector2Int> { new Vector2Int(0, 0), new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(2, 1) };
    }
}
