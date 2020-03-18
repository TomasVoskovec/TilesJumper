using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TomikPlayer : MonoBehaviour
{
    public bool canLerp = false;
    public bool canLerpNext = false;

    public float timeStartedLerping;
    public float lerpTime;
    public float lerpDistance;

    public Vector3 startPossition;
    public Vector3 endPossitionn;

    void startLerping(bool boost = false)
    {
        timeStartedLerping = Time.time;

        if(boost)
        {
            endPossitionn.z += lerpDistance*2;
        }
        else
        {
            endPossitionn.z += lerpDistance;
        }

        canLerp = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        canLerpNext = true;
        endPossitionn = startPossition;
    }

    void Update()
    {
        if(canLerpNext)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startLerping();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                startLerping(true);
            }
        }

        if(canLerp)
        {
            transform.position = Lerp(startPossition, endPossitionn, timeStartedLerping, lerpTime);
        }
    }

    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime = 1)
    {
        canLerpNext = false;

        float timeSinceStarted = Time.time - timeStartedLerping;

        float percentageComplete = timeSinceStarted / lerpTime;

        Vector3 result = Vector3.Lerp(start, end, percentageComplete);

        if (percentageComplete >= 1)
        {
            startPossition = endPossitionn;
            canLerpNext = true;
        }

        return result;
    }
}
