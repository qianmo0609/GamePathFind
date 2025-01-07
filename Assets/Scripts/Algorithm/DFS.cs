using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS : FindPathAlgorithm
{
    public DFS(int[,] mapData, int xCount, int zCount) : base(mapData, xCount, zCount){}

    public override List<Vector2Int> FindPath(Vector2Int startPos, Vector2Int goalPos)
    {
        DataNode dataNode = this.DFSFind(startPos, goalPos);
        if (dataNode == null)
        {
            Debug.LogError("寻路有误，请检查参数是否正确");
            return null;
        }
        return Utils.GetPath(dataNode);
    }


    public DataNode DFSFind(Vector2Int startPos, Vector2Int goalPos)
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

            Vector2Int nextNodePos = GetNextNode(currentNode.pos, reached);
            //如果没有找到合适的子节点,就将当前节点的父节点加入队列
            if(nextNodePos.x >= 99999999)
            {
                frontier.Enqueue(currentNode.parent);
            }
            else
            {
                frontier.Enqueue(new DataNode(nextNodePos, currentNode));
                reached.Add(nextNodePos);
            }
        }
        return null;
    }

    Vector2Int GetNextNode(Vector2Int current, List<Vector2Int> reached) {
        for (int i = 0; i < Utils.pointDir.Count; i++)
        {
            Vector2Int neighbor = current + Utils.pointDir[i];
            if (this.IsCanAdd(neighbor, reached))
            {
                return neighbor;
            }
        }
        return Vector2Int.one * int.MaxValue;
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
}
