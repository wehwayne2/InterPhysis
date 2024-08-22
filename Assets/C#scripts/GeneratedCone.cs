using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class DynamicCone : MonoBehaviour
{
    // These variables should appear in the Inspector
    public Transform pointA; // Base point 1
    public Transform pointB; // Base point 2
    public Transform pointC; // Base point 3
    public Transform pointD; // Tip of the cone

    private Mesh mesh;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        CreateCone();
    }

    void CreateCone()
    {
        Vector3[] vertices = new Vector3[4];
        vertices[0] = pointA.position; // Base point 1
        vertices[1] = pointB.position; // Base point 2
        vertices[2] = pointC.position; // Base point 3
        vertices[3] = pointD.position; // Tip point

        int[] triangles = new int[12];
        // Base triangle
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        // Side triangles
        triangles[3] = 0;
        triangles[4] = 1;
        triangles[5] = 3;

        triangles[6] = 1;
        triangles[7] = 2;
        triangles[8] = 3;

        triangles[9] = 2;
        triangles[10] = 0;
        triangles[11] = 3;

        // Normals (optional, helps with lighting)
        Vector3[] normals = new Vector3[4];
        normals[0] = -Vector3.up;
        normals[1] = -Vector3.up;
        normals[2] = -Vector3.up;
        normals[3] = Vector3.up;

        // Update the mesh
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.normals = normals;

        mesh.RecalculateNormals(); // Optional: recalculate normals for better lighting
        mesh.RecalculateBounds();
    }
}
