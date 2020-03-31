using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollision : MonoBehaviour
{
    // Player props
    private Player player;
 
    void Start()
    {
        // Get player script
        player = GetComponentInParent<Player>();
    }

    // Generate smoke particle
    public void GenerateParticles()
    {
        player.GenerateParticles();
    }

    // Starts push tile animation
    public void PushTile()
    {
        player.PushTile();
    }

    // Starts lerping animation
    public void MoveToNextTile()
    {
        player.Lerps = 1;
        player.StartLerping();
    }
}
