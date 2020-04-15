using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelect : MonoBehaviour
{
    [Header("Public references")]
    public Player player;
    public GameObject MainMenu;
    [Header("UI")]
    public GameObject SkinSelectCam;
    public GameObject SkinSelectUI;
    public GameObject Points_UI;

    public Button Next;
    public Button Previous;
    [Space]
    

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



}
