using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<GameObject> Tiles;

    // Tiles props
    int tileNumber = 0;
    float tileWhiteZ = 0.07f;
    float tileBlackZ = 0.0875f;

    // Speed props
    public float StartSpawningSpeed = 1f;
    public float Acceleration = 0f;
    float spawningSpeed;

    // Start is called before the first frame update
    void Start()
    {
        spawningSpeed = StartSpawningSpeed;
        StartCoroutine(generateMap());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator generateMap()
    {
        yield return new WaitForSeconds(0f);
        while (true)
        {
            if (tileNumber >= Tiles.Count)
            {
                tileNumber = 0;
            }

            spawnTile(isTileWhite(tileNumber));
            tileNumber++;
            spawningSpeed -= Acceleration;
            yield return new WaitForSeconds(spawningSpeed);
        }
    }

    bool isTileWhite(int num)
    {
        return !(num == 1 || num == 3 || num == 6 || num == 8 || num == 10);
    }

    void spawnTile(bool isWhite = true)
    {
        if(isWhite)
        {
            tileWhiteZ -= 0.035f;
            Instantiate(Tiles[tileNumber], new Vector3(0.485f, 0, tileWhiteZ), new Quaternion(0, 0, 0, 0));

            print("Is white");
        }
        else
        {
            if(tileNumber == 1 || tileNumber == 6)
            {
                tileBlackZ -= 0.035f;
            }
            tileBlackZ -= 0.035f;
            Instantiate(Tiles[tileNumber], new Vector3(0, 0, tileBlackZ), new Quaternion(0, 0, 0, 0));

            print("Is black");
        }
    }
}