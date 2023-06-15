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
        popUpExpansion.SetActive(false);
        popUpExpansion.SetActive(false);
        popUpActions.SetActive(false);
    }


    public void loadpopUpExpansion(){
        if (!open){
            open = true;
            popUpExpansion.SetActive(true);
        } else {
            open = false;
            popUpExpansion.SetActive(false);
        }
    }

    public void loadPopUpActions(){
        if (!open){
            open = true;
            popUpActions.SetActive(true);
        } else {
            open = false;
            popUpActions.SetActive(false);
        }
    }

    public void loadPopUpManagers(){
        if (!open){
            open = true;
            popUpManagers.SetActive(true);
        } else {
            open = false;
            popUpManagers.SetActive(false);
        }
    }

}
