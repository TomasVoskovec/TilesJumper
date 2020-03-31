using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // Get players props and players script
    public GameObject Player;
    Player playerScript;

    // Tiles game objects
    public List<GameObject> Tiles = new List<GameObject>();

    // Tiles props
    bool canChangeColor = false;
    int tileNumber = 0;
    public int renderDistance = 10;
    public int WhiteTileNumber;

    float tileScale;
    float tileWhiteZ;
    float tileBlackZ;

    // Colors of tiles
    Color currentColor;
    public Color[] AvailableColors;

    void Start()
    {
        playerScript = Player.GetComponent<Player>();

        currentColor = AvailableColors[0];

        tileScale = Tiles[0].transform.localScale.x;

        tileWhiteZ = 0.07f * tileScale;
        tileBlackZ = 0.0875f * tileScale;
    }

    
    void Update()
    {
        // Generate map
        if (canGanarate())
        {
            generateTile();
        }
    }

    // Check if can generate depending on the render distance
    bool canGanarate()
    {
        if (playerScript.Points + renderDistance <= WhiteTileNumber)
        {
            return false;
        }

        return true;
    }

    // Generate tile function
    void generateTile()
    {
        if (tileNumber >= Tiles.Count)
        {
            tileNumber = 0;
        }

        spawnTile(isTileWhite(tileNumber));
        tileNumber++;
    }

    // Check if tile is "white" (the bigger one / not the black one)
    bool isTileWhite(int num)
    {
        return !(num == 1 || num == 3 || num == 6 || num == 8 || num == 10);
    }

    // Select tile from tiles list and set color of tile
    void spawnTile(bool isWhite = true)
    {
        if(isWhite)
        {
            tileWhiteZ -= 0.035f * tileScale;
            GameObject gameobject = Instantiate(Tiles[tileNumber], new Vector3(0.485f * tileScale, 0, tileWhiteZ), new Quaternion(0, 0, 0, 0));

            if (canChangeColor)
            {
                gameobject.GetComponentInChildren<MeshRenderer>().material.color = GetRandomColor();
                canChangeColor = false;
            }
            else
            {
                gameobject.GetComponentInChildren<MeshRenderer>().material.color = currentColor;
                canChangeColor = true;
            }

            WhiteTileNumber++;
        }
        else
        {
            if(tileNumber == 1 || tileNumber == 6)
            {
                tileBlackZ -= 0.035f * tileScale;
            }
            tileBlackZ -= 0.035f * tileScale;
            Instantiate(Tiles[tileNumber], new Vector3(0, 0, tileBlackZ), new Quaternion(0, 0, 0, 0));
        }
    }

    // Returns random color from own color list
    Color GetRandomColor()
    {
        int i = Random.Range(0, AvailableColors.Length);
        return AvailableColors[i];
    }

}