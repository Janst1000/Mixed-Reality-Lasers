using UnityEngine;
using TMPro; // For TextMeshPro UI

public class LaserTargetTracker : MonoBehaviour
{
    public TextMeshProUGUI[] targetStatusTexts; // Assign text objects in the UI for each target
    public GameObject[] laserTargets; // Assign or dynamically find laser targets

    void Start()
    {
        // Optionally find all targets with the "LaserTarget" tag
        // laserTargets = GameObject.FindGameObjectsWithTag("LaserTarget");
    }

    void Update()
    {
        for (int i = 0; i < laserTargets.Length; i++)
        {
            if (i < targetStatusTexts.Length)
            {
                // Access the LaserTarget script to check if the target is being hit
                LaserTarget targetScript = laserTargets[i].GetComponent<LaserTarget>();
                if (targetScript != null)
                {
                    if (targetScript.IsBeingHit())
                    {
                        targetStatusTexts[i].text = "Target hit: Yes\n";
                    }
                    else
                    {
                        targetStatusTexts[i].text = "Target hit: No\n";
                    }
                }
            }
        }
    }
}
