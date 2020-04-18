using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    // Lerp props
    bool canLerp = false;
    public bool canLerpNext = false;
    float timeStartedLerping;

    Vector3 startPossition;
    Vector3 endPossitionn;

    // Map props
    MapGenerator mapGenerator;

    // Animation
    Animator jumpAnimator;

    [Space]
    [Header("Animation")]
    public float LerpDistance;
    public float JumpHeight;
    
    [Space]
    [Header("Gameplay")]
    public float StartSpeed = 1;
    public float MaxSpeed = 2;
    public float Acceleration = 0.001f;

    [Space]
    [Header("UI")]
    public int HighScore;
    public int Points;
    public GameObject Points_UI;

    [Space]
    [Header("Particles")]
    public GameObject SmokeParticle;

    [Space]
    [Header("Skin")]
    public int CurrentSkinID;

    [Space]
    [Header("Public references")]
    public Menu Menu;
    public GameObject Map;
    private GameManager manager;

    [Space]
    [Header("Developer tools")]
    public bool End;
    public bool CanAccelerate = true;
    public bool JumpBoost;
    public int Lerps = 0;
    public float Speed;

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
        Speed = StartSpeed;
        canLerpNext = true;
        startPossition = transform.position;
        endPossitionn = startPossition;

        // Jump animation setup
        jumpAnimator = GetComponentInChildren<Animator>();

    }
    private void Update()
    {
        // Jump 2x farther if you click / tap on display
        if (!manager.MainMenuActive)
        {

            if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
            {

                Lerps += 2;
                JumpHeight = 2;
                JumpBoost = true;

            }

        }
    }
    private void FixedUpdate()
    {
        // Check if player has found colorball, using RayCast 
        colorBallCheck();

        // Sets the speed of jumping animation
        jumpAnimator.SetFloat("JumpingTimeMultiplier", Speed);

        // Draws a raycast line in front of the player in Unity Editor, not visible in game
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.yellow);


        // Move player (only) forward
        if (canLerp)
        {
            transform.position = Lerp(startPossition, endPossitionn, timeStartedLerping, Speed + Speed * 0.2f);
            
            // Accelerate player
        }
    }

    // Starts the Lerping animation
    public void StartLerping()
    {
        if (!manager.MainMenuActive && !End)
        {
            timeStartedLerping = Time.time;
            UpdateUI();
            if (JumpBoost)
            {
                endPossitionn.z += LerpDistance * 2;
                Points += 2;
            }
            else
            {
                endPossitionn.z += LerpDistance;
                Points++;
            }
            JumpHeight = 1;
            JumpBoost = false;
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
            // Get the tile below player with raycast
            if (hit.collider.tag == "tile")
            {
                // Start the push animation on the tile below the player
                hit.collider.gameObject.GetComponent<Animator>().SetTrigger("pushed");

                if (hit.collider.gameObject.GetComponent<MeshRenderer>().material.color != gameObject.GetComponentInChildren<MeshRenderer>().material.color)
                {
                    if (!manager.Immortality)
                    {
                        End = true;
                        GetComponentInChildren<Animator>().SetTrigger("End");
                        manager.RestartGame();
                    }
                }
            }
        }
    }
    private void colorBallCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
        {
            // If the object it hits has the tag "ColorBall"
            if (hit.collider.tag == "ColorBall")
            {
                gameObject.GetComponentInChildren<MeshRenderer>().material.color = hit.collider.gameObject.GetComponent<MeshRenderer>().material.color;
                Destroy(hit.collider.gameObject);
            }
        }
    }
}
