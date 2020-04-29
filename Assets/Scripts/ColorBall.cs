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
        //ballLight = GetComponentInChildren<Light>();
        ChangeColor();
    }
    private void Awake()
    {
        particles = GetComponentInChildren<ParticleSystem>();
        //ballLight = GetComponentInChildren<Light>();
    }

    public void ChangeColor()
    {

        // Change color of the particles to the color of the colorball
        Color color = gameObject.GetComponent<MeshRenderer>().material.color;
        ParticleSystem.MainModule settings = particles.main;
        color.a = 1;
        settings.startColor = new ParticleSystem.MinMaxGradient(color);

        // Change color of the light to the color of the colorball
        //ballLight.color = color;
    }
}
