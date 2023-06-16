using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPopUps : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popUpExpansion;
    public GameObject popUpActions;

    public GameObject popUpManagers;
    private bool open = false;


    void Start()
    {
        popUpManagers.SetActive(false);
        popUpExpansion.SetActive(false);
        popUpActions.SetActive(false);
    }


    public void loadpopUpExpansion(){
        if (!open && GameManager.menuOpen == false){
            open = true;
            GameManager.menuOpen = true;
            popUpExpansion.SetActive(true);
        } else {
            open = false;
            GameManager.menuOpen = false;
            popUpExpansion.SetActive(false);
        }
    }

    public void loadPopUpActions(){
        if (!open && GameManager.menuOpen == false){
            open = true;
            GameManager.menuOpen = true;
            popUpActions.SetActive(true);
        } else {
            open = false;
            GameManager.menuOpen = false;
            popUpActions.SetActive(false);
        }
    }

    public void loadPopUpManagers(){
        if (!open && GameManager.menuOpen == false){
            open = true;
            GameManager.menuOpen = true;
            popUpManagers.SetActive(true);
        } else {
            open = false;
            GameManager.menuOpen = false;
            popUpManagers.SetActive(false);
        }
    }

}
