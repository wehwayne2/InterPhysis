using UnityEngine;

public class LineConnector : MonoBehaviour
{
    public Transform pointA;  // The first point
    public Transform pointB;  // The second point

    private LineRenderer lineRenderer;

    void Start()
    {
        // Get the LineRenderer component attached to this GameObject
        lineRenderer = GetComponent<LineRenderer>();

        // Set the number of positions to 2
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        // Update the positions of the LineRenderer to match the positions of pointA and pointB
        if (lineRenderer != null && pointA != null && pointB != null)
        {
            lineRenderer.SetPosition(0, pointA.position);
            lineRenderer.SetPosition(1, pointB.position);
        }
    }
}
