using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skin
{
    public int ID;
    public RarityName Rarity;
    public string Name;
    public Material SkinMaterial;
    public Material TrailMaterial;
    public Mesh SkinMesh;
    public int Price;

    public enum RarityName
    {
        Basic,
        Rare,
        Mythic
        
    }
}
