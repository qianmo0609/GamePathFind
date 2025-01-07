using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static List<Vector2Int> pointDir = new List<Vector2Int> { 
      new Vector2Int (0, -1),
      new Vector2Int (1, -1),
      new Vector2Int (1, 0),
      new Vector2Int (1, 1),
      new Vector2Int (0, 1),
      new Vector2Int (-1, 1),
      new Vector2Int (-1, 0),
      new Vector2Int (-1, -1),
    };

    public static void DisplayData(List<Vector2Int> data)
    {
        for(int i = 0; i < data.Count; i++)
        {
            Debug.Log($"{data[i].x},{data[i].y}");
        }
    }

    public static void DisplayData(List<DataNode> data)
    {
        string s = "";
        for (int i = 0; i < data.Count; i++)
        {
            //Debug.Log($"{data[i].pos.x},{data[i].pos.y}");
            s += $"{data[i].pos.x},{data[i].pos.y}  |";
        }
        Debug.Log(s);
    }

    public static List<Vector2Int> GetPath(DataNode dataNode)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        path.Add(dataNode.pos);
        while (dataNode.parent != null)
        {
            path.Add(dataNode.parent.pos);
            dataNode = dataNode.parent;
        }
        path.Reverse();
        return path;
    }
}
