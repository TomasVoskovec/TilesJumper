using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    public GameManager Manager;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void FadeRestart()
    {
        Manager.Restart();
        
    }
    public void FadeExit()
    {
        gameObject.SetActive(false);
    }
}
