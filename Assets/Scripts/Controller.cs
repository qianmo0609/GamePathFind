using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller Instance;
    public Map map;

    public Vector2Int startPos;

    private FindPathAlgorithm algorithm;

    void Start()
    {
        Instance = this;
        var data = MapData.m_MapData;
        int xCount = data.GetLength(1);
        int zCount = data.GetLength(0);
        map.OnInitMap(data,xCount,zCount);
        startPos = new Vector2Int(0, 0) + new Vector2Int(xCount/2,zCount/2);
        //BFS
        //algorithm = new BFS(data, xCount,zCount);
        //DFS
        //algorithm = new DFS(data, xCount, zCount);
        //Dijkstra
        algorithm = new Dijkstra(data, xCount, zCount);
        //A*
        //algorithm = new AStar(data, xCount, zCount);
        //GBFS
        //algorithm = new GBFS(data, xCount, zCount);
        
    }

    public void SetTargetPoint(int x , int z)
    {
        map.onSetFindNode(algorithm.FindPath(startPos, new Vector2Int(x, z)));
    }
}
