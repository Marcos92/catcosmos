using UnityEngine;
using System.Collections.Generic;

public class SphericalGrid : MonoBehaviour
{
    [SerializeField]
    private float spaceBetweenPoints;
    private float size;
    private int totalPoints;

    [HideInInspector]
    public GridNode[] nodes;

    [SerializeField]
    private GridNode nodePrefab;

    private GameObject player;

    void Start() 
    {
        size = transform.GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x * 0.5f;

        totalPoints = (int)Mathf.Pow(Mathf.FloorToInt(size / spaceBetweenPoints), 2.0f);

        nodes = new GridNode[totalPoints];

        CreateNodes();

        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        FindActiveNode(1.0f);
    }
    
    void CreateNodes()
    {    
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

    void FindActiveNode(float minDistance)
    {
        Vector3 playerPosition = player.transform.position;

        GridNode closestNode = null;
        float closestDistance = 0.0f;

        for(int i = 0; i < totalPoints; i++)
        {
            float distance = Vector3.Distance(nodes[i].gameObject.transform.position, playerPosition);

            if(distance <= minDistance)
            {
                if(closestNode == null || closestNode != null && distance < closestDistance)
                {
                    closestNode = nodes[i];
                    closestDistance = distance;
                }
            }
            
            nodes[i].active = nodes[i] == closestNode;
        }
    }
}
 
