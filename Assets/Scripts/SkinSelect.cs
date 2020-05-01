using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Models;
using UnityEngine.Rendering.PostProcessing;
public class SkinSelect : MonoBehaviour
{
    [Header("Public references")]
    public Player player;
    public ParticleSystemRenderer Trail;
    public GameObject MainMenu;
    public GameObject ShowLight;
    
    [Header("UI")]
    public GameObject SkinSelectCam;
    public GameObject SkinSelectUI;
    public GameObject Points_UI;
    public GameObject LockImage;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Rarity;
    public GameObject PriceGameobject;
    public TextMeshProUGUI Price_text;
    public GameObject ActivatedSkinImage;
    public Button Next;
    public Button Activate;
    public Button Buy;
    public Button Previous;
    [Space]
    private Mesh currentPlayerMesh;
    private Material currentPlayerMaterial;
    public Material currentTrailMaterial;
    private GameManager manager;
    public Skin SelectedSkin;
    public bool SkinSelectionActive;
    public List<Skin> Skins;

    int selectedSkinIndex;
   

    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
       
    }

    
    void Update()
    {
        
    }
    private void skinCheck()
    {
        if (SkinSelectionActive)
        {
            if (Skins.IndexOf(SelectedSkin) + 1 >= Skins.Count)
            {
                Next.interactable = false;
            }
            else
            {
                Next.interactable = true;
            }

            if (Skins.IndexOf(SelectedSkin) <= 0)
            {
                Previous.interactable = false;
            }
            else
            {
                Previous.interactable = true;
            }
        }

        skinUnlocked();
    }
    private void skinUnlocked()
    {
        if (player.UnlockedSkins.Contains(SelectedSkin))
        {
            // If the skin is unlocked enable light and disable the "lock" image
            ShowLight.SetActive(true);
            LockImage.SetActive(false);
            PriceGameobject.SetActive(false);

            // If the selected skin is the current active skin show image
            if (SelectedSkin == player.CurrentSkin)
            {
                Activate.interactable = false;
                ActivatedSkinImage.SetActive(true);
            }
            else
            {
                Activate.gameObject.SetActive(true);
                Activate.interactable = true;
                ActivatedSkinImage.SetActive(false);
            }
        }
        else
        {
             // If the selected skin is not unlocked, disable light and show "lock" image
            ActivatedSkinImage.SetActive(false);
            LockImage.SetActive(true);
            PriceGameobject.SetActive(true);
            Price_text.text = Skins[SelectedSkin.ID].Price.ToString();
            if (player.GoldenTiles >= Skins[SelectedSkin.ID].Price)
            {
                Buy.interactable = true;

            }else
            {
                Buy.interactable = false;
            }
            ShowLight.SetActive(false);

            Activate.gameObject.SetActive(false);
            
        }
    }
    // Transition in or out of skin selection
    public void SkinSelection(bool enable)
    {
        SkinSelectCam.SetActive(enable);
        SkinSelectUI.SetActive(enable);
        MainMenu.SetActive(!enable);
        Points_UI.SetActive(!enable);
        SkinSelectionActive = enable;
        
        SelectedSkin = player.CurrentSkin;
        // Check which skins are avaivable
        skinCheck();
        // Set player animation to "showcase" animation
        player.GetComponentInChildren<Animator>().SetBool("showcasing", enable);

        if (enable)
        {
            
            currentPlayerMesh = player.GetComponentInChildren<MeshFilter>().mesh;
            currentPlayerMaterial = player.GetComponentInChildren<MeshRenderer>().material;
            currentTrailMaterial = Trail.material;
            // Display skin name
            Rarity.text = SelectedSkin.Rarity.ToString();
            Rarity.color = rarityCheck(SelectedSkin.Rarity.ToString());
            Name.text = SelectedSkin.Name;
            player.GetComponentInChildren<MeshFilter>().mesh = SelectedSkin.SkinMesh;
        }
        else
        {
            player.GetComponentInChildren<MeshFilter>().mesh = currentPlayerMesh;
            player.GetComponentInChildren<MeshRenderer>().material = currentPlayerMaterial;
            Trail.material = currentTrailMaterial;
        }
    }
    // Show next skin
    public void NextSkin()
    {
        SelectedSkin = Skins[Skins.IndexOf(SelectedSkin) + 1];
        player.gameObject.GetComponentInChildren<MeshFilter>().mesh = SelectedSkin.SkinMesh;
        player.GetComponentInChildren<MeshRenderer>().material = SelectedSkin.SkinMaterial;
        skinCheck();
        Rarity.text = SelectedSkin.Rarity.ToString();
        Rarity.color = rarityCheck(SelectedSkin.Rarity.ToString());
        Name.text = SelectedSkin.Name;
    }
    // Show previous skin
    public void PreviousSkin()
    {
        SelectedSkin = Skins[Skins.IndexOf(SelectedSkin) - 1];
        player.gameObject.GetComponentInChildren<MeshFilter>().mesh = SelectedSkin.SkinMesh;
        player.GetComponentInChildren<MeshRenderer>().material = SelectedSkin.SkinMaterial;
        skinCheck();
        Rarity.text = SelectedSkin.Rarity.ToString();
        Rarity.color = rarityCheck(SelectedSkin.Rarity.ToString());
        Name.text = SelectedSkin.Name;
    }

    //Activate selected skin
    public void ActivateSkin()
    {
        currentPlayerMesh = SelectedSkin.SkinMesh;
        currentPlayerMaterial = SelectedSkin.SkinMaterial;
        currentTrailMaterial = SelectedSkin.TrailMaterial;
        player.CurrentSkin = SelectedSkin;
        skinCheck();

        GameDataManager.Save(player);
    }
    public void BuySkin()
    {
        if (player.GoldenTiles >= SelectedSkin.Price)
        {
            player.UnlockedSkins.Add(SelectedSkin);

            LockImage.GetComponent<Animator>().SetTrigger("fade");
            skinCheck();
            manager.Remove(SelectedSkin.Price);

            GameDataManager.Save(player);
        }
    }
    private Color rarityCheck(string value)
    {
        if (value == "Basic")
        {
            return Color.white;
        }
        else if (value == "Rare")
        {
            return Color.cyan;
        }
        else if (value == "Mythic")
        {
            return Color.yellow;
        }
        else
        {
            return Color.white;
        }
    }


}
