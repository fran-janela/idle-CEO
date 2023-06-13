using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMainUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject updateAll;

    public buyUpgradeLaptop buyUpgradeLaptopScript;

    public buyUpgradeTable buyUpgradeTableScript;

    void Start()
    {
        updateAll.SetActive(false);
        buyUpgradeLaptopScript.LateStart();
        buyUpgradeTableScript.LateStart();
    }

    public void openUpdateAll(){
        updateAll.SetActive(true);
    }

    public void closeUpdateAll(){
        updateAll.SetActive(false);
    }

 
}
