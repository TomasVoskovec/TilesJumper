using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool MainMenuActive;
    public DeathMenu menu;
    [Space]
    [Header("Cheats")]
    public bool Immortality;
    void Start()
    {
        MainMenuActive = true;
    }

    
    void Update()
    {
        
    }

    public void RestartGame()
    {
        menu.ShowDeathMenu();
    }
}
