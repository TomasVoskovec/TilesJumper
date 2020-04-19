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
        if (Challenge.Completed)
        {
            Claim_button.gameObject.SetActive(false);
            Completed.SetActive(true);
        }else
        {
            if (Challenge.Progress == Challenge.Goal)
            {
                Claim_button.interactable = true;
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
