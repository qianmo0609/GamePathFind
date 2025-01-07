using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : FindPathAlgorithm
{
    public BFS(int[,] mapData, int xCount, int zCount) : base(mapData, xCount, zCount) { }

    public override List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int goalPos)
    {
        DataNode dataNode = this.BFSFind(startPos, goalPos);
        if(dataNode == null)
        {
            Debug.LogError("寻路有误，请检查参数是否正确");
            return null;
        }
        //Utils.DisplayData(path);
        return Utils.GetPath(dataNode);
    }

    DataNode BFSFind(Vector2Int startPos, Vector2Int goalPos)
    {
        //存储要检测的点
        Queue<DataNode> frontier = new Queue<DataNode>();
        //存储已经检测的点
        List<Vector2Int> reached = new List<Vector2Int>();

        frontier.Enqueue(new DataNode(startPos, null));
        reached.Add(startPos);

        while (frontier.Count > 0)
        {
            DataNode currentNode = frontier.Dequeue();
            if (currentNode.pos == goalPos)
            {
                return new DataNode(goalPos, currentNode.parent);
            }
            List<Vector2Int> neighbors = GetNeighbors(currentNode.pos, reached);
            foreach (Vector2Int p in neighbors)
            {
                if (this.mapData[p.y,p.x] != 1)
                {
                    frontier.Enqueue(new DataNode(p, currentNode));
                }
                //增加已检测点
                reached.Add(p);
            }
        }
        return null;
    }

    List<Vector2Int> GetNeighbors(Vector2Int current, List<Vector2Int> reached)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();
        for (int i = 0; i < Utils.pointDir.Count; i++)
        {
            Vector2Int neighbor = current + Utils.pointDir[i];
            if (this.IsCanAdd(neighbor, reached))
            {
                neighbors.Add(neighbor);
            }
        }
        return neighbors;
    }

    bool IsCanAdd(Vector2Int current, List<Vector2Int> reached)
    {
        if (reached.Contains(current))
            return false;
        if (current.x >= 0 && current.y >= 0 && current.x < xCount && current.y < zCount)
            return true;
        return false;
    }
}
