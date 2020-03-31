using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBall : MonoBehaviour
{
    private ParticleSystem particles;
    private Light ballLight;

    void Start()
    {
        
        particles = GetComponentInChildren<ParticleSystem>();
        ballLight = GetComponentInChildren<Light>();
        changeColor();
    }

    void changeColor()
    {

        // Change color of the particles to the color of the colorball
        Color color = gameObject.GetComponent<MeshRenderer>().material.color;

        var main = particles.main;
        main.startColor = color;
        // Change color of the light to the color of the colorball
        ballLight.color = color;
    }
}
