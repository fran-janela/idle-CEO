using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPopUps : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject popUp;
    private bool open = false;


    void Start()
    {
        popUp.SetActive(false);
    }


    public void loadPopUpUpdates(){
        if (!open){
            open = true;
            popUp.SetActive(true);
        } else {
            open = false;
            popUp.SetActive(false);
        }
        
    }

}
