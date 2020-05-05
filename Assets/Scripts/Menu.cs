using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    public GameObject GameLogo;
    public GameObject StartButton;
    public GameObject SkinButton;
    public GameObject ChallengeButton;
    private GameManager manager;

    
    
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        manager.MenuMusic.Play();
    }

    
    void Update()
    {
        
    }
    public void StartGame()
    {
        // Move menu out of screen with animation
        GameLogo.GetComponent<Animator>().SetTrigger("move");
        StartButton.GetComponent<Animator>().SetTrigger("move"); 
        StartButton.GetComponent<Button>().interactable = false;
        ChallengeButton.GetComponent<Button>().interactable = false;
        SkinButton.GetComponent<Button>().interactable = false;
        manager.MenuMusic.Stop();
        manager.GameMusic.Play();
        manager.MainMenuActive = false;
        manager.TutorialBlock1.SetActive(false);
        manager.GameTutorial();

    }
    
    public void BackToMenu()
    {
        // Show mainmenu
        GameLogo.GetComponent<Animator>().SetTrigger("move");
        StartButton.GetComponent<Animator>().SetTrigger("move");
        manager.MainMenuActive = true;
    }
}
