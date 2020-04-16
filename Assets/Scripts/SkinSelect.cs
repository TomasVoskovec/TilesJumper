using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
            // If the skin is unlocked enable light and disable the "lock" image
            ShowLight.SetActive(true);
            LockImage.SetActive(false);
            
            // If the selected skin is the current active skin show image
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
             // If the selected skin is not unlocked, disable light and show "lock" image
            ActivatedSkinImage.SetActive(false);
            LockImage.SetActive(true);
            ShowLight.SetActive(false);
            Activate.interactable = false;
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
            // Display skin name
            Name.text = Skins[SelectedSkinID].Name;
            player.GetComponentInChildren<MeshFilter>().mesh = Skins[SelectedSkinID].SkinMesh;
        }
        else
        {
            player.GetComponentInChildren<MeshFilter>().mesh = currentPlayerMesh;
        }
    }
    // Show next skin
    public void NextSkin()
    {
        player.gameObject.GetComponentInChildren<MeshFilter>().mesh = Skins[SelectedSkinID + 1].SkinMesh;
        SelectedSkinID++;
        skinCheck();
        Name.text = Skins[SelectedSkinID].Name;
    }
    // Show previous skin
    public void PreviousSkin()
    {
        player.gameObject.GetComponentInChildren<MeshFilter>().mesh = Skins[SelectedSkinID - 1].SkinMesh;
        SelectedSkinID--;
        skinCheck();
        Name.text = Skins[SelectedSkinID].Name;
    }

    //Activate selected skin
    public void ActivateSkin()
    {
        currentPlayerMesh = Skins[SelectedSkinID].SkinMesh;
        player.CurrentSkinID = SelectedSkinID;
        skinCheck();
    }


}
