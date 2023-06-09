using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class buyUpgradeLaptop : MonoBehaviour
{
    // public TextMeshProUGUI moneyText;

    public TextMeshProUGUI levelText;
    public CanvasGroup canvasGroup;
    public Image BuyBar; 
    public Button button;
    public GameObject laptop;
    public TextMeshProUGUI earningsText;
    public TextMeshProUGUI costText;

    private int room_id;
    public int total_level = 5;

    public float earnings = 10f;
    public float earningsBase = 10f;
    public float multiplier;
    public int level = 0;
    public float growthRate = 0.2f;
    public float balancing_production = 1f;

    public List<Sprite> sprites = new List<Sprite>();
    
    public float decreaseTime = 1f;

    public float delayTime = 8f;

    public GameObject progress;
    public GameObject runButton;
    public GameObject employee;

    public float cost = 50f;
    public float baseCost = 50f;
    public float balancing_cost = 1f;

    public int upgrade = 0;
    public int laptopID; // ID do laptop atual
    public ClickDeskScript clickDeskScript;

    public bool maxLevel = false;
    private bool lateStartExecuted;

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
        earningsText.text = GameManager.formatCash(earnings);     
        levelText.text = level.ToString();

        // Get room_id from parent name
        room_id = ExtractNumberFromString(transform.parent.parent.parent.parent.parent.parent.name);
        laptopID = ExtractNumberFromString(transform.parent.parent.parent.parent.name);
        // Debug.Log("=====================================================\n" + "Room ID: " + room_id.ToString());
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


    public void LateStart()
    {
        // Debug.Log("Laptop ID: " + laptopID);

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.LaptopInfo laptopInfo = GameManager.GetLaptopInfo(laptopID);
        GameManager.LaptopParameters laptopParameters = GameManager.GetLaptopParameters(laptopID);

        // Verifique se os objetos são nulos antes de acessar suas propriedade
        
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
            upgrade = laptopParameters.upgrade;
            // Debug.Log("OLHA O BALANCING COST: " + balancing_cost + "BASE COST: " + baseCost + "GROWTH RATE: " + growthRate + "LEVEL: " + level + "MULTIPLIER: " + multiplier + "BALANCING PRODUCTION: " + balancing_production + "DECREASE TIME: " + decreaseTime);
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost, room_id, upgrade, laptopID);
            // Debug.Log("OLHA O CUSTO AQUIIIIIIIII: " + cost);
            costText.text = "Buy $ " + GameManager.formatCash(cost);
            if (level == total_level){
                BuyBar.fillAmount = 1f;
                canvasGroup.alpha = 0.2f;
                upgrade = 40;
                maxLevel = true;
            } else {
                // Debug.Log("Level não é igual ao total level");
                // Debug.Log("BuyBar.fillAmount: " + laptopParameters.buyBar);
                BuyBar.fillAmount = laptopParameters.buyBar;
            }
        }
        else {
            canvasGroup.alpha = 0.2f;
            BuyBar.fillAmount = 0;
            level = 0;
            levelText.text = "0";
            multiplier = 0f;
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost, room_id, upgrade, laptopID);
        }
        if (laptopInfo != null)
        {
            // Debug.Log("Laptop Info não é null");
            earnings = laptopInfo.earnings;
            delayTime = laptopInfo.delayTime;
        }
        if (level == 0)
        {
            progress.SetActive(false);
            runButton.SetActive(false);
            employee.SetActive(false);
            if (laptopID == 1)
            {
                cost = 0;
                earnings = GameManager.CalculateProduction(multiplier+0.7f, level+1, growthRate+1.1f, balancing_production+5f, room_id, laptopID, earningsBase);
            }
            else
            {
                cost = baseCost*laptopID*Mathf.Pow(100f, room_id-1);
                earnings = GameManager.CalculateProduction(multiplier+0.7f, level+1, growthRate+1.1f, balancing_production+5f, room_id, laptopID, earningsBase);
            }
            costText.text = "Buy $ " + GameManager.formatCash(cost);
            earningsText.text = GameManager.formatCash(earnings);
        }
        else 
        {
            laptop.GetComponent<SpriteRenderer>().sprite = sprites[level-1];
        }

        // Salvar os dados do laptop no dicionário
        gameManager.SaveLaptopData(laptopID, earnings, delayTime, room_id);
        // Debug.Log("Mandando os dados pro LaptopParameters : " + laptopID + " " + earningsBase + " " + growthRate + " " + balancing_production + " " + decreaseTime + " " + baseCost + " " + balancing_cost + " " + multiplier + " " + level + " " + BuyBar.fillAmount);
        float fillAmount = BuyBar.fillAmount;
        gameManager.SaveLaptopParameters(laptopID, earningsBase, growthRate, balancing_production, decreaseTime, baseCost, balancing_cost, multiplier, level, fillAmount, upgrade);


    }



    public void buy()
    {
        if (level == 0 && GameManager.money >= cost)
        {
            progress.SetActive(true);
            runButton.SetActive(true);
            employee.SetActive(true);
            Color tmp = laptop.GetComponent<SpriteRenderer>().color;
            tmp.a = 1f;
            laptop.GetComponent<SpriteRenderer>().color = tmp;
            level += 1;
            upgrade += 1;
            multiplier += 0.7f;
            growthRate += 1.1f;
            balancing_production += 5f;
            levelText.text = level.ToString();
            canvasGroup.alpha = 1f;
            BuyBar.fillAmount = 0;
            GameManager.DecrementMoney(cost);
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost, room_id, upgrade, laptopID);
            costText.text = "Buy $ " + GameManager.formatCash(cost);
            earnings = GameManager.CalculateProduction(multiplier, level, growthRate, balancing_production, room_id, laptopID, earningsBase);
            earningsText.text = GameManager.formatCash(earnings);
        }
        else if (GameManager.money >= cost && !maxLevel){
            if (BuyBar.fillAmount == 1f && level < total_level)
            {
                delayTime -= decreaseTime;
                BuyBar.fillAmount = 0;
                level += 1;
                levelText.text = level.ToString();
                laptop.GetComponent<SpriteRenderer>().sprite = sprites[level-1];
            }
            canvasGroup.alpha = 1f;
            BuyBar.fillAmount += 1.0f/(float)10;
            upgrade += 1;
            GameManager.DecrementMoney(cost);
            // Debug.Log("Money porque compreiiii: " + GameManager.money);

            // Atualizando os valores do increase
            multiplier += 0.7f;
            growthRate += 1.1f;
            balancing_production += 5f;
            
            earnings = GameManager.CalculateProduction(multiplier, level, growthRate, balancing_production, room_id, laptopID, earningsBase);
            earningsText.text = GameManager.formatCash(earnings);
            //Atualizando os valores do custo
            cost = GameManager.CalculateCost(baseCost, growthRate, level, balancing_cost, room_id, upgrade, laptopID);
            // Debug.Log("OLHAAAAAAAAAAAAA O Custo: " + cost);
            costText.text = "Buy $ " + GameManager.formatCash(cost);

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
        earningsText.text = GameManager.formatCash(earnings);

        //carrega o script do gamemanager e salva os dados do laptop no dicionário
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SaveLaptopData(laptopID, earnings, delayTime, room_id);
        float fillAmount = BuyBar.fillAmount;
        // Debug.Log("OLHA O FILLLLLL aMOUNT : " + fillAmount);
        gameManager.SaveLaptopParameters(laptopID, earningsBase, growthRate, balancing_production, decreaseTime, baseCost, balancing_cost, multiplier, level, fillAmount, upgrade);

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
