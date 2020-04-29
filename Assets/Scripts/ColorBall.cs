using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBall : MonoBehaviour
{
    public ParticleSystem EmitParticles;
    public ParticleSystem CollectParticles;
    private Light ballLight;

    public Color BallColor;
    private bool canDestroy;
    void Start()
    {
        
       
        
        ChangeColor();
    }
    

    public void ChangeColor()
    {

        // Change color of the particles to the color of the colorball
        Color color = BallColor;
        ParticleSystem.MainModule settings = EmitParticles.main;
        color.a = 1;
        settings.startColor = new ParticleSystem.MinMaxGradient(color);

        ParticleSystem.MainModule settings2 = CollectParticles.main;
        settings2.startColor = new ParticleSystem.MinMaxGradient(color);
        // Change color of the light to the color of the colorball
        //ballLight.color = color;
    }
    public void CollectParticlesEmit()
    {
        Instantiate(CollectParticles, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z), CollectParticles.transform.rotation);
    }
}
