using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public bool MainMenuActive;
    public DeathMenu menu;
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
