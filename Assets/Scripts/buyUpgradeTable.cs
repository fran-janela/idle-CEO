using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buyUpgradeTable : MonoBehaviour
{
    // public TextMeshProUGUI moneyText;

    public TextMeshProUGUI levelText;
    public int room_id;
    public CanvasGroup canvasGroup;
    public Image BuyBar; 
    public Button button;
    public GameObject desk;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI earningsText;
    public TextMeshProUGUI costText;


    public int total_level = 5;

    public float earnings = 5f;
    public float earningsBase = 5f;
    public float multiplier;
    public int level = 0;
    public float growthRate = 0.1f;
    public float balancing_production = 1f;


    
    public float decreaseTime = 0.2f;

    public float delayTime = 4f;

    public GameObject computer;
    public GameObject chair;
    public GameObject laptopCode;

    public float cost = 100f;
    public float baseCost = 100f;
    public float balancing_cost = 1f;
    public int tableID; // ID do table atual
    public ClickDeskScript clickDeskScript;

    public bool maxLevel = false;


    void Start()
    {

        // cor do upgrade começa bem clarinha, a barra de níveis começa zerada e o nível começa em 0
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
        levelText.text = level.ToString(); 

    }

    public void  LateStart()
    {
        tableID = clickDeskScript.laptopTableSetID;
        Debug.Log("Table ID: " + tableID);


        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.TableInfo tableInfo = GameManager.GetTableInfo(tableID);
        GameManager.TableParameters tableParameters = GameManager.GetTableParameters(tableID);
        if (tableParameters != null)
        {
            Debug.Log("Table Parameters não é null");
            earningsBase = tableParameters.earningsBase;
            multiplier = tableParameters.multiplier;
            level = tableParameters.level;
            growthRate = tableParameters.growthRate;
            balancing_production = tableParameters.balancing_production;
            decreaseTime = tableParameters.decreaseTime;
            baseCost = tableParameters.baseCost;
            balancing_cost = tableParameters.balancing_cost;
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost);
            costText.text = "Buy $ " + Mathf.Round(cost*100f/100f).ToString();
            if (level == total_level){
                BuyBar.fillAmount = 1f;
                canvasGroup.alpha = 0.2f;
                maxLevel = true;
            }
            else {
                Debug.Log("Level não é igual ao total level");
                Debug.Log("BuyBar.fillAmount TABLEEEEEEEE: " + tableParameters.buyBar);
                BuyBar.fillAmount = tableParameters.buyBar;
            }
        } else {
            canvasGroup.alpha = 0.2f;
            BuyBar.fillAmount = 0f;
            levelText.text = "0";
            multiplier = 0f;
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost);
        }
        if (tableInfo != null)
        {
            Debug.Log("Table Info não é null");
            earnings = tableInfo.earnings;
            delayTime = tableInfo.delayTime;
        }
        if (level == 0)
        {
            computer.SetActive(false);
            chair.SetActive(false);
            laptopCode.SetActive(false);
            Color tmp = desk.GetComponent<SpriteRenderer>().color;
            tmp.a = 0.5f;
            desk.GetComponent<SpriteRenderer>().color = tmp;
            if (tableID == 1)
                cost = 0;
            else
                cost = baseCost;
        }


        gameManager.SaveTableData(tableID, earnings, delayTime, room_id);
        float fillAmount = BuyBar.fillAmount;
        gameManager.SaveTableParameters(tableID, earningsBase, growthRate, balancing_production, decreaseTime, baseCost, balancing_cost, multiplier, level, fillAmount);
    }


    public void buy()
    {
        if (level == 0 && GameManager.money >= cost)
        {
            computer.SetActive(true);
            chair.SetActive(true);
            laptopCode.SetActive(true);
            Color tmp = desk.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            desk.GetComponent<SpriteRenderer>().color = tmp;
            Color tmp2 = computer.GetComponent<SpriteRenderer>().color;
            tmp2.a = 0.5f;
            computer.GetComponent<SpriteRenderer>().color = tmp2;
            Debug.Log("COMPRA primeiro Nível");
            level += 1;
            levelText.text = level.ToString();
            canvasGroup.alpha = 1f;
            BuyBar.fillAmount = 0;
            GameManager.DecrementMoney(cost);
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost);
        }
        else if (GameManager.money >= cost && !maxLevel){
            if (BuyBar.fillAmount >= 1f)
            {
                delayTime -= decreaseTime;
                BuyBar.fillAmount = 0;
                level += 1;
                levelText.text = level.ToString();
            }
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
            earningsText.text = Mathf.Round(earnings*100f/100f).ToString();

            //Atualizando os valores do custo
            Debug.Log("Olha o base cost: " + baseCost + " e o growth rate: " + growthRate + " e o level: " + level + " e o balancing: " + balancing_cost);
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost);

        } else {
            Debug.Log("Not enough money");
        }
        if (level == total_level)
        {
            // cor do botão fica mais escura
            BuyBar.fillAmount = 1f;
            canvasGroup.alpha = 0.2f;
            maxLevel = true;
        }

        costText.text = "Buy $ " + Mathf.Round(cost*100f/100f).ToString();
        timeText.text = delayTime.ToString() + "s";
        earningsText.text = Mathf.Round(earnings*100f/100f).ToString();

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SaveTableData(tableID, earnings, delayTime, room_id);
        float fillAmount = BuyBar.fillAmount;
        Debug.Log("OLHA O FILLLLLL aMOUNT TABLE: " + fillAmount);
        gameManager.SaveTableParameters(tableID, earningsBase, growthRate, balancing_production, decreaseTime, baseCost, balancing_cost, multiplier, level, fillAmount);
    }


    void Update()
    {

        
    }
}
