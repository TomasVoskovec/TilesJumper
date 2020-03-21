using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCollision : MonoBehaviour
{
    private Player player;
 
    void Start()
    {
        player = GetComponentInParent<Player>();
    }


    void Update()
    {
        
    }

    public void GenerateParticles()
    {
        Instantiate(player.SmokeParticle, new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z), player.SmokeParticle.transform.rotation);
    }
    public void PushTile()
    {
        player.PushTile();
    }
    public void MoveToNextTile()
    {
        player.lerps = 1;
        //player.JumpDistance = 1;
        player.StartLerping();
    }
}
