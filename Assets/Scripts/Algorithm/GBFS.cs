using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBFS : FindPathAlgorithm
{
    public GBFS(int[,] mapData, int xCount, int zCount) : base(mapData, xCount, zCount){}

    public override List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int goalPos)
    {
        List<Vector2Int> nodes = this.GBFSFind(startPos, goalPos);
        if (nodes == null)
        {
            Debug.LogError("寻路有误，请检查参数是否正确");
            return null;
        }
        return nodes;
    }

    List<Vector2Int> GBFSFind(Vector2Int startPos, Vector2Int goalPos)
    {
        //存储要检测的点
        List<DataNode> frontier = new List<DataNode>();
        //存储已经检测的点
        List<Vector2Int> reached = new List<Vector2Int>();

        //存储路径点
        List<Vector2Int> path = new List<Vector2Int>();
        DataNode startNode = new DataNode(startPos, null);
        startNode.hCost = 0;
        frontier.Add(startNode);
        reached.Add(startPos);

        while (frontier.Count > 0)
        {
            DataNode currentNode = GetLowestgCostNode(frontier);
            path.Add(currentNode.pos);

            if (currentNode.pos == goalPos)
            {
                return path;
            }

            frontier.Clear();

            List<DataNode> neighbors = GetNeighbors(currentNode.pos, reached);
            foreach (DataNode neighbourNode in neighbors)
            {
                neighbourNode.parent = currentNode;
                neighbourNode.hCost = CalculateDistanceCost(goalPos, neighbourNode.pos);

                if (!frontier.Contains(neighbourNode))
                {
                    frontier.Add(neighbourNode);
                }
                reached.Add(neighbourNode.pos);
            }
        }
        return path;
    }

    

    List<DataNode> GetNeighbors(Vector2Int current, List<Vector2Int> reached)
    {
        List<DataNode> neighbors = new List<DataNode>();
        for (int i = 0; i < Utils.pointDir.Count; i++)
        {
            Vector2Int neighbor = current + Utils.pointDir[i];
            if (this.IsCanAdd(neighbor, reached))
            {
                neighbors.Add(new DataNode(neighbor, null));
            }
        }
        return neighbors;
    }

    bool IsCanAdd(Vector2Int current, List<Vector2Int> reached)
    {
        if (reached.Contains(current))
            return false;
        if (current.x >= 0 && current.y >= 0 && current.x < xCount && current.y < zCount)
        {
            //如果是障碍物，则不能被Add
            if (this.mapData[current.y, current.x] == 1)
            {
                return false;
            }
            return true;
        }
        return false;
    }

    private int CalculateDistanceCost(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private DataNode GetLowestgCostNode(List<DataNode> pathNodeList)
    {
        DataNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].hCost < lowestFCostNode.hCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
