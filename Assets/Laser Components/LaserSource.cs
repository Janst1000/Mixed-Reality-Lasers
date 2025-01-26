using UnityEngine;

public class LaserSource : MonoBehaviour
{
    public Transform laserOrigin;     // Usually the same as this transform, or a child
    public LineRenderer lineRenderer;
    public float maxDistance = 50f;

    void Start()
    {
        if (!laserOrigin) Debug.LogError("LaserOrigin not assigned!");
        if (!lineRenderer) Debug.LogError("LineRenderer not assigned!");

        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
    }

    void Update()
    {
        if (!laserOrigin || !lineRenderer) return;

        // First point of the laser
        lineRenderer.SetPosition(0, laserOrigin.position);

        RaycastHit hit;
        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit, maxDistance))
        {
            // End the beam at the hit point
            lineRenderer.SetPosition(1, hit.point);

            // Check if it's a Redirection Cube
            if (hit.collider.CompareTag("Redirector"))
            {
                LaserRedirector redirector = hit.collider.GetComponent<LaserRedirector>();
                if (redirector != null)
                {
                    redirector.OnLaserHit();
                }
            }

            // Check if it's a Target
            if (hit.collider.CompareTag("Target"))
            {
                // Optionally, call OnLaserHit() on the target script
                LaserTarget targetScript = hit.collider.GetComponent<LaserTarget>();
                if (targetScript != null)
                {
                    targetScript.OnLaserHit();
                }
            }
        }
        else
        {
            // If nothing is hit, laser goes full distance
            lineRenderer.SetPosition(1, laserOrigin.position + laserOrigin.forward * maxDistance);
        }
    }
}
