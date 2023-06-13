using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buyUpgradeLaptop : MonoBehaviour
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
    public float growthRate = 0.2f;
    public float balancing_production = 1f;


    
    public float decreaseTime = 0.5f;

    public float delayTime = 5f;


    
    public float cost = 200f;
    public float baseCost = 200f;
    public float balancing_cost = 1f;
    public int laptopID; // ID do laptop atual
    public ClickDeskScript clickDeskScript;

    public bool maxLevel = false;
    private bool lateStartExecuted;


    void Start()
    {

        // canvasGroup.alpha = 0.2f;
        // BuyBar.fillAmount = 0;
        // levelText.text = "0";
        // multiplier = 0f;

        // cor do botão começa toda em 1
        Image buttonImage = button.GetComponent<Image>();
        Color buttonColor = buttonImage.color;
        buttonColor.a = 1.0f;
        buttonImage.color = buttonColor;

        // Atualizando os textos
        costText.text = "Buy $ " + cost.ToString();
        timeText.text = delayTime.ToString() + "s";
        earningsText.text = earningsBase.ToString();     

    }

    // public void  LateStart()
    // {
    //     //carrega o script do gamemanager e salva os dados do laptop no dicionário
    //             // laptopID = desk.GetComponent<ClickDeskScript>().laptopTableSetID;
    //     laptopID = clickDeskScript.laptopTableSetID;
    //     Debug.Log("Laptop ID: " + laptopID);


    //     GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    //     Debug.Log("Game Manager: " + gameManager);
    //     gameManager.SaveLaptopData(laptopID, earnings, delayTime);
    // }

    public void LateStart()
    {
        laptopID = clickDeskScript.laptopTableSetID;
        Debug.Log("Laptop ID: " + laptopID);

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.LaptopInfo laptopInfo = GameManager.GetLaptopInfo(laptopID);
        GameManager.LaptopParameters laptopParameters = GameManager.GetLaptopParameters(laptopID);

        // Verifique se os objetos são nulos antes de acessar suas propriedades
        if (laptopInfo != null)
        {
            earnings = laptopInfo.earnings;
            delayTime = laptopInfo.delayTime;
        }

        if (laptopParameters != null)
        {
            Debug.Log("Laptop Parameters não é null");
            earningsBase = laptopParameters.earningsBase;
            multiplier = laptopParameters.multiplier;
            level = laptopParameters.level;
            growthRate = laptopParameters.growthRate;
            balancing_production = laptopParameters.balancing_production;
            decreaseTime = laptopParameters.decreaseTime;
            baseCost = laptopParameters.baseCost;
            balancing_cost = laptopParameters.balancing_cost;
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost);
            if (level == total_level){
                BuyBar.fillAmount = 1f;
                canvasGroup.alpha = 0.2f;
                maxLevel = true;
            } else {
                Debug.Log("Level não é igual ao total level");
                Debug.Log("BuyBar.fillAmount: " + laptopParameters.buyBar);
                BuyBar.fillAmount = laptopParameters.buyBar;
            }
        }
        if (laptopParameters == null) {
            canvasGroup.alpha = 0.2f;
            BuyBar.fillAmount = 0;
            levelText.text = "0";
            multiplier = 0f;
        }

        // Salvar os dados do laptop no dicionário
        gameManager.SaveLaptopData(laptopID, earnings, delayTime);
        Debug.Log("Mandando os dados pro LaptopParameters : " + laptopID + " " + earningsBase + " " + growthRate + " " + balancing_production + " " + decreaseTime + " " + baseCost + " " + balancing_cost + " " + multiplier + " " + level + " " + BuyBar.fillAmount);
        gameManager.SaveLaptopParameters(laptopID, earningsBase, growthRate, balancing_production, decreaseTime, baseCost, balancing_cost, multiplier, level, BuyBar.fillAmount);


    }



    public void buy()
    {
        if (BuyBar.fillAmount == 1f && level < total_level){
            delayTime -= decreaseTime;
            BuyBar.fillAmount = 0;
            level += 1;
            levelText.text = level.ToString();
        } 
        if (level == total_level){
            // cor do botão fica mais escura
            BuyBar.fillAmount = 1f;
            canvasGroup.alpha = 0.2f;
            maxLevel = true;
        }
        if (GameManager.money >= cost && !maxLevel){
            canvasGroup.alpha = 1f;
            BuyBar.fillAmount += 1.0f/(float)10;
            GameManager.DecrementMoney(cost);
            Debug.Log("Money porque compreiiii: " + GameManager.money);

            // Atualizando os valores do increase
            multiplier += 2f;
            growthRate += 1.1f;
            balancing_production += 5f;
            earnings += GameManager.CalculateProduction(multiplier, level, growthRate, balancing_production)*earningsBase;
            // GameManager.IncrementMoney(earnings);
            earningsText.text = earnings.ToString();

            //Atualizando os valores do custo
            Debug.Log("Olha o base cost: " + baseCost + " e o growth rate: " + growthRate + " e o level: " + level + " e o balancing: " + balancing_cost);
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost);

        } else {
            Debug.Log("Not enough money");
        }

        costText.text = "Buy $ " + cost.ToString();
        timeText.text = delayTime.ToString() + "s";
        earningsText.text = earnings.ToString();

        //carrega o script do gamemanager e salva os dados do laptop no dicionário
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SaveLaptopData(laptopID, earnings, delayTime);
        gameManager.SaveLaptopParameters(laptopID, earningsBase, growthRate, balancing_production, decreaseTime, baseCost, balancing_cost, multiplier, level, BuyBar.fillAmount);

    }


    void Update()
    {

        
    }
}
