using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMainUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject updateLaptop;

    public GameObject updateTable;

    public buyUpgradeLaptop buyUpgradeLaptopScript;

    void Start()
    {
        updateLaptop.SetActive(false);
        updateTable.SetActive(false);
        buyUpgradeLaptopScript.LateStart();
    }

    public void openUpdateLaptop(){
        updateLaptop.SetActive(true);
    }

    public void closeUpdateLaptop(){
        updateLaptop.SetActive(false);
    }

    public void openUpdateTable(){
        updateTable.SetActive(true);
    }

    public void closeUpdateTable(){
        updateTable.SetActive(false);
    }

 
}
