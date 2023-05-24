using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buyUpgrade : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public TextMeshProUGUI levelText;

    public Image BuyBar; 
    public float multiplier;

    public int level = 0;

    public CanvasGroup canvasGroup;

    public Button button;

    public Money moneyScript;


    void Start()
    {
        moneyScript = GameObject.FindObjectOfType<Money>();
        canvasGroup.alpha = 0.2f;
        BuyBar.fillAmount = 0;
        multiplier = 0f;
        moneyText.text = moneyScript.money.ToString();
        levelText.text = "0";

        Image buttonImage = button.GetComponent<Image>();
        Color buttonColor = buttonImage.color;
        buttonColor.a = 1.0f;
        buttonImage.color = buttonColor;
        
    }

    public void buy()
    {
        if (BuyBar.fillAmount == 1f){
            level += 1;
            BuyBar.fillAmount = 0;
            
            moneyScript.money += 100 * multiplier;
            moneyText.text = moneyScript.money.ToString();
            levelText.text = level.ToString();
        } 
        if (moneyScript.money >= 200){
            canvasGroup.alpha = 1f;
            moneyScript.money -= 200;
            multiplier += 0.25f;
            BuyBar.fillAmount += 0.25f;
            moneyText.text = moneyScript.money.ToString();
            // hasItem = true;
            // mudarTransparencia(1f);
        } else {
            Debug.Log("Not enough money");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
