using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buyUpgrade : MonoBehaviour
{
    // public TextMeshProUGUI moneyText;

    public TextMeshProUGUI levelText;

    public Image BuyBar; 
    public float multiplier;

    public int level = 0;

    public CanvasGroup canvasGroup;

    public Button button;

    public GameManager moneyScript;

    public GameObject desk;

    void Start()
    {
        canvasGroup.alpha = 0.2f;
        BuyBar.fillAmount = 0;
        multiplier = 0f;
        // moneyText.text = GameManager.money.ToString();
        levelText.text = "0";

        Image buttonImage = button.GetComponent<Image>();
        Color buttonColor = buttonImage.color;
        buttonColor.a = 1.0f;
        buttonImage.color = buttonColor;
        
    }

    public void buy()
    {
        if (BuyBar.fillAmount == 1f){
            ClickDeskScript clickDeskScript =  desk.GetComponent<ClickDeskScript>();
            clickDeskScript.clickDelay -= 0.5f;
            level += 1;
            BuyBar.fillAmount = 0;
            GameManager.multiplier += 1f;
            levelText.text = level.ToString();
        } 
        if (GameManager.money >= 200){
            canvasGroup.alpha = 1f;
            GameManager.DecrementMoney(200);
            GameManager.multiplier += 0.1f;
            BuyBar.fillAmount += 0.25f;

        } else {
            Debug.Log("Not enough money");
        }

    }


    void Update()
    {
        
    }
}
