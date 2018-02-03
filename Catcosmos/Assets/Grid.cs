using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private float spaceBetweenPoints;
    private float size;
    private int totalPoints;

    void Awake()
    {
        size = transform.GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x;
        totalPoints = Mathf.RoundToInt(size / spaceBetweenPoints);

        for (int x = 0; x < totalPoints; x++)
        {
            for (int y = 0; y < totalPoints; y++)
            {
                var point = GetSphericalCoordinates(x, y);
                GameObject mySphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Debug.Log(x * totalPoints + y);
                mySphere.transform.position = point;
                mySphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }   
        }
    }

    public Vector3 GetSphericalCoordinates(int x, int y)
    {
        float radius = size * 0.5f;

        //Debug.Log("Angle: " + Mathf.PI * 2.0f * ((float)x / (float)totalPoints));

        float posX = Mathf.Sin(Mathf.PI * 2.0f * ((float)x / (float)totalPoints)) * radius;
        float posZ = Mathf.Cos(Mathf.PI * 2.0f * ((float)y / (float)totalPoints)) * radius;
        float posY = Mathf.Sin(Mathf.PI * 2.0f * ((float)y / (float)totalPoints)) * radius;
        //float posY = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow (posX, 2) - Mathf.Pow (posZ, 2));

        Vector3 pos = new Vector3(posX, posY, posZ);

        pos += transform.position;

        return pos;
    }
}