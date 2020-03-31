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
        Color color = gameObject.GetComponent<MeshRenderer>().material.color;

        var main = particles.main;
        main.startColor = color;

        ballLight.color = color;
    }
}
