using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Player;
    Player playerScript;
    public List<GameObject> Tiles = new List<GameObject>();

    // Tiles props
    bool canChangeColor = false;
    int tileNumber = 0;
    public int renderDistance = 10;
    public int WhiteTileNumber;

    float tileScale;
    float tileWhiteZ;
    float tileBlackZ;

    float WhiteSpawnStartTime;

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
        if (canGanarate())
        {
            generateTile();
        }
    }

    bool canGanarate()
    {
        if (playerScript.Points + renderDistance <= WhiteTileNumber)
        {
            return false;
        }

        return true;
    }

    void generateTile()
    {
        if (tileNumber >= Tiles.Count)
        {
            tileNumber = 0;
        }

        spawnTile(isTileWhite(tileNumber));
        tileNumber++;
    }

    bool isTileWhite(int num)
    {
        return !(num == 1 || num == 3 || num == 6 || num == 8 || num == 10);
    }

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

    Color GetRandomColor()
    {
        int i = Random.Range(0, AvailableColors.Length);
        return AvailableColors[i];
    }

}