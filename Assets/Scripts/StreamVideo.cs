using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    
    public VideoPlayer Video;
    
    
    void Start()
    {
       
        Video.loopPointReached += loadScene;
    }

    void loadScene(VideoPlayer video)
    {
        SceneManager.LoadScene("Main");

    }

    
}
