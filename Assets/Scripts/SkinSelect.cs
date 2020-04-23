﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts.Models;

public class SkinSelect : MonoBehaviour
{
    [Header("Public references")]
    public Player player;
    public GameObject MainMenu;
    public GameObject ShowLight;
    [Header("UI")]
    public GameObject SkinSelectCam;
    public GameObject SkinSelectUI;
    public GameObject Points_UI;
    public GameObject LockImage;
    public TextMeshProUGUI Name;
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
    private GameManager manager;
    public int SelectedSkinID;
    public bool SkinSelectionActive;
    public Skin[] Skins;
    

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
            if (SelectedSkinID + 1 >= Skins.Length)
            {
                Next.interactable = false;
            }
            else
            {
                Next.interactable = true;
            }

            if (SelectedSkinID <= 0)
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
        if (Skins[SelectedSkinID].Unlocked)
        {
            // If the skin is unlocked enable light and disable the "lock" image
            ShowLight.SetActive(true);
            LockImage.SetActive(false);
            PriceGameobject.SetActive(false);

            // If the selected skin is the current active skin show image
            if (SelectedSkinID == player.CurrentSkinID)
            {
                Activate.interactable = false;
                ActivatedSkinImage.SetActive(true);
            }else
            {
                Activate.gameObject.SetActive(true);
                Activate.interactable = true;
                ActivatedSkinImage.SetActive(false);
            }
        }else
        {
             // If the selected skin is not unlocked, disable light and show "lock" image
            ActivatedSkinImage.SetActive(false);
            LockImage.SetActive(true);
            PriceGameobject.SetActive(true);
            Price_text.text = Skins[SelectedSkinID].Price.ToString();
            if (manager.GoldenTiles >= Skins[SelectedSkinID].Price)
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
        
        SelectedSkinID = 0;
        // Check which skins are avaivable
        skinCheck();
        // Set player animation to "showcase" animation
        player.GetComponentInChildren<Animator>().SetBool("showcasing", enable);

        if (enable)
        {
            
            currentPlayerMesh = player.GetComponentInChildren<MeshFilter>().mesh;
            currentPlayerMaterial = player.GetComponentInChildren<MeshRenderer>().material;
            // Display skin name
            Name.text = Skins[SelectedSkinID].Name;
            player.GetComponentInChildren<MeshFilter>().mesh = Skins[SelectedSkinID].SkinMesh;
        }
        else
        {
            player.GetComponentInChildren<MeshFilter>().mesh = currentPlayerMesh;
            player.GetComponentInChildren<MeshRenderer>().material = currentPlayerMaterial;
        }
    }
    // Show next skin
    public void NextSkin()
    {
        player.gameObject.GetComponentInChildren<MeshFilter>().mesh = Skins[SelectedSkinID + 1].SkinMesh;
        player.GetComponentInChildren<MeshRenderer>().material = Skins[SelectedSkinID + 1].SkinMaterial;
        SelectedSkinID++;
        skinCheck();
        Name.text = Skins[SelectedSkinID].Name;
    }
    // Show previous skin
    public void PreviousSkin()
    {
        player.gameObject.GetComponentInChildren<MeshFilter>().mesh = Skins[SelectedSkinID - 1].SkinMesh;
        player.GetComponentInChildren<MeshRenderer>().material = Skins[SelectedSkinID - 1].SkinMaterial;
        SelectedSkinID--;
        skinCheck();
        Name.text = Skins[SelectedSkinID].Name;
    }

    //Activate selected skin
    public void ActivateSkin()
    {
        currentPlayerMesh = Skins[SelectedSkinID].SkinMesh;
        currentPlayerMaterial = Skins[SelectedSkinID].SkinMaterial;
        player.CurrentSkinID = SelectedSkinID;
        skinCheck();

        GameDataManager.Save(player);
    }
    public void BuySkin()
    {
        if (manager.GoldenTiles >= Skins[SelectedSkinID].Price)
        {
            Skin skin = Skins[SelectedSkinID];

            player.UnlockedSkins.Add(skin.ID);
            
            skin.Unlocked = true;
            LockImage.GetComponent<Animator>().SetTrigger("fade");
            skinCheck();
            manager.Remove(Skins[SelectedSkinID].Price);

            GameDataManager.Save(player);
        }

        
    }


}
