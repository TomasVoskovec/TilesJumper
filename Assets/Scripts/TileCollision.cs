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
        if (!player.End)
        {
            player.StartLerping();
            
            // Accelerate player (animation speed)
            if (player.CanAccelerate && !player.Manager.MainMenuActive)
            {
                if (player.Speed < player.MaxSpeed)
                {
                    // a(player.Lerps) = player.StartSpeed * ((player.StatrtSpeed + player.Acceleration)^(player.Lerps - 1)
                    player.Speed += player.Speed * player.Acceleration;
                }
            }
        }
    }
}
