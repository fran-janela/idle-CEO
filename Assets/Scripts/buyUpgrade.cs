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

    public float increaseEarnings = 0.1f;

    public float decreaseTime = 0.5f;

    public float cost = 200f;

    public float increaseCost = 1.5f;


    public TextMeshProUGUI costText;

    public TextMeshProUGUI timeText;

    public TextMeshProUGUI earningsText;

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

        costText.text = "Buy $ " + cost.ToString();
        ClickDeskScript clickDeskScript =  desk.GetComponent<ClickDeskScript>();
        timeText.text = clickDeskScript.clickDelay.ToString() + "s";
        earningsText.text = GameManager.money.ToString();
    }

    public void buy()
    {
        if (BuyBar.fillAmount == 1f){
            ClickDeskScript clickDeskScript =  desk.GetComponent<ClickDeskScript>();
            clickDeskScript.clickDelay -= decreaseTime;
            cost = cost + increaseCost*cost;
            BuyBar.fillAmount = 0;
            level += 1;
            levelText.text = level.ToString();
            costText.text = "Buy $ " + cost.ToString();
            timeText.text = clickDeskScript.clickDelay.ToString() + "s";
            earningsText.text = GameManager.money.ToString();
        } 
        if (GameManager.money >= cost){
            canvasGroup.alpha = 1f;
            BuyBar.fillAmount += 0.25f;
            GameManager.DecrementMoney(cost);
            GameManager.multiplier += increaseEarnings;
            ClickDeskScript clickDeskScript =  desk.GetComponent<ClickDeskScript>();
            timeText.text = clickDeskScript.clickDelay.ToString() + "s";
            earningsText.text = GameManager.money.ToString();

        } else {
            Debug.Log("Not enough money");
        }

    }


    void Update()
    {
        
    }
}
