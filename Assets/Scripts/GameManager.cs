using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Models;

public class GameManager : MonoBehaviour
{
    public bool MainMenuActive;
    public DeathMenu menu;
    [Space]
    [Header("Game")]
    public int GoldenTiles;
    public GameObject GoldenTiles_UI;
    public float GoldenTilesAnimationDuration = 0.01f;
    public bool TutorialActive;
    public bool TutorialPause;
    [Space]
    [Header("Cheats")]
    public bool Immortality;
    [Space]
    [Header("Public references")]
    public GameObject BlackScreen;
    public GameObject MainMenu;
    public GameObject GoldenTilesBlock;
    [Space]
    [Header("Tutorials")]
    public GameObject TutorialBlock1;
    public GameObject TutorialBlock2;
    public GameObject TutorialBlock3;
    public GameObject TutorialBlock4;
    [Space]
    [Header("Audio")]
    
    public AudioSource GameMusic;
    public AudioSource MenuMusic;
    public AudioSource EndMusic;

    Player player;

    void Start()
    {
        MainMenuActive = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        UpdateValues();
        loadData();
        BlackScreenFade("fadeout");
        
    }

    void loadData()
    {
        GoldenTiles = player.GoldenTiles;
        
    }

    
    void Update()
    {
        if (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space) && TutorialActive && !MainMenuActive)
        {

            player.JumpBoost = true;
            player.GetComponentInChildren<Animator>().enabled = true;
            TutorialBlock2.SetActive(false);
            TutorialBlock4.SetActive(false);

        }
    }

    public void Tutorial()
    {
        MainMenu.SetActive(false);
        GoldenTilesBlock.SetActive(false);
        TutorialBlock1.SetActive(true);
        TutorialActive = true;
        TutorialPause = true;
    }
    public void TutorialConfirm()
    {

        player.GetComponentInChildren<Animator>().enabled = true;
        TutorialBlock4.SetActive(false);
        TutorialPause = false;
    }
    public void ColorBallTutorial()
    {
        if (TutorialActive && TutorialPause)
        {
            TutorialBlock4.SetActive(true);
            player.GetComponentInChildren<Animator>().enabled = false;
        }
    }
    public void GameTutorial()
    {
        if (!MainMenuActive && TutorialActive)
        {
            player.JumpBoost = true;
            TutorialBlock2.SetActive(true);
            player.GetComponentInChildren<Animator>().enabled = false;

            
        }
    }
    public void FinishTutorial()
    {
        TutorialActive = false;
        TutorialBlock3.SetActive(false);
    }

    public void UpdateValues()
    {
        GoldenTiles_UI.GetComponent<TextMeshProUGUI>().text = GoldenTiles.ToString();
    }

    public void LoadValues()
    {
        GoldenTiles_UI.GetComponent<TextMeshProUGUI>().text = player.GoldenTiles.ToString();
    }

    public void RestartGame(bool isHighScore)
    {
        menu.ShowDeathMenu(isHighScore);
    }

    public void AddGoldenTiles(int value)
    {
        GoldenTiles = player.GoldenTiles;
        player.GoldenTiles += value;
        GameDataManager.Save(player);
        GoldenTiles_UI.GetComponent<Animator>().SetTrigger("Add");
        //StopAllCoroutines();
        StartCoroutine(AddGoldenTilesAnim());
    }
    public void Remove(int value)
    {
        GoldenTiles = player.GoldenTiles;
        player.GoldenTiles -= value;
        GameDataManager.Save(player);
        GoldenTiles_UI.GetComponent<Animator>().SetTrigger("Add");
        //StopAllCoroutines();
        StartCoroutine(RemoveGoldenTiles());
    }
    IEnumerator AddGoldenTilesAnim()
    {
        while(GoldenTiles < player.GoldenTiles)
        {
            GoldenTiles++;
            UpdateValues();
            yield return new WaitForSeconds(GoldenTilesAnimationDuration);
        }
        if (GoldenTiles == player.GoldenTiles)
        {
            UpdateValues();
            GoldenTiles_UI.GetComponent<Animator>().SetTrigger("Exit");
        }

    }
    IEnumerator RemoveGoldenTiles()
    {
        while (GoldenTiles > player.GoldenTiles)
        {
            GoldenTiles--;
            UpdateValues();
            yield return new WaitForSeconds(GoldenTilesAnimationDuration);
        }
        if (GoldenTiles == player.GoldenTiles)
        {
            UpdateValues();
            GoldenTiles_UI.GetComponent<Animator>().SetTrigger("Exit");
        }
    }

    public void RestoreGameData()
    {
        GameDataManager.Restore();
        Restart();
    }

    public void Restart()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(scene);
    }

    public void BlackScreenFade(string fade)
    {
        BlackScreen.SetActive(true);
        BlackScreen.GetComponent<Animator>().SetTrigger(fade);
    }
}
