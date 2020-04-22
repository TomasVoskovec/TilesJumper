using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ChallengeUI : MonoBehaviour
{

    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Reward;
    public TextMeshProUGUI RewardBar_text;
    public Image RewardBar;
    public TextMeshProUGUI Difficulty;
    public Image Difficulty_back;
    public Button Claim_button;
    public GameObject Completed;

    public Challenge Challenge;

    private ChallengeManager manager;
    private Player player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (Challenge.Completed)
        {
            Claim_button.gameObject.SetActive(false);
            Completed.SetActive(true);
        }
        else
        {
            if (Challenge.Progress == Challenge.Goal)
            {
                bool isCollected = false;
                if (player.CompletedChallanges != null && player.CompletedChallanges.Count > 0)
                {
                    foreach (int compleatedChallenge in player.CompletedChallanges)
                    {
                        if (compleatedChallenge == Challenge.ID)
                        {
                            isCollected = true;
                        }
                    }
                }
                if (isCollected)
                {
                    Claim_button.gameObject.SetActive(false);
                    Completed.SetActive(true);
                }
                else
                {
                    Claim_button.interactable = true;
                }
            }
            else
            {
                Claim_button.interactable = false;
            }
        }
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ChallengeManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void ClaimReward()
    {
        Claim_button.gameObject.SetActive(false);
        Completed.SetActive(true);
        manager.CompleteChallenge(Challenge.ID);
    }

}
