using UnityEngine;

public class WireCircleAnimator : MonoBehaviour
{
    public int numberOfPoints = 50;  // Number of points in the circle
    public float radius = 1f;        // Radius of the circle
    public float noiseScale = 0.1f;  // Scale of the Perlin noise (frequency of the noise)
    public float animationSpeed = 1f;// Speed of the animation
    public float noiseAmplitude = 0.2f; // Amplitude of the noise (how much the wire moves)

    private LineRenderer lineRenderer;
    private Vector3[] points;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numberOfPoints + 1;
        points = new Vector3[numberOfPoints + 1];

        CreateCircle();
    }

    void Update()
    {
        AnimateCircle();
    }

    void CreateCircle()
    {
        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfPoints;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            points[i] = new Vector3(x, 0, z);
        }
        points[numberOfPoints] = points[0]; // Close the circle
    }

    void AnimateCircle()
    {
        Vector3 basePosition = transform.position;
        Quaternion rotation = transform.rotation; // Get the current rotation of the GameObject

        for (int i = 0; i < numberOfPoints; i++)
        {
            // Apply Perlin noise for random, smooth movement
            float noiseValue = Mathf.PerlinNoise(i * noiseScale, Time.time * animationSpeed);
            float offset = (noiseValue - 0.5f) * 2f * noiseAmplitude;

            // Start with the original point in the circle
            Vector3 animatedPoint = points[i];

            // Apply the noise offset to the y-coordinate
            animatedPoint.y += offset;

            // Apply the GameObject's rotation to the point
            animatedPoint = rotation * animatedPoint;

            // Translate the point to world space by adding the base position
            lineRenderer.SetPosition(i, basePosition + animatedPoint);
        }

        // Ensure the circle is closed
        lineRenderer.SetPosition(numberOfPoints, lineRenderer.GetPosition(0));
    }
}
