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
    [Space]
    [Header("Cheats")]
    public bool Immortality;
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
        StartCoroutine(AddGoldenTilesAnim(value));
    }
    public void Remove(int value)
    {
        GoldenTiles = player.GoldenTiles;
        player.GoldenTiles -= value;
        GameDataManager.Save(player);
        GoldenTiles_UI.GetComponent<Animator>().SetTrigger("Add");
        //StopAllCoroutines();
        StartCoroutine(RemoveGoldenTiles(value));
    }
    IEnumerator AddGoldenTilesAnim(int value)
    {
        int i = 0;
        while(i < value)
        {
            GoldenTiles++;
            i++;
            UpdateValues();
            yield return new WaitForSeconds(0.01f);
        }
        if (i == value)
        {
            GoldenTiles = player.GoldenTiles;
            UpdateValues();
            GoldenTiles_UI.GetComponent<Animator>().SetTrigger("Exit");
        }

    }
    IEnumerator RemoveGoldenTiles(int value)
    {
        int i = value;
        while (i > 0)
        {
            GoldenTiles--;
            i--;
            UpdateValues();
            yield return new WaitForSeconds(0.01f);
        }
        if (i == value)
        {
            GoldenTiles = player.GoldenTiles;
            UpdateValues();
            GoldenTiles_UI.GetComponent<Animator>().SetTrigger("Exit");
        }

    }
}
