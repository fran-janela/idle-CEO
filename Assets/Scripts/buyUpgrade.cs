using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buyUpgrade : MonoBehaviour
{
    public float money;

    public Image BuyBar; 
    public int multiplier;
    // Start is called before the first frame update
    void Start()
    {
        BuyBar.fillAmount = 0;
        money = 1000;
        multiplier = 1;
        
    }

    public void buy()
    {
        if (money >= 200){
            money -= 200;
            multiplier += 1;
            BuyBar.fillAmount += 0.1f;
        } else {
            Debug.Log("Not enough money");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
