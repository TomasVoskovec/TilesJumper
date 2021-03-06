﻿using Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class ChallengeManager : MonoBehaviour
{

    public List<Challenge> Challenges;
    [Space]
    public GameObject Challenge_UI;
    public GameObject MainMenu_UILogo;
    public GameObject MainMenu_UIStartButton;
    [Space]
    public GameObject Challenge_prefab;
    public GameObject Content_parent;
    [Space]
    [Header("PopUp")]
    public GameObject PopUp;
    public TextMeshProUGUI PopUp_name;
    public TextMeshProUGUI PopUp_desc;
    public Image PopUp_bar;
    public TextMeshProUGUI PopUp_progress;
    [Space]
    [Header("Public references")]
    public Menu Menu;
    private Player player;

    private int popUpQueue;
    
    private bool popInProgress;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

   
    void Update()
    {
        
    }

    public void ProgressChallenge(Challenge.GroupName group)
    {
        StartCoroutine(Progress(group));
    }
    IEnumerator popUpTimer()
    {
        yield return new WaitForSeconds(3);

        PopUp.GetComponent<Animator>().SetTrigger("hide");
        popInProgress = false;
    }

    IEnumerator Progress(Challenge.GroupName group)
    {
        foreach (Challenge challenge in Challenges)
        {
            if (challenge.Group == group)
            {
                if (challenge.Progress != challenge.Goal)
                {
                    challenge.Progress++;
                    if (!popInProgress)
                    {
                        
                        if (challenge.Progress == challenge.Goal)
                        {
                            popInProgress = true;

                            PopUp_name.text = challenge.Name;
                            PopUp_desc.text = challenge.Description;
                            PopUp_bar.fillAmount = (float)challenge.Progress / (float)challenge.Goal;
                            PopUp_progress.text = challenge.Progress + "/" + challenge.Goal;
                            PopUp.GetComponent<Animator>().SetTrigger("show");
                            StartCoroutine(popUpTimer());
                            yield return new WaitForSeconds(1);
                            
                        }
                    }
                }

            }
        }
    }
    public void ResetChallengeProgress(Challenge.GroupName group)
    {
        foreach (Challenge challenge in Challenges)
        {
            if (challenge.Group == group)
            {
                if (challenge.Progress != challenge.Goal)
                {

                    challenge.Progress = 0;
                    //popInProgress = false;
                    //PopUp.GetComponent<Animator>().SetTrigger("hide");
                    
                }

            }
        }
    }
    public void ShowChallenge(bool enable)
    {
        //Challenge_UI.SetActive(enable);

        //MainMenu_UI.SetActive(!enable);
        if (GetComponent<GameManager>().MainMenuActive)
        {
            Challenge_UI.GetComponent<Animator>().SetTrigger("move");
            MainMenu_UILogo.GetComponent<Animator>().SetTrigger("move");
            MainMenu_UIStartButton.GetComponent<Animator>().SetTrigger("move");

            if (enable)
            {
                LoadChallenges();

            }
            else
            {

                foreach (Transform child in Content_parent.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
    }
    void LoadChallenges()
    {
        int i = 0;
        List<Challenge> SortedList = Challenges.OrderBy(o => o.Group).ToList();
        foreach (Challenge chal in SortedList)
        {
            GameObject challenge = Instantiate(Challenge_prefab, Content_parent.transform);
            challenge.transform.position = new Vector3(challenge.transform.position.x, challenge.transform.position.y + i, challenge.transform.position.z);
            ChallengeUI challenge_settings = challenge.GetComponent<ChallengeUI>();

            challenge_settings.Name.text = chal.Name;
            challenge_settings.Description.text = chal.Description;
            challenge_settings.Reward.text = chal.Reward.ToString();
            challenge_settings.RewardBar_text.text = chal.Progress + "/" + chal.Goal;
            challenge_settings.RewardBar.fillAmount = (float)chal.Progress / (float)chal.Goal;
            challenge_settings.Difficulty.text = Difficulty(chal.Difficulty);
            challenge_settings.Difficulty_back.color = difficultyBackground(chal.Difficulty);
            challenge_settings.Challenge = chal;
            i -= 400;
        }
    }
    string Difficulty(int id)
    {
        if (id == 1)
        {
            return "EASY";
        }
        else if (id == 2)
        {
            return "MEDIUM";
        }
        else if (id == 3)
        {
            return "HARD";
        }
        else if (id == 4)
        {
            return "MASTER";
        }
        else
        {
            return null;
        }

    }
    Color difficultyBackground(int id)
    {
        if (id == 1)
        {
            return Color.green;
        }
        else if (id == 2)
        {
            return Color.yellow;
        }
        else if (id == 3)
        {
            return Color.red;
        }
        else if (id == 4)
        {
            return Color.black;
        }
        else
        {
            return Color.green;
        }
    }
    public void CompleteChallenge(int ChallengeID)
    {
        foreach (Challenge challenge in Challenges)
        {
            if (challenge.ID == ChallengeID)
            {
                bool isChallengeCompleated = false;

                foreach(int compleatedChallenge in player.CompletedChallanges)
                {
                    if (ChallengeID == compleatedChallenge)
                    {
                        isChallengeCompleated = true;
                    }
                }

                if (!isChallengeCompleated)
                {
                    GameManager manager = GetComponent<GameManager>();
                    manager.AddGoldenTiles(challenge.Reward);
                    challenge.Completed = true;

                    player.CompletedChallanges.Add(ChallengeID);
                    GameDataManager.Save(player);
                }
            }
        }
    }
}
