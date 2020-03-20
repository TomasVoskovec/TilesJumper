using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
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
    [Header("GamePlay")]
    public int Points;
    public GameObject Points_UI;
    [Space]
    [Header("Particles")]
    public GameObject SmokeParticle;

    // Start is called before the first frame update
    void Start()
    {
        //Get map informations
        
        mapGenerator = Map.GetComponent<MapGenerator>();
        UpdateUI();
        canLerpNext = true;
        startPossition = transform.position;
        endPossitionn = startPossition;
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down), Color.yellow);
        if (canLerpNext)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (lerps + 1 < mapGenerator.WhiteTileNumber)
                {
                    startLerping(false);
                    lerps++;
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (lerps + 2 < mapGenerator.WhiteTileNumber)
                {
                    startLerping(true);
                    lerps += 2;
                }
            }
        }

        if (canLerp)
        {
            transform.position = Lerp(startPossition, endPossitionn, timeStartedLerping, LerpTime);
        }
    }
    void startLerping(bool boost)
    {
        timeStartedLerping = Time.time;
        Points++;
        UpdateUI();
        if (boost)
        {
            endPossitionn.z += LerpDistance * 2;
        }
        else
        {
            endPossitionn.z += LerpDistance;
        }

        canLerp = true;
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
    void UpdateUI()
    {
        // update points in the UI
        Points_UI.GetComponent<TextMeshProUGUI>().text = Points.ToString();
    }
    public void GenerateParticles()
    {
        Instantiate(SmokeParticle, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z), SmokeParticle.transform.rotation);
    }
    public void PushTile()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            
            print("hit");
            if (hit.collider.tag == "tile")
            {
                print("hit Tile");
                
                hit.collider.gameObject.GetComponent<Animator>().SetTrigger("pushed");
            }
        }
        
    } 
}
