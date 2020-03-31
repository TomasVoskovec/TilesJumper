using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            int i = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(i);
        }
    }
}
