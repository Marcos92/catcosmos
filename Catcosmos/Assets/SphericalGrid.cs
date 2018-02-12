﻿using UnityEngine;
using System.Collections.Generic;

public class SphericalGrid : MonoBehaviour
{
    [SerializeField]
    private float spaceBetweenPoints;
    private float size;
    private int totalPoints;

    public float minDistance;

    [HideInInspector]
    public GridNode[] nodes;
    private GridNode closestNode;

    [SerializeField]
    private GridNode nodePrefab;

    private GameObject player;

    private GameObject[] obstacles;

    void Start() 
    {
        size = transform.GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x * 0.5f;

        totalPoints = (int)Mathf.Pow(Mathf.FloorToInt(size / spaceBetweenPoints), 2.0f);

        nodes = new GridNode[totalPoints];

        obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        CreateNodes();
        RemoveOverlapingNodes();

        player = GameObject.FindWithTag("Player");
        PlayerMovement.OnMove += UpdateNodes;

        UpdateNodes(); 
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
    
        for (int i = 0; i < totalPoints; i++)
        {
            y = i * off - 1 + (off /2);
            r = Mathf.Sqrt(1 - y * y);
            phi = i * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            CreateNode(new Vector3(x, y, z), i);
        }
    }

    void CreateNode(Vector3 position, int index)
    {
        GridNode node = Instantiate(nodePrefab, position * size, Quaternion.FromToRotation(transform.up, position * size - transform.position) * transform.rotation);
        node.transform.parent = transform;
        nodes[index] = node;
    }

    void RemoveOverlapingNodes()
    {
        for (int i = 0; i < totalPoints; i++)
        {
            Bounds nodeBounds = nodes[i].transform.GetComponent<Collider>().bounds;

            foreach(GameObject obstacle in obstacles)
            {
                if(nodeBounds.Intersects(obstacle.transform.GetComponent<Collider>().bounds))
                {
                    nodes[i].gameObject.SetActive(false);
                }
            }
        }
    }

    void FindActiveNode()
    {
        Vector3 playerPosition = player.transform.position;

        float closestDistance = minDistance;

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
        }
    }

    void UpdateNodes()
    {
        FindActiveNode();

        for(int i = 0; i < totalPoints; i++)
        {
            nodes[i].active = nodes[i] == closestNode;
        }
    }
}
 
