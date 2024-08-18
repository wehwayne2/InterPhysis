using UnityEngine;

public class TriangleFace : MonoBehaviour
{
    public Transform pointA;  // The first vertex
    public Transform pointB;  // The second vertex
    public Transform pointC;  // The third vertex

    private Mesh mesh;

    void Start()
    {
        // Initialize the mesh
        mesh = new Mesh();

        // Assign the mesh to the MeshFilter component
        GetComponent<MeshFilter>().mesh = mesh;

        // Create the initial triangle
        UpdateMesh();
    }

    void Update()
    {
        // Update the triangle mesh to follow the moving points
        UpdateMesh();
    }

    void UpdateMesh()
    {
        // Calculate the centroid of the triangle
        Vector3 centroid = (pointA.position + pointB.position + pointC.position) / 3f;

        // Define the vertices of the triangle relative to the centroid
        Vector3[] vertices = new Vector3[3];
        vertices[0] = pointA.position - centroid;
        vertices[1] = pointB.position - centroid;
        vertices[2] = pointC.position - centroid;

        // Define the single triangle
        int[] triangles = new int[3];
        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        // Clear the previous mesh data
        mesh.Clear();

        // Assign the vertices and triangles to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        // Optionally, recalculate the normals for lighting
        mesh.RecalculateNormals();

        // Move the GameObject to the centroid
        transform.position = centroid;
    }
}
