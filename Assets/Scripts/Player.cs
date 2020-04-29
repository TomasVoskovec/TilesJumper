using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.Scripts.Models;
using System.Linq;

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

    // Skin props
    SkinSelect skinSelect;

    [Space]
    [Header("GameData")]
    public bool FirstGame = true;
    public int GoldenTiles;
    public int HighScore;
    public int JumpBoosts;
    public int OverallJumpBoosts;
    public int Deaths;
    public int CollectedColorChangers;
    public List<int> CompletedChallanges;
    public List<Skin> UnlockedSkins;
    public List<Challenge> ChallengeProgress;

    [Space]
    [Header("Animation")]
    public float LerpDistance;
    public float JumpHeight;

    [Space]
    [Header("Gameplay")]
    public int CanJumpBoostAfter = 2;
    public float StartSpeed = 1;
    public float MaxSpeed = 2;
    public float Acceleration = 0.001f;

    [Space]
    [Header("UI")]
    public int Points;
    public GameObject Points_UI;

    [Space]
    [Header("Particles")]
    public GameObject SmokeParticle;
    public ParticleSystem BoostParticles;

    [Space]
    [Header("Skin")]
    public Skin CurrentSkin;

    [Space]
    [Header("Public references")]
    public Menu Menu;
    public GameObject Map;
    public GameManager Manager;
    public GameObject SkinManager;
    public ChallengeManager ChallengeManager;

    [Space]
    [Header("Developer tools")]
    public bool End;
    public bool CanAccelerate = true;
    public bool JumpBoost;
    public int Jumps;
    public int JumpBoostsinARow;
    public float Speed;

    void Start()
    {
        // Load skin select sript
        skinSelect = SkinManager.GetComponent<SkinSelect>();

        // Load saved player data
        loadGameData();

        // First game action
        if (FirstGame)
        {
            firstGame();
        }

        // Menu
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

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

        // Load UI values
        Manager.LoadValues();
    }

    private void Update()
    {
        // Jump 2x farther if you click / tap on display
        if (!Manager.MainMenuActive)
        {

            if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))
            {

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
        }
    }

    void firstGame()
    {
        UnlockedSkins.Add(skinSelect.Skins[0]);

        FirstGame = false;

        GameDataManager.Save(this);
    }

    // Starts the Lerping animation
    public void StartLerping()
    {
        if (!Manager.MainMenuActive && !End)
        {
            Jumps++;
            timeStartedLerping = Time.time;
            UpdateUI();
            if (JumpBoost && Jumps >= CanJumpBoostAfter)
            {
                endPossitionn.z += LerpDistance * 2;
                Points += 2;
                JumpBoosts++;
                OverallJumpBoosts++;
                JumpBoostsinARow++;
                ChallengeManager.ProgressChallenge(Challenge.GroupName.JumpMaster);
                ChallengeManager.ProgressChallenge(Challenge.GroupName.ScoreJumper);
                ChallengeManager.ProgressChallenge(Challenge.GroupName.ScoreJumper);
                BoostParticles.Play();
            }
            else
            {
                BoostParticles.Stop();
                endPossitionn.z += LerpDistance;
                Points++;
                ChallengeManager.ResetChallengeProgress(Challenge.GroupName.JumpMaster);
                ChallengeManager.ProgressChallenge(Challenge.GroupName.ScoreJumper);
                JumpBoostsinARow = 0;
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
                    if (!Manager.Immortality)
                    {
                        if (!Manager.MainMenuActive)
                        {
                            endGame();
                        }
                        else
                        {
                            // Set color of Player
                            gameObject.GetComponentInChildren<MeshRenderer>().material.color = hit.collider.gameObject.GetComponent<MeshRenderer>().material.color;
                            // Set color of Jump Boost particles
                            ParticleSystem.MainModule settings = BoostParticles.main;
                            Color color = hit.collider.gameObject.GetComponent<MeshRenderer>().material.color;
                            color.a = 1;
                            settings.startColor = new ParticleSystem.MinMaxGradient(color);
                        }
                    }
                }
            }
        }
    }

    void endGame()
    {
        bool isHighScore = false;

        // Chceck if is new high score
        if (Points > HighScore)
        {
            HighScore = Points;
            isHighScore = true;
        }

        // Update challenges
        ChallengeManager.ProgressChallenge(Challenge.GroupName.DeathJumper);
        ChallengeManager.ResetChallengeProgress(Challenge.GroupName.ScoreJumper);
        updatePlayerData();

        // Update deaths
        Deaths++;

        // Show end menu and restart game
        End = true;
        GetComponentInChildren<Animator>().SetTrigger("End");
        Manager.GameMusic.Stop();
        Manager.EndMusic.Play();

        // Save game data
        GameDataManager.Save(this);

        // Restart game
        Manager.RestartGame(isHighScore);
    }

    void updatePlayerData()
    {
        // Reset in one game challenges
        ChallengeManager.ResetChallengeProgress(0);
        ChallengeProgress = updateChallengeProgress();
    }

    List<Challenge> updateChallengeProgress()
    {
        List<Challenge> challengeProgress = new List<Challenge>();
        foreach (Challenge challenge in ChallengeManager.Challenges)
        {
            if (challenge.Progress > 0)
            {
                challengeProgress.Add(challenge);
            }
        }
        return challengeProgress;
    }

    // Load game data saved in .bin file
    void loadGameData()
    {
        GameData loadedData = GameDataManager.Load();

        if(loadedData != null)
        {
            this.FirstGame = loadedData.FirstGame;
            this.GoldenTiles = loadedData.GoldenTiles;
            this.HighScore = loadedData.HighScore;
            this.OverallJumpBoosts = loadedData.OverallJumpBoosts;
            this.CurrentSkin = loadCurrentSkin(loadedData.CurrentSkinID);
            this.Deaths = loadedData.Deaths;
            this.CollectedColorChangers = loadedData.CollectedColorChangers;
            this.CompletedChallanges = (isNullOrEmpty(loadedData.CompletedChallenges)) ? new List<int>() : new List<int>(loadedData.CompletedChallenges);
            this.UnlockedSkins = loadUnlockedSkins(loadedData.UnlockedSkins);
            this.ChallengeProgress = loadChallengeProgress(loadedData.ChallengeProgress);
        }
    }

    // Load skin from game skins
    Skin loadCurrentSkin(int skinId)
    {
        Skin currentSkin = new Skin();
        foreach(Skin skin in skinSelect.Skins)
        {
            if(skin.ID == skinId)
            {
                currentSkin = skin;
            }
        }
        loadSkin(currentSkin);

        return currentSkin;
    }

    bool isNullOrEmpty(int[] array)
    {
        return (array == null || array.Length == 0);
    }

    // Load unlocked skins from game data (by skin IDs)
    List<Skin> loadUnlockedSkins(int[] skinsId)
    {
        List<Skin> skins = new List<Skin>();
        if(!isNullOrEmpty(skinsId))
        {
            foreach(int skinId in skinsId)
            {
                foreach(Skin skin in skinSelect.Skins)
                {
                    if(skin.ID == skinId)
                    {
                        skins.Add(skin);
                    }
                }
            }
        }

        return skins;
    }

    // Load current skin (change player mesh and material)
    void loadSkin(Skin skin)
    {
        GetComponentInChildren<MeshFilter>().mesh = skin.SkinMesh;
        GetComponentInChildren<MeshRenderer>().material = skin.SkinMaterial;
    }

    // Load challenge progres from game data (int dictionary to challenge class)
    List<Challenge> loadChallengeProgress(Dictionary<int, int> challengeProgress)
    {
        List<Challenge> challenges = new List<Challenge>();
        
        if (challengeProgress != null && challengeProgress.Count > 0)
        {
            foreach (KeyValuePair<int, int> progress in challengeProgress)
            {
                Challenge selectedChallenge = ChallengeManager.Challenges[progress.Key];
                selectedChallenge.Progress = progress.Value;
                challenges.Add(selectedChallenge);
            }
        }

        return challenges;
    }

    void colorBallCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
        {
            // If the object it hits has the tag "ColorBall"
            if (hit.collider.tag == "ColorBall")
            {
                CollectedColorChangers++;


                var block = new MaterialPropertyBlock();

                // You can look up the property by ID instead of the string to be more efficient.
                block.SetColor("_BaseColor", hit.collider.gameObject.GetComponent<MeshRenderer>().material.color);

                // You can cache a reference to the renderer to avoid searching for it.
                GetComponentInChildren<Renderer>().SetPropertyBlock(block);
                
                ParticleSystem.MainModule settings = BoostParticles.main;
                Color color = hit.collider.gameObject.GetComponent<MeshRenderer>().material.color;
                color.a = 1;
                settings.startColor = new ParticleSystem.MinMaxGradient(color);
                Destroy(hit.collider.gameObject);
            }
        }
    }

    public void RestoreGameData()
    {
        GameDataManager.Restore();
        loadGameData();
        Manager.RestartGame(false);
    }
}
