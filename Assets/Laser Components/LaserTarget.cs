using UnityEngine;

public class LaserTarget : MonoBehaviour
{
    // Optional: a material or color to switch to when hit by the laser
    public Material hitMaterial;
    private Material originalMaterial;
    private Renderer objectRenderer;

    // This will track if we are currently being hit by the laser
    private bool isBeingHit;
    private bool lastFrameWasHit;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }
    }

    // Called when the laser hits this object
    public void OnLaserHit()
    {
        // 1) Log a message
        Debug.Log(gameObject.name + " is hit by the laser!");

        // 2) Change color / material while it’s being hit
        if (objectRenderer && hitMaterial)
        {
            objectRenderer.material = hitMaterial;
        }

        // 3) Possibly set a flag that it’s currently being hit
        isBeingHit = true;
    }

    

    void Update(){
        // If we were being hit last frame, but not anymore, revert
        if (isBeingHit)
        {
            // Raycast from LaserSource might only call OnLaserHit() once 
            // per frame if it continuously hits. 
            // We want to reset isBeingHit to false after each frame,
            // so that if next frame the laser is NOT hitting this object,
            // we can revert color in the next line of code.
            isBeingHit = false;
            lastFrameWasHit = true;
        }
        else
        {
            // This runs if we are NOT being hit this frame but possibly 
            // changed color in a previous frame. So revert to original color.
            lastFrameWasHit = false;
            if (objectRenderer && originalMaterial && objectRenderer.material != originalMaterial)
            {
                objectRenderer.material = originalMaterial;
            }
        }
    }
        
    public bool IsBeingHit()
    {
        Debug.Log("LaserTarget: IsBeingHit() called: " + isBeingHit);
        return lastFrameWasHit;
    }
}
