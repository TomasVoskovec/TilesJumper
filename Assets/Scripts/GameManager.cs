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

    [Space]
    [Header("Cheats")]
    public bool Immortality;
    [Space]
    [Header("Public references")]
    public GameObject BlackScreen;
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
