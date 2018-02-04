using UnityEngine;
using System.Collections.Generic;

public class SphericalGrid : MonoBehaviour
{
    [SerializeField]
    private float spaceBetweenPoints;
    private float size;
    private int totalPoints;

    /*void Start()
    {
        size = transform.GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x;
        totalPoints = Mathf.FloorToInt(size / spaceBetweenPoints);

        Debug.Log(totalPoints);

        for (int x = 0; x < totalPoints / 2; x++)
        {
            for (int y = 0; y < totalPoints; y++)
            {
                var point = GetSphericalCoordinates(x, y);
                GameObject mySphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                //Debug.Log(x * totalPoints + y);
                mySphere.transform.position = point;
                mySphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }   
        }
    }

    public Vector3 GetSphericalCoordinates(int x, int y)
    {
        int n = (x * totalPoints + y);
        float radius = size * 0.5f;
        float a = 0%Mathf.PI/(n/2)%2*Mathf.PI; 
        float b = -Mathf.PI%2*Mathf.PI/n%Mathf.PI;

        float posX = Mathf.Sin(b) * Mathf.Cos(a) * radius;
        float posZ = Mathf.Sin(a) * Mathf.Sin(b) * radius;
        float posY = Mathf.Cos(b) * radius;
        //float posY = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow (posX, 2) - Mathf.Pow (posZ, 2));

        Vector3 pos = new Vector3(posX, posY, posZ);

        pos += transform.position;

        return pos;
    }*/

    void Start() 
    {
        size = transform.GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x * 0.5f;

        totalPoints = (int)Mathf.Pow(Mathf.FloorToInt(size / spaceBetweenPoints), 2.0f);
        List<Vector3> pts = PointsOnSphere(totalPoints);

        List<GameObject> spheres = new List<GameObject>();

        int i = 0;
    
        foreach (Vector3 pt in pts)
        {
            spheres.Add(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            spheres[i].transform.parent = transform;
            spheres[i].transform.position = pt * size;
            spheres[i].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

            i++;
        }
    }
    
    List<Vector3> PointsOnSphere(int n) //Creates list of points around sphere
    {    
        List<Vector3> pts = new List<Vector3>();
        float inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        float off = 2.0f / n;
        float x;
        float y;
        float z;
        float r;
        float phi;
    
        for (int k = 0; k < n; k++)
        {
            y = k * off - 1 + (off /2);
            r = Mathf.Sqrt(1 - y * y);
            phi = k * inc;
            x = Mathf.Cos(phi) * r;
            z = Mathf.Sin(phi) * r;

            pts.Add(new Vector3(x, y, z));
        }

        return pts;
    }
} 
 
