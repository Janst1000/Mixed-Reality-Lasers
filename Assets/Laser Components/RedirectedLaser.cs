using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RedirectedLaser : MonoBehaviour
{
    public LaserRedirector redirector;  // Reference to the LaserRedirector on this cube
    public Transform outputPoint;       // Child transform indicating beam origin/direction
    public float maxDistance = 50f;

    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;

        // Start with the beam hidden
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (!redirector || !outputPoint || !lineRenderer) return;

        if (redirector.isPowered)
        {
            Debug.Log("Redirected Laser is powered!");
            // Enable the beam
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, outputPoint.position);

            RaycastHit hit;
            if (Physics.Raycast(outputPoint.position, outputPoint.forward, out hit, maxDistance))
            {
                lineRenderer.SetPosition(1, hit.point);

                // If it hits another redirector, power it
                if (hit.collider.CompareTag("Redirector"))
                {
                    LaserRedirector other = hit.collider.GetComponent<LaserRedirector>();
                    if (other != null)
                    {
                        other.OnLaserHit();
                    }
                }

                // If it hits a target
                if (hit.collider.CompareTag("Target"))
                {
                    LaserTarget targetScript = hit.collider.GetComponent<LaserTarget>();
                    if (targetScript != null)
                    {
                        targetScript.OnLaserHit();
                    }
                }
            }
            else
            {
                // No hit, extend to maxDistance
                lineRenderer.SetPosition(1, 
                    outputPoint.position + outputPoint.forward * maxDistance);
            }
        }
        else
        {
            // Not powered, so no beam
            lineRenderer.enabled = false;
        }
    }
}
