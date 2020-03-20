using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TomikPlayer : MonoBehaviour
{
    bool canLerp = false;
    bool canLerpNext = false;

    public int lerps = 0;

    float timeStartedLerping;

    Vector3 startPossition;
    Vector3 endPossitionn;

    MapGenerator mapGenerator;

    public float LerpTime;
    public float LerpDistance;

    public GameObject Map;

    void startLerping(bool boost = false)
    {
        timeStartedLerping = Time.time;

        if(boost)
        {
            endPossitionn.z += LerpDistance*2;
        }
        else
        {
            endPossitionn.z += LerpDistance;
        }

        canLerp = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get map informations
        mapGenerator = Map.GetComponent<MapGenerator>();

        canLerpNext = true;
        startPossition = transform.position;
        endPossitionn = startPossition;
    }

    void Update()
    {
        if(canLerpNext)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (lerps + 1 < mapGenerator.WhiteTileNumber)
                {
                    startLerping();
                    lerps++;
                }
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                if(lerps + 2 < mapGenerator.WhiteTileNumber)
                {
                    startLerping(true);
                    lerps += 2;
                }
            }
        }

        if(canLerp)
        {
            transform.position = Lerp(startPossition, endPossitionn, timeStartedLerping, LerpTime);
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
