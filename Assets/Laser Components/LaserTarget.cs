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
        Debug.Log(gameObject.name + " is hit by the laser!");

        if (objectRenderer && hitMaterial)
        {
            objectRenderer.material = hitMaterial;
        }

        isBeingHit = true;
    }

    

    void Update(){
        // If we were being hit last frame, but not anymore, revert
        if (isBeingHit)
        {
            // Resetting for the next frame (needed so that we can detect changes)
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
        return lastFrameWasHit;
    }
}
