using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPopUps : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popUpUpdates;
    public GameObject popUpActions;

    public GameObject updateLaptop;

    public GameObject updateTable;
    private bool open = false;


    void Start()
    {
        popUpUpdates.SetActive(false);
        popUpActions.SetActive(false);
    }


    public void loadPopUpUpdates(){
        if (!open){
            open = true;
            popUpUpdates.SetActive(true);
        } else {
            open = false;
            popUpUpdates.SetActive(false);
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

}
