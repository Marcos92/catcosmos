using UnityEngine;
using System.Collections.Generic;

public class SphericalGrid : MonoBehaviour
{
    [SerializeField]
    private float spaceBetweenPoints;
    private float size;
    private int totalPoints;

    GridNode[] nodes;
    public GridNode nodePrefab;

    void Start() 
    {
        size = transform.GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x * 0.5f;

        totalPoints = (int)Mathf.Pow(Mathf.FloorToInt(size / spaceBetweenPoints), 2.0f);

        nodes = new GridNode[totalPoints];

        CreateNodes();
    }
    
    void CreateNodes()
    {    
        List<GridNode> nodes = new List<GridNode>();

        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / totalPoints;
        float x;
        float y;
        float z;
        float r;
        float phi;
    
        for (int k = 0; k < totalPoints; k++)
        {
            y = k * off - 1 + (off /2);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            CreateNode(new Vector3(x, y, z), k);
        }
    }

    void CreateNode(Vector3 position, int index)
    {
        GridNode node = Instantiate(nodePrefab, position * size, Quaternion.FromToRotation(transform.up, position * size - transform.position) * transform.rotation);
        nodes[index] = node;
    }
} 
 
