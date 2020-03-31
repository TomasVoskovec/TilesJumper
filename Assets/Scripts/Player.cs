using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    // Lerp props
    bool canLerp = false;
    bool canLerpNext = false;
    float timeStartedLerping;

    Vector3 startPossition;
    Vector3 endPossitionn;

    // Map props
    MapGenerator mapGenerator;

    // Animation
    Animator jumpAnimator;

    [Space]
    [Header("Animation")]
    public int Lerps = 0;
    public float LerpDistance;
    public float JumpHeight;
    public float Speed = 1;

    [Space]
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
        // Set speed of jumping animation
        jumpAnimator.SetFloat("JumpingTimeMultiplier", Speed);

        // ???
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow);

        // Jump 2x farther if you click / tap on display
        if (canLerpNext && Input.touchCount > 0 && !manager.MainMenuActive)
        {
            Touch touch = Input.GetTouch(0);

            if (Lerps + 2 < mapGenerator.WhiteTileNumber)
            {
                Lerps += 2;
                JumpHeight = 2;
            }

        }

        // Move player (only) forward
        if (canLerp)
        {
            transform.position = Lerp(startPossition, endPossitionn, timeStartedLerping, Speed + Speed * 0.2f);
        }
    }

    // Starts the Lerping animation
    public void StartLerping()
    {
        if (!manager.MainMenuActive)
        {
            timeStartedLerping = Time.time;
            Points++;
            UpdateUI();
            if (JumpHeight == 2)
            {
                endPossitionn.z += LerpDistance * 2;
            }
            else
            {
                endPossitionn.z += LerpDistance;
            }
            JumpHeight = 1;
            canLerp = true;
        }
    }

    // Lerp animation setup
    public Vector3 Lerp(Vector3 start, Vector3 end, float timeStartedLerping, float lerpTime)
    {
        canLerpNext = false;

        // Generate percentage completion (1.0 = 100%)
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percentageComplete = timeSinceStarted * lerpTime;

        // Lerp animation
        Vector3 result = Vector3.Lerp(start, end, percentageComplete);

        // Increase starting lerp possition
        if (percentageComplete >= 1)
        {
            startPossition = endPossitionn;
            canLerpNext = true;
        }

        return result;
    }

    // Update points in the UI
    void UpdateUI()
    {
        Points_UI.GetComponent<TextMeshProUGUI>().text = Points.ToString();
    }

    // Generate smoke particles under player
    public void GenerateParticles()
    {
        Instantiate(SmokeParticle, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z), SmokeParticle.transform.rotation);
    }

    // Push tile down after player colide with it
    public void PushTile()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "tile")
            {
                hit.collider.gameObject.GetComponent<Animator>().SetTrigger("pushed");
            }
        }
    } 
}
