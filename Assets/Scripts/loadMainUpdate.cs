using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMainUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject updateAll;
    public GameObject button;

    public buyUpgradeLaptop buyUpgradeLaptopScript;

    public buyUpgradeTable buyUpgradeTableScript;

    void Start()
    {
        updateAll.SetActive(false);
        button.SetActive(false);
        buyUpgradeLaptopScript.LateStart();
        buyUpgradeTableScript.LateStart();
    }

    public void openUpdateAll(){
        updateAll.SetActive(true);
        button.SetActive(true);
    }

    public void closeUpdateAll(){
        updateAll.SetActive(false);
        button.SetActive(false);
    }

 
}
