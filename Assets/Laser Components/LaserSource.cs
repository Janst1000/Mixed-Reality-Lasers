using UnityEngine;

public class LaserSource : MonoBehaviour
{
    public Transform laserOrigin;
    public LineRenderer lineRenderer;
    public float maxLaserDistance = 50f;

    void Start()
    {
        if (!laserOrigin) Debug.LogError("LaserOrigin is not assigned!");
        if (!lineRenderer) Debug.LogError("LineRenderer is not assigned!");

        // For clarity, let's make sure we're using world space:
        if (lineRenderer)
        {
            lineRenderer.useWorldSpace = true;
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
        }
    }

    void Update()
    {
        if (!laserOrigin || !lineRenderer) return;

        // Set the start of the laser in world space:
        lineRenderer.SetPosition(0, laserOrigin.position);

        // Perform a Raycast in world space:
        RaycastHit hit;
        if (Physics.Raycast(laserOrigin.position, laserOrigin.forward, out hit, maxLaserDistance))
        {
            // 1) Draw the laser up to the hit point
            lineRenderer.SetPosition(1, hit.point);

            // 2) Check if the hit object is a "Target"
            if (hit.collider.CompareTag("Target"))
            {
                Debug.Log("Laser hit the target object!");

                // Call the OnLaserHit() method on the target object
                LaserTarget targetScript = hit.collider.GetComponent<LaserTarget>();
                if (targetScript != null)
                {
                    targetScript.OnLaserHit(); 
                }
            }
        }
        else
        {
            // If nothing is hit, draw the laser to max distance
            lineRenderer.SetPosition(1, laserOrigin.position + laserOrigin.forward * maxLaserDistance);
        }
    }
}
