using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDestroyer : MonoBehaviour
{
    // Destroy tiles behind the player
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent != null)
        {
            Destroy(other.transform.parent.gameObject);
        }else
        {
            Destroy(other.gameObject);
        }
    }
}
