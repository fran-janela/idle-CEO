using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadMainUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject updateAll;
    public GameObject button;
    public GameObject panel;

    public buyUpgradeLaptop buyUpgradeLaptopScript;

    public buyUpgradeTable buyUpgradeTableScript;

    void Start()
    {
        updateAll.SetActive(false);
        button.SetActive(false);
        panel.SetActive(false);
        buyUpgradeLaptopScript.LateStart();
        buyUpgradeTableScript.LateStart();
    }

    public void openUpdateAll(){
        updateAll.SetActive(true);
        panel.SetActive(true);
        button.SetActive(true);
        GameManager.menuOpen = true;
    }

    public void closeUpdateAll(){
        updateAll.SetActive(false);
        panel.SetActive(false);
        button.SetActive(false);
        GameManager.menuOpen = false;
    }

 
}
