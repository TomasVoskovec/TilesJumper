using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage raw;
    public VideoPlayer Video;
    public AudioSource Audio;
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        Video.Prepare();
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        while(!Video.isPrepared)
        {
            yield return waitForSeconds;
            break;
        }
        raw.texture = Video.texture;
        Video.Play();
        Audio.Play();
    }
}
