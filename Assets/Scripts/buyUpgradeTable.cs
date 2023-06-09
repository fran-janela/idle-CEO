using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

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
    public TextMeshProUGUI costText;

    public List<Sprite> sprites = new List<Sprite>();

    public int total_level = 5;

    public float earnings = 5f;
    public float earningsBase = 5f;
    public float multiplier;
    public int level = 0;
    public float growthRate = 0.1f;
    public float balancing_production = 1f;


    
    public float decreaseTime = 0.07f;

    public float delayTime = 7f;

    public GameObject computer;
    public GameObject chair;
    public GameObject laptopCode;

    public float cost = 30f;
    public float baseCost = 30f;
    public float balancing_cost = 1f;
    public int tableID; // ID do table atual
    public ClickDeskScript clickDeskScript;

    public bool maxLevel = false;

    int upgrade = 0;

    public Color normalColor = Color.white;
    public Color notEnoughMoneyColor = new Color(1f, 0.6f, 0.6f); // Vermelho claro


    void Start()
    {

        // cor do botão começa toda em 1
        Image buttonImage = button.GetComponent<Image>();
        Color buttonColor = buttonImage.color;
        buttonColor.a = 1.0f;
        buttonImage.color = buttonColor;

        // Atualizando os textos
        costText.text = "Buy $ " + GameManager.formatCash(cost);
        timeText.text = (Math.Round(delayTime, 2)).ToString() + "s";
        levelText.text = level.ToString();

        room_id = ExtractNumberFromString(transform.parent.parent.parent.parent.parent.parent.name);
        tableID = ExtractNumberFromString(transform.parent.parent.parent.parent.name);
    }

    public void LateStart()
    {
        Debug.Log("Table ID: " + tableID);


        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.TableInfo tableInfo = GameManager.GetTableInfo(tableID);
        GameManager.TableParameters tableParameters = GameManager.GetTableParameters(tableID);
        if (tableParameters != null)
        {
            // Debug.Log("Table Parameters não é null");
            earningsBase = tableParameters.earningsBase;
            multiplier = tableParameters.multiplier;
            level = tableParameters.level;
            growthRate = tableParameters.growthRate;
            balancing_production = tableParameters.balancing_production;
            decreaseTime = tableParameters.decreaseTime;
            baseCost = tableParameters.baseCost;
            balancing_cost = tableParameters.balancing_cost;
            upgrade = tableParameters.upgrade;
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost, room_id, upgrade, tableID);
            costText.text = "Buy $ " + GameManager.formatCash(cost);
            if (level == total_level){
                BuyBar.fillAmount = 1f;
                canvasGroup.alpha = 0.2f;
                maxLevel = true;
            }
            else {
                // Debug.Log("Level não é igual ao total level");
                // Debug.Log("BuyBar.fillAmount TABLEEEEEEEE: " + tableParameters.buyBar);
                BuyBar.fillAmount = tableParameters.buyBar;
            }
        } else {
            canvasGroup.alpha = 0.2f;
            BuyBar.fillAmount = 0f;
            levelText.text = "0";
            multiplier = 0f;
            upgrade = 0;
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost, room_id, upgrade,tableID);
        }
        if (tableInfo != null)
        {
            // Debug.Log("Table Info não é null");
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
                cost  = baseCost*tableID*Mathf.Pow(100f, room_id-1);
            costText.text = "Buy $ " + GameManager.formatCash(cost);
        }
        else {
            desk.GetComponent<SpriteRenderer>().sprite = sprites[level-1];
        }


        gameManager.SaveTableData(tableID, earnings, delayTime, room_id);
        float fillAmount = BuyBar.fillAmount;
        gameManager.SaveTableParameters(tableID, earningsBase, growthRate, balancing_production, decreaseTime, baseCost, balancing_cost, multiplier, level, fillAmount, upgrade);
    }

    void Update()
    {
        if (GameManager.money >= cost)
        {
            // Define a cor normal quando o jogador tem dinheiro suficiente
            button.image.color = normalColor;
        }
        else
        {
            // Define a cor quando o jogador não tem dinheiro suficiente
            button.image.color = notEnoughMoneyColor;
        }
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
            // Debug.Log("COMPRA primeiro Nível");
            level += 1;
            upgrade += 1;
            levelText.text = level.ToString();
            canvasGroup.alpha = 1f;
            BuyBar.fillAmount = 0;
            GameManager.DecrementMoney(cost);
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost, room_id, upgrade, tableID);
            costText.text = "Buy $ " + GameManager.formatCash(cost);
        }
        else if (GameManager.money >= cost && !maxLevel){
            delayTime -= decreaseTime;
            if (BuyBar.fillAmount >= 1f)
            {
                delayTime = 7-(level*1.25f);
                level += 1;
                BuyBar.fillAmount = 0;
                levelText.text = level.ToString();
                desk.GetComponent<SpriteRenderer>().sprite = sprites[level-1];
            }
            canvasGroup.alpha = 1f;
            BuyBar.fillAmount += 1.0f/(float)10;
            upgrade += 1;
            GameManager.DecrementMoney(cost);
            // Debug.Log("Money porque compreiiii: " + GameManager.money);

            // Atualizando os valores do increase
            multiplier += 0.7f;
            growthRate += 0.8f;
            balancing_production += 5f;
            earnings = GameManager.CalculateProduction(multiplier, level, growthRate, balancing_production, room_id, tableID, earningsBase);
            // GameManager.IncrementMoney(earnings);

            //Atualizando os valores do custo
            // Debug.Log("Olha o base cost: " + baseCost + " e o growth rate: " + growthRate + " e o level: " + level + " e o balancing: " + balancing_cost);
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost, room_id, upgrade, tableID);

        } else {
            // Debug.Log("Not enough money");
        }
        if (level == total_level)
        {
            // cor do botão fica mais escura
            BuyBar.fillAmount = 1f;
            canvasGroup.alpha = 0.2f;
            maxLevel = true;
        }

        costText.text = "Buy $ " + GameManager.formatCash(cost);
        timeText.text = (Math.Round(delayTime, 2)).ToString() + "s";

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SaveTableData(tableID, earnings, delayTime, room_id);
        float fillAmount = BuyBar.fillAmount;
        // Debug.Log("OLHA O FILLLLLL aMOUNT TABLE: " + fillAmount);
        gameManager.SaveTableParameters(tableID, earningsBase, growthRate, balancing_production, decreaseTime, baseCost, balancing_cost, multiplier, level, fillAmount, upgrade);
    }

    static int ExtractNumberFromString(string input)
    {
        string pattern = @"\d+"; // Matches one or more digits
        Match match = Regex.Match(input, pattern);

        if (match.Success)
        {
            int number;
            bool success = int.TryParse(match.Value, out number);

            if (success)
                return number;
        }

        // Return a default value or throw an exception if no number found
        return -1;
    }
}
