using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AStar : FindPathAlgorithm
{
    public AStar(int[,] mapData, int xCount, int zCount) : base(mapData, xCount, zCount){}

    public override List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int goalPos)
    {
        DataNode dataNode = this.AStarFind(startPos, goalPos);
        if (dataNode == null)
        {
            Debug.LogError("寻路有误，请检查参数是否正确");
            return null;
        }
        return Utils.GetPath(dataNode);
    }
    
    DataNode AStarFind(Vector2Int startPos, Vector2Int goalPos)
    {
        //存储要检测的点
        List<DataNode> frontier = new List<DataNode>();
        //存储已经检测的点
        List<Vector2Int> reached = new List<Vector2Int>();

        DataNode startNode = new DataNode(startPos,null);
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startPos, goalPos);
        startNode.CalculateFCost();
        frontier.Add(startNode);

        while (frontier.Count > 0)
        {
            DataNode currentNode = GetLowestFCostNode(frontier);
            if (currentNode.pos == goalPos)
            {
                return new DataNode(goalPos, currentNode.parent);
            }

            frontier.Remove(currentNode);
            reached.Add(currentNode.pos);

            List<DataNode> neighbors = GetNeighbors(currentNode.pos, reached);
            foreach (DataNode neighbourNode in neighbors)
            {
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode.pos, neighbourNode.pos);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.parent = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode.pos, goalPos);
                    neighbourNode.CalculateFCost();

                    if (!frontier.Contains(neighbourNode))
                    {
                        frontier.Add(neighbourNode);
                    }
                }
            }
        }
        return null;
    }

    List<DataNode> GetNeighbors(Vector2Int current, List<Vector2Int> reached)
    {
        List<DataNode> neighbors = new List<DataNode>();
        for (int i = 0; i < Utils.pointDir.Count; i++)
        {
            Vector2Int neighbor = current + Utils.pointDir[i];
            if (this.IsCanAdd(neighbor, reached))
            {
                neighbors.Add(new DataNode(neighbor,null));
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

    private DataNode GetLowestFCostNode(List<DataNode> pathNodeList)
    {
        DataNode lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
