using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Space]
    [Header("Public references")]

    // Get players props and players script
    public GameObject Player;
    Player playerScript;

    // Tiles game objects
    public List<GameObject> Tiles = new List<GameObject>();

    // Changing color ball object
    public GameObject ColorBall;

    // Tiles props
    bool canChangeColor = false;
    int tileNumber = 0;

    [Space]
    [Header("Map Settings")]

    // Map settings
    public int RenderDistance = 10;
    public int ChangeColorAfter = 5;

    float tileScale;
    float tileWhiteZ;
    float tileBlackZ;

    [Space]
    [Header("Tile collors")]

    // Colors of tiles
    public List<Color> AvailableColors;

    [Space]
    [Header("Developer tools")]

    // Developer data
    public Color CurrentColor;
    public int WhiteTileNumber;

    void Start()
    {
        playerScript = Player.GetComponent<Player>();

        CurrentColor = AvailableColors[0];

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
        if (playerScript.Points + RenderDistance <= WhiteTileNumber)
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

            if (canChangeColor && WhiteTileNumber >= ChangeColorAfter)
            {
                if(percentageChance(0.5f))
                {
                    if (percentageChance(0.3f))
                    {
                        GenerateColorBall(gameobject);
                    }
                    var block = new MaterialPropertyBlock();
                    Color color = otherRandomColor();


                    block.SetColor("_BaseColor", color); 
                    gameobject.GetComponentInChildren<MeshRenderer>().SetPropertyBlock(block);
                    gameobject.GetComponentInChildren<Tile>().TileColor = color;
                    canChangeColor = false;
                }
                else
                {
                    var block = new MaterialPropertyBlock();
                    block.SetColor("_BaseColor", CurrentColor);
                    gameobject.GetComponentInChildren<MeshRenderer>().SetPropertyBlock(block);
                    gameobject.GetComponentInChildren<Tile>().TileColor = CurrentColor;
                    canChangeColor = true;
                }
            }
            else
            {
                var block = new MaterialPropertyBlock();
                block.SetColor("_BaseColor", CurrentColor);
                gameobject.GetComponentInChildren<MeshRenderer>().SetPropertyBlock(block);
                gameobject.GetComponentInChildren<Tile>().TileColor = CurrentColor;

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

    // Returns random color from custom color list
    Color randomColor()
    {
        int i = Random.Range(0, AvailableColors.Count);
        return AvailableColors[i];
    }

    // Returns a random color from a custom color list except the current color
    Color otherRandomColor()
    {
        List<Color> colors = new List<Color>(AvailableColors);
        colors.Remove(CurrentColor);

        int i = Random.Range(0, colors.Count);
        return colors[i];
    }

    // Returns true if generated (random) chance is equals or higher than required chance (1.0 = 100%)
    bool percentageChance(float chance)
    {
        float requiredChance = 1f - chance;
        float randomPercentage = Random.Range(0.0f, 1.0f);

        return randomPercentage >= requiredChance;
    }
    private void GenerateColorBall(GameObject tile)
    {
        GameObject ball = Instantiate(ColorBall, new Vector3(-1.56f, tile.transform.position.y + 0.5f, tile.transform.position.z - (0.035f * tileScale)), ColorBall.transform.rotation);

        Color ballColor = otherRandomColor();

        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", ballColor);
        ball.GetComponent<Renderer>().SetPropertyBlock(block);

        
        ball.GetComponent<ColorBall>().ChangeColor();
        ball.GetComponent<ColorBall>().BallColor = ballColor;

        CurrentColor = ballColor;
    }

}