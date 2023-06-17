using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPopUps : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popUpExpansion;
    public GameObject popUpActions;
    public GameObject popUpManagers;

    void Start()
    {
        popUpManagers.SetActive(false);
        popUpExpansion.SetActive(false);
        popUpActions.SetActive(false);
    }

    public void openpopUpExpansion(){
        if (GameManager.menuOpen == false){
            popUpExpansion.SetActive(true);
            GameManager.menuOpen = true;
        } 
    }

    public void closepopUpExpansion(){
        popUpExpansion.SetActive(false);
        GameManager.menuOpen = false;
    }


    public void openpopUpActions(){
        if (GameManager.menuOpen == false){
            popUpActions.SetActive(true);
            GameManager.menuOpen = true;
        } 
        
    }

    public void closepopUpActions(){
        popUpActions.SetActive(false);
        GameManager.menuOpen = false;
    }

    public void openpopUpManagers(){
        if (GameManager.menuOpen == false){
            popUpManagers.SetActive(true);
            GameManager.menuOpen = true;
        }
    }

    public void closepopUpManagers(){
        popUpManagers.SetActive(false);
        GameManager.menuOpen = false;
    }

}
