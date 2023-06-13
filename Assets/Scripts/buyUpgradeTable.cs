using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buyUpgradeTable : MonoBehaviour
{
    // public TextMeshProUGUI moneyText;

    public TextMeshProUGUI levelText;
    public CanvasGroup canvasGroup;
    public Image BuyBar; 
    public Button button;
    public GameObject desk;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI earningsText;
    public TextMeshProUGUI costText;


    public int total_level = 5;

    public float earnings = 10f;
    public float earningsBase = 10f;
    public float multiplier;
    public int level = 0;
    public float growthRate = 0;
    public float balancing_production = 0;


    
    public float decreaseTime = 0.2f;


    
    public float cost = 100f;
    public float baseCost = 100f;
    public float balancing_cost = 0;


    public static int tableIDCounter = 1; // Variável estática para controlar o ID dos laptops

    public int tableID; // ID do laptop atual


    void Start()
    {
        // define id do laptop atual
        tableID = tableIDCounter;
        tableIDCounter++;
        Debug.Log("Table ID: " + tableID);

        // cor do upgrade começa bem clarinha, a barra de níveis começa zerada e o nível começa em 0
        canvasGroup.alpha = 0.2f;
        BuyBar.fillAmount = 0;
        levelText.text = "0";
        multiplier = 0f;

        // cor do botão começa toda em 1
        Image buttonImage = button.GetComponent<Image>();
        Color buttonColor = buttonImage.color;
        buttonColor.a = 1.0f;
        buttonImage.color = buttonColor;

        // Atualizando os textos
        costText.text = "Buy $ " + cost.ToString();
        ClickDeskScript clickDeskScript =  desk.GetComponent<ClickDeskScript>();
        timeText.text = clickDeskScript.clickDelay.ToString() + "s";
        earningsText.text = earningsBase.ToString();
    }

    public void buy()
    {
        if (BuyBar.fillAmount == 1f){
            ClickDeskScript clickDeskScript =  desk.GetComponent<ClickDeskScript>();
            clickDeskScript.clickDelay -= decreaseTime;
            BuyBar.fillAmount = 0;
            level += 1;
            levelText.text = level.ToString();
            costText.text = "Buy $ " + cost.ToString();
            timeText.text = clickDeskScript.clickDelay.ToString() + "s";
            earningsText.text = earnings.ToString();
        } 
        if (GameManager.money >= cost){
            canvasGroup.alpha = 1f;
            BuyBar.fillAmount += 1/total_level;
            GameManager.DecrementMoney(cost);

            // Atualizando os valores do increase
            multiplier += 2f;
            growthRate += 1.1f;
            balancing_production += 5f;
            earnings += GameManager.CalculateProduction(multiplier, level, growthRate, balancing_production)*earningsBase;
            // GameManager.IncrementMoney(earnings);
            earningsText.text = earnings.ToString();

            //Atualizando os valores do custo
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost);
            costText.text = "Buy $ " + cost.ToString();

            // Atualizando os textos
            ClickDeskScript clickDeskScript =  desk.GetComponent<ClickDeskScript>();
            timeText.text = clickDeskScript.clickDelay.ToString() + "s";
            earningsText.text = earnings.ToString();

        } else {
            Debug.Log("Not enough money");
        }

    }


    void Update()
    {
        
    }
}
