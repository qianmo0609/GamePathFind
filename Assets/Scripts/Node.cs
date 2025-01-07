using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Transform arrow;
    public MeshRenderer m_renderer;
    public Material defultMat;
    public Material pathMat;
    public TextMeshPro idxTex;

    [SerializeField]
    private int x;
    [SerializeField]
    private int z;

    private float startAngle = 180;
    private Vector3 pos;
    public Vector3 POS { get { return pos; } }

    public void OnInitNode(int x, int z,Vector3 pos,int halfX,int halfZ)
    {
        this.OnResetNode();
        this.x = x;
        this.z = z;
        this.idxTex.text = $"({x-halfX},{z-halfZ})";
        this.pos = pos;
    }

    private void OnMouseDown()
    {
        Controller.Instance.SetTargetPoint(x,z);
    }

    public void OnResetNode()
    {
        arrow.gameObject.SetActive(false);
        m_renderer.sharedMaterial = defultMat;
    }

    public void OnSetFind(float angle)
    {
        arrow.gameObject.SetActive(true);
        arrow.eulerAngles = new Vector3(0, startAngle + angle,0);
        m_renderer.sharedMaterial = pathMat;
    }
}
