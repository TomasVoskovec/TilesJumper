using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    
    private ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

   
    void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                // Destroy particles that finished their loop/animation
                Destroy(gameObject);
            }
        }
    }
}
