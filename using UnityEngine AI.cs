using UnityEngine;

public class Distance : MonoBehaviour
{
    public GameObject wrist;
    public GameObject finger;
    public GameObject block;

    public float previousDistance;
    public float currentDistance;

    public int inwardCount = 0;
    public int outwardCount = 0;

    public float stableThreshold = 0.002f;   // small change = stable
    public int moveThreshold = 5;            // how many same-direction movements before moving block
    public float blockMoveStep = 0.02f;      // how much block moves

    void Start()
    {
        previousDistance = Vector3.Distance(
            wrist.transform.position,
            finger.transform.position
        );
    }

    void Update()
    {
        // Measure current distance
        currentDistance = Vector3.Distance(
            wrist.transform.position,
            finger.transform.position
        );

        float difference = currentDistance - previousDistance;

        // Compare current distance with previous distance
        if (Mathf.Abs(difference) < stableThreshold)
        {
            Debug.Log("Stable");
        }
        else if (currentDistance < previousDistance)
        {
            inwardCount++;
            outwardCount = 0;   // reset opposite direction
            Debug.Log("Inward motion");
        }
        else
        {
            outwardCount++;
            inwardCount = 0;    // reset opposite direction
            Debug.Log("Outward motion");
        }

        // If enough inward motions happen -> move block forward
        if (inwardCount >= moveThreshold)
        {
            block.transform.position += new Vector3(0f, 0f, blockMoveStep);
            inwardCount = 0;
            outwardCount = 0;
            Debug.Log("Block moved forward");
        }

        // If enough outward motions happen -> move block backward
        if (outwardCount >= moveThreshold)
        {
            block.transform.position += new Vector3(0f, 0f, -blockMoveStep);
            inwardCount = 0;
            outwardCount = 0;
            Debug.Log("Block moved backward");
        }

        // Update previous distance for next frame
        previousDistance = currentDistance;
    }
}