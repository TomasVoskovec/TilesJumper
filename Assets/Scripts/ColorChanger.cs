using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public GameObject player;

    Renderer playerRend;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        playerRend = player.GetComponentInChildren<MeshRenderer>();
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        print("hit");

        if(collider == player)
        {
            playerRend.material.color = rend.material.color;
        }
    }
}
