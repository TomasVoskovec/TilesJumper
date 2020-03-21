using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject GameLogo;
    public GameObject StartButton;
    private GameManager manager;
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    
    void Update()
    {
        
    }
    public void StartGame()
    {
        GameLogo.GetComponent<Animator>().SetTrigger("move");
        StartButton.GetComponent<Animator>().SetTrigger("move");
        manager.MainMenuActive = false;
    }
    public void BackToMenu()
    {
        GameLogo.GetComponent<Animator>().SetTrigger("move");
        StartButton.GetComponent<Animator>().SetTrigger("move");
        manager.MainMenuActive = true;
    }
}
