using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class DeathMenu : MonoBehaviour
{
    
    public TextMeshProUGUI CurrentScore_text;
    public TextMeshProUGUI BestScore_text;
    public GameObject HighScoreWarning;
    private Player player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

   
    void Update()
    {
        
    }
    public void ShowDeathMenu()
    {
        gameObject.GetComponent<Animator>().SetTrigger("move");
        CurrentScore_text.text = player.Points.ToString();
        if (player.HighScore < player.Points)
        {
            player.HighScore = player.Points;
            BestScore_text.text = player.HighScore.ToString();
            HighScoreWarning.SetActive(true);
        }else
        {
            BestScore_text.text = player.HighScore.ToString();
        }
        
    }
    public void Restart()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(scene);
    }
}
