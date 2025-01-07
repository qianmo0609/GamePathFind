using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Dijkstra : FindPathAlgorithm
{
    public Dijkstra(int[,] mapData, int xCount, int zCount) : base(mapData, xCount, zCount){}

    public override List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int goalPos)
    {
        DataNode dataNode = this.DijkstraFind(startPos, goalPos);
        if (dataNode == null)
        {
            Debug.LogError("寻路有误，请检查参数是否正确");
            return null;
        }
        return Utils.GetPath(dataNode);
    }

    DataNode DijkstraFind(Vector2Int startPos, Vector2Int goalPos)
    {
        //存储要检测的点
        List<DataNode> frontier = new List<DataNode>();
        //存储已经检测的点
        List<Vector2Int> reached = new List<Vector2Int>();

        DataNode startNode = new DataNode(startPos, null);
        startNode.gCost = 0;
        frontier.Add(startNode);
        reached.Add(startPos);

        while (frontier.Count > 0)
        {
            DataNode currentNode = GetLowestgCostNode(frontier);
            frontier.Remove(currentNode);
            if (currentNode.pos == goalPos)
            {
                Debug.Log("完成！！！");
                return new DataNode(goalPos, currentNode.parent);
            }

            List<DataNode> neighbors = GetNeighbors(currentNode.pos, reached);
            foreach (DataNode neighbourNode in neighbors)
            {
                if (!frontier.Contains(neighbourNode))
                {
                    neighbourNode.parent = currentNode;
                    frontier.Add(neighbourNode);
                }
                reached.Add(neighbourNode.pos);
            }

            this.UpdateCost(frontier, currentNode);
        }

        return null;
    }

    //更新成本值
    void UpdateCost(List<DataNode> nodes, DataNode currentNode)
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            int newCost = currentNode.gCost + CalculateDistanceCost(nodes[i].pos, currentNode.pos);
            if (nodes[i].gCost > newCost)
            {
                nodes[i].gCost = newCost;
            }
        }
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
        if (current.x >= 0 && current.y >= 0 && current.x < MapData.m_MapData.GetLength(1) && current.y < MapData.m_MapData.GetLength(0))
        {
            //如果是障碍物，则不能被Add
            if (MapData.m_MapData[current.y, current.x] == 1)
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
            if (pathNodeList[i].gCost < lowestFCostNode.gCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
