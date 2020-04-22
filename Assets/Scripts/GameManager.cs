using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
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
    void Start()
    {
        MainMenuActive = true;
        UpdateValues();
    }

    
    void Update()
    {
        
    }

    public void UpdateValues()
    {
        
        GoldenTiles_UI.GetComponent<TextMeshProUGUI>().text = GoldenTiles.ToString();
    }
    public void RestartGame(bool isHighScore)
    {
        menu.ShowDeathMenu(isHighScore);
    }

    public void Add(int add)
    {
        GoldenTiles_UI.GetComponent<Animator>().SetTrigger("Add");
        //StopAllCoroutines();
        StartCoroutine(AddGoldenTiles(add));
    }
    public void Remove(int remove)
    {
        GoldenTiles_UI.GetComponent<Animator>().SetTrigger("Add");
        //StopAllCoroutines();
        StartCoroutine(RemoveGoldenTiles(remove));
    }
    IEnumerator AddGoldenTiles(int Value)
    {
        int i = 0;
        while(i < Value)
        {
            GoldenTiles++;
            i++;
            UpdateValues();
            yield return new WaitForSeconds(0.01f);
        }
        if (i == Value)
        {
            GoldenTiles_UI.GetComponent<Animator>().SetTrigger("Exit");
        }

    }
    IEnumerator RemoveGoldenTiles(int Value)
    {
        int i = Value;
        while (i > 0)
        {
            GoldenTiles--;
            i--;
            UpdateValues();
            yield return new WaitForSeconds(0.01f);
        }
        if (i == Value)
        {
            GoldenTiles_UI.GetComponent<Animator>().SetTrigger("Exit");
        }

    }
}
