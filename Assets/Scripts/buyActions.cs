using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buyActions : MonoBehaviour
{
    // public TextMeshProUGUI moneyText;
    public Image BuyBar; 
    public CanvasGroup canvasGroup;

    public Button button;


    public TextMeshProUGUI costText;

    public TextMeshProUGUI earningsText;
    public TextMeshProUGUI timeText;


    public float earnings = 5000f;
    public float cost = 200f;
    public float Timer;
    public float currentTimer;
    public bool StartTimer = false;

    public int actionsID;

    public Color normalColor = Color.white;
    public Color notEnoughMoneyColor = new Color(1f, 0.6f, 0.6f); // Vermelho claro

    void Start()
    {
        LateStart();
        BuyBar.fillAmount = 0;
        timeText.text = (Mathf.Round(Timer * 10f) / 10f).ToString() + "s";
        costText.text = "Buy $ " + GameManager.formatCash(cost);
        earningsText.text = GameManager.formatCash(earnings);

    }

    public void LateStart(){        
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.ActionInfo actionInfo = GameManager.GetActionInfo(actionsID);

        if (actionInfo != null){
            Debug.Log("ACTION INFO É DIFERENTE DE NULL");
            StartTimer = actionInfo.bought;
            if (StartTimer == true){
                Image buttonImage = button.GetComponent<Image>();
                Color buttonColor = buttonImage.color;
                buttonColor.a = 0.2f;
                buttonImage.color = buttonColor;
                button.interactable = false;
            } else{
                Debug.Log("É FALSEEEEEEEEEEEEEEEEEEEEE");
                Image buttonImage = button.GetComponent<Image>();
                Color buttonColor = buttonImage.color;
                buttonColor.a = 1.0f;
                buttonImage.color = buttonColor;
                canvasGroup.alpha = 0.2f;
            }
        } else {
            Debug.Log("ACTION INFO É NULL");
            StartTimer = false;
            canvasGroup.alpha = 0.2f;
        }

        gameManager.SaveActionData(actionsID, StartTimer);
    }

    public void buyActionOnClick()
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
        if (StartTimer == false && GameManager.money >= cost){
            canvasGroup.alpha = 1f;
            GameManager.DecrementMoney(cost);
            StartTimer = true;
            currentTimer = Timer;
        }
        else {
            Debug.Log("Not enough money");
        }

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SaveActionData(actionsID, StartTimer);

    }
    void Update()
    {
        if (StartTimer == true)
        {
            currentTimer -= Time.deltaTime;


            // Calcular a porcentagem completada do timer
            float fillPercentage = 1f - (currentTimer / Timer);
            BuyBar.fillAmount = fillPercentage;

            if (currentTimer <= 0)
            {
                // Quando o tempo acabar
                // StartTimer = false;
                currentTimer = 0;

                // Executar as ações de incrementar o dinheiro
                GameManager.IncrementMoney(5000);
                earningsText.text = GameManager.formatCash(earnings);

                // Reiniciar o timer
                StartTimer = true;
                currentTimer = Timer;
            }
        } else{
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
    }
}