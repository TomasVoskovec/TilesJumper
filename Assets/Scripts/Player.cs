using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    bool canLerp = false;
    bool canLerpNext = false;

    public int lerps = 0;
    public float JumpDistance;
    public float Speed = 1;
    float timeStartedLerping;

    Vector3 startPossition;
    Vector3 endPossitionn;

    MapGenerator mapGenerator;
    Animator jumpAnimator;

    public float LerpTime;
    public float LerpDistance;

    
    [Header("GamePlay")]
    public int Points;
    public GameObject Points_UI;

    [Space]
    [Header("Particles")]
    public GameObject SmokeParticle;

    [Space]
    [Header("Public references")]
    public Menu Menu;
    public GameObject Map;
    private GameManager manager;

    void Start()
    {
        // Menu
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        // Get map informations
        GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        mapGenerator = Map.GetComponent<MapGenerator>();

        // Set start color of player
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = mapGenerator.AvailableColors[0];

        // Update score
        UpdateUI();

        // Lerping props
        canLerpNext = true;
        startPossition = transform.position;
        endPossitionn = startPossition;

        // Jump animation setup
        jumpAnimator = GetComponentInChildren<Animator>();

    }

    private void FixedUpdate()
    {
        jumpAnimator.SetFloat("JumpingTimeMultiplier", Speed);

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow);

        if (canLerpNext && Input.touchCount > 0 && !manager.MainMenuActive)
        {
            Touch touch = Input.GetTouch(0);

            if (lerps + 2 < mapGenerator.WhiteTileNumber)
            {
                lerps += 2;
                JumpDistance = 2;
            }

        }

        if (canLerp)
        {
            transform.position = Lerp(startPossition, endPossitionn, timeStartedLerping, LerpTime);
        }
    }

    void Update()
    {
        
    }
    public void StartLerping()
    {
        if (!manager.MainMenuActive)
        {
            timeStartedLerping = Time.time;
            Points++;
            UpdateUI();
            if (JumpDistance == 2)
            {
                endPossitionn.z += LerpDistance * 2;
            }
            else
            {
                endPossitionn.z += LerpDistance;
            }
            JumpDistance = 1;
            canLerp = true;
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
            }else if (hit.collider.tag == "tile_generate")
            {
                // map generate code here ...
            }
        }
        
    } 
}
