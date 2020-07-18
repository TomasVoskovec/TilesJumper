using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject SettingsUI;
    private ChallengeManager challengemanager;
    // Start is called before the first frame update
    void Start()
    {
        challengemanager = GetComponent<ChallengeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSettings(bool value)
    {
        SettingsUI.SetActive(value);
        challengemanager.MainMenu_UILogo.GetComponent<Animator>().SetTrigger("move");
        challengemanager.MainMenu_UIStartButton.GetComponent<Animator>().SetTrigger("move");
    }

    public void SetQuality(int value)
    {
        QualitySettings.SetQualityLevel(value);
        print("Quality set to " + value);
    }
}
