using UnityEngine;

public class LaserRedirector : MonoBehaviour
{
    [HideInInspector]
    public bool isPowered;
    public bool lastFrameWasPowered;

    // Called by any laser script when it hits this object
    public void OnLaserHit()
    {
        isPowered = true;
        Debug.Log(gameObject.name + " is powered!");
    }

    void LateUpdate()
    {
        // Reset isPowered after all lasers have updated
        if (isPowered)
        {
            isPowered = false;
            lastFrameWasPowered = true;
        } else
        {
            lastFrameWasPowered = false;
        }
        
    }
}
