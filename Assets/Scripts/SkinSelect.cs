using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject ActivatedSkinImage;
    public Button Next;
    public Button Activate;
    public Button Previous;
    [Space]
    private Mesh currentPlayerMesh;

    public int SelectedSkinID;
    public bool SkinSelectionActive;
    public Skin[] Skins;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
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
            
            ShowLight.SetActive(true);
            LockImage.SetActive(false);
            
            if (SelectedSkinID == player.CurrentSkinID)
            {
                Activate.interactable = false;
                ActivatedSkinImage.SetActive(true);
            }else
            {
                Activate.interactable = true;
                ActivatedSkinImage.SetActive(false);
            }
        }else
        {
            ActivatedSkinImage.SetActive(false);
            LockImage.SetActive(true);
            ShowLight.SetActive(false);
            Activate.interactable = false;
        }
    }
    public void SkinSelection(bool enable)
    {
        SkinSelectCam.SetActive(enable);
        SkinSelectUI.SetActive(enable);
        MainMenu.SetActive(!enable);
        Points_UI.SetActive(!enable);
        SkinSelectionActive = enable;
        
        SelectedSkinID = 0;
        
        skinCheck();
        player.GetComponentInChildren<Animator>().SetBool("showcasing", enable);

        if (enable)
        {
            currentPlayerMesh = player.GetComponentInChildren<MeshFilter>().mesh;
            player.GetComponentInChildren<MeshFilter>().mesh = Skins[SelectedSkinID].SkinMesh;
        }
        else
        {
            player.GetComponentInChildren<MeshFilter>().mesh = currentPlayerMesh;
        }
    }

    public void NextSkin()
    {
        player.gameObject.GetComponentInChildren<MeshFilter>().mesh = Skins[SelectedSkinID + 1].SkinMesh;
        SelectedSkinID++;
        skinCheck();
    }
    public void PreviousSkin()
    {
        player.gameObject.GetComponentInChildren<MeshFilter>().mesh = Skins[SelectedSkinID - 1].SkinMesh;
        SelectedSkinID--;
        skinCheck();
    }

    public void ActivateSkin()
    {
        currentPlayerMesh = Skins[SelectedSkinID].SkinMesh;
        player.CurrentSkinID = SelectedSkinID;
        skinCheck();
    }


}
