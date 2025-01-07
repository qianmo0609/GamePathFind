using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Node node;
    public GameObject blackNode;

    private float halfWidth = 0.5f;

    public List<Node> nodes;

    private List<Vector2Int> originalPath;

    public void OnInitMap(int[,] data, int xCount,int zCount)
    {
        nodes = new List<Node>();
        originalPath = null;
  
        float startX = -xCount * 0.5f * halfWidth;
        float startZ = zCount * 0.5f * halfWidth;
        for(int i = 0; i < zCount; i++)
        {
            for(int j = 0; j < xCount; j++)
            {
                if (data[i, j] == 1)
                {
                    Instantiate(blackNode, new Vector3(startX + j * halfWidth * 2, 1, startZ - i * halfWidth * 2), Quaternion.identity, this.transform);
                }
                Vector3 pos = new Vector3(startX + j * halfWidth * 2, 0, startZ - i * halfWidth * 2);
                Node _node = Instantiate<Node>(node, pos, Quaternion.identity, this.transform);
                _node.OnInitNode(j, i, pos, xCount / 2, zCount / 2);
                nodes.Add(_node);
            }
        }   
    }

    public void onSetFindNode(List<Vector2Int> nodes)
    {
        var data = MapData.m_MapData;
        int xCount = data.GetLength(1);
        int zCount = data.GetLength(0);
        this.ClearPathData(xCount,zCount);
        float angle = 0;
        for (int i = 0; i < nodes.Count; i++)
        {
            Node nI = this.getNode(nodes[i], xCount,zCount);
            if (i < nodes.Count - 1)
            {
                Node nI1 = this.getNode(nodes[i + 1], xCount,zCount);
                Vector3 dir = (nI1.POS - nI.POS).normalized;
                angle = Vector3.SignedAngle(Vector3.forward, dir,Vector3.up);
            }
            nI.OnSetFind(angle);
        }
        this.originalPath = nodes;
    }

    public Node getNode(Vector2Int node, int xCount,int zCount)
    {
        return nodes[xCount* node.y + node.x];
    }

    private void ClearPathData(int xCount,int zCount)
    {
        if(this.originalPath!=null && this.originalPath.Count > 0)
        {
            for (int i = 0; i < this.originalPath.Count; i++)
            {
                this.getNode(this.originalPath[i], xCount, zCount).OnResetNode();
            }
        }
    }
}
