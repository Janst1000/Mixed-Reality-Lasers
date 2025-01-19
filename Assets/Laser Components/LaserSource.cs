using UnityEngine;

public class LaserSource : MonoBehaviour
{
    public Transform laserOrigin;          // Starting point of the laser
    public LineRenderer lineRenderer;      // LineRenderer component
    public float maxLaserDistance = 50f;   // Maximum laser distance

    void Start()
    {
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer is not assigned!");
            return;
        }

        if (laserOrigin == null)
        {
            Debug.LogError("LaserOrigin is not assigned!");
            return;
        }

        // Set LineRenderer to use 2 points
        lineRenderer.positionCount = 2;

        // Set initial width
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
    }

    void Update()
    {
        if (lineRenderer == null || laserOrigin == null)
        {
            return; // Skip update if something is missing
        }

        

        //Debug.Log($"Laser Start: {laserOrigin.position}, Laser Direction: {laserOrigin.forward}");

        Vector3 lineRendererOrigin = Vector3.zero;

        // Set the laser's start position
        lineRenderer.SetPosition(0, lineRendererOrigin);

        // Perform raycast to compute the endpoint
        RaycastHit hit;
        //Debug.DrawRay(laserOrigin.position, laserOrigin.forward * maxLaserDistance, Color.red);

        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit, maxLaserDistance))
        {
            Vector3 hitPointLocalFrame = laserOrigin.transform.InverseTransformPoint(hit.point);
            lineRenderer.SetPosition(1, hitPointLocalFrame); // Laser stops at the hit point
            //Debug.Log($"Laser hit object: {hit.collider.name} at {hit.point}");
            // Lenght of the laser is the distance from the origin to the hit point
            //Debug.Log($"Laser Length: {Vector3.Distance(laserOrigin.position, hit.point)}");

        }
        else
        {
            lineRenderer.SetPosition(1, lineRendererOrigin + laserOrigin.forward * maxLaserDistance); // Laser extends to max distance
        }
    }
}
