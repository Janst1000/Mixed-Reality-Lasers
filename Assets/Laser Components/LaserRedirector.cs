using UnityEngine;

public class LaserRedirector : MonoBehaviour
{
    [HideInInspector]
    public bool isPowered;

    // Called by any laser script when it hits this object
    public void OnLaserHit()
    {
        Debug.Log("LaserRedirector is hit!");
        isPowered = true;
    }

    void LateUpdate()
    {
        // Reset isPowered after all lasers have updated
        isPowered = false;
    }
}
