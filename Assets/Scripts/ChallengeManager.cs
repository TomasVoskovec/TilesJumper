using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{

    public Challenge[] Challenges;
    [Space]
    public GameObject Challenge_UI;
    public GameObject MainMenu_UI;
    [Space]
    public GameObject Challenge_prefab;
    public GameObject Content_parent;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void ShowChallenge(bool enable)
    {
        Challenge_UI.SetActive(enable);
        MainMenu_UI.SetActive(!enable);

        if (enable)
        {
            LoadChallenges();
        }else
        {
            foreach (Transform child in Content_parent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
    void LoadChallenges()
    {
        int i = 0;
        foreach(Challenge chal in Challenges)
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
            i -= 500;
        }
    }
    string Difficulty(int id)
    {
        if (id == 1)
        {
            return "EASY";
        }else if (id == 2)
        {
            return "MEDIUM";
        }else if (id == 3)
        {
            return "HARD";
        }else
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
        else
        {
            return Color.green;
        }
    }
    public void CompleteChallenge(int ChallengeID)
    {
        foreach(Challenge challenge in Challenges)
        {
            if (challenge.ID == ChallengeID)
            {
                GameManager manager = GetComponent<GameManager>();
                //manager.GoldenTiles += challenge.Reward;
                manager.Add(challenge.Reward);
                challenge.Completed = true;
            }
        }
    }
}
