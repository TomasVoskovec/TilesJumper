using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDestroyer : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        print("trigger");
        if (other.gameObject.tag == "tile")
        {
            print("tiles");
            Destroy(other.gameObject);
        }
    }
}
