using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> Tiles;

    int tileNumber = 0;
    float tileWhiteZ = 0.07f;
    float tileBlackZ = 0.035f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(tileNumber+1 >= Tiles.Count)
            {
                tileNumber = 0;
            }
            else
            {
                tileNumber++;
            }

            if (tileNumber == 0)
            {
                
            }
        }
        
    }

    void spawnTile(bool isWhite = true)
    {
        if(isWhite)
        {
            tileWhiteZ += 0.035f;
            Instantiate(Tiles[tileNumber], new Vector3(1f,1f), new Quaternion(0,0,0,0));
        }
    }
}
