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

    public float cost = 200f;

    public TextMeshProUGUI costText;

    public TextMeshProUGUI earningsText;

    public TextMeshProUGUI timeText;

    public float Timer;

    public float currentTimer;

    public bool StartTimer = false;



    void Start()
    {
        canvasGroup.alpha = 0.2f;
        BuyBar.fillAmount = 0;
        // moneyText.text = GameManager.money.ToString();

        Image buttonImage = button.GetComponent<Image>();
        Color buttonColor = buttonImage.color;
        buttonColor.a = 1.0f;
        buttonImage.color = buttonColor;
        timeText.text = Timer.ToString() + "s";
        costText.text = "Buy $ " + cost.ToString();
        earningsText.text = GameManager.money.ToString();
    }

    public void buyActionOnClick()
    {
        if (StartTimer == false && GameManager.money >= cost){
            canvasGroup.alpha = 1f;
            GameManager.DecrementMoney(cost);
            StartTimer = true;
            currentTimer = Timer;
        }
        else {
            Debug.Log("Not enough money");
        }

    }
    void Update()
    {
        if (StartTimer == true)
        {
            currentTimer -= Time.deltaTime;

            // Atualizar a exibição do tempo na UI
            timeText.text = Timer.ToString("0.0") + "s";

            // Calcular a porcentagem completada do timer
            float fillPercentage = 1f - (currentTimer / Timer);
            BuyBar.fillAmount = fillPercentage;

            if (currentTimer <= 0)
            {
                // Quando o tempo acabar
                StartTimer = false;
                currentTimer = 0;

                // Executar as ações de incrementar o dinheiro
                GameManager.IncrementMoney(5000);
                earningsText.text = GameManager.money.ToString();

                // Reiniciar o timer
                StartTimer = true;
                currentTimer = Timer;
            }
        }
    }
}



    // void Update()
    // {
    //     if (StartTimer == true){
    //         currentTimer += Time.deltaTime;
    //         BuyBar.fillAmount += 1f/(Timer * Time.deltaTime);
    //         if (currentTimer == Timer){
    //             BuyBar.fillAmount = 1f;
    //             StartTimer = false;
    //             currentTimer = 0;
    //             GameManager.IncrementMoney(5000f);
    //             earningsText.text = GameManager.money.ToString();
    //         } if (currentTimer > Timer){
    //             BuyBar.fillAmount = 0f;
    //             StartTimer = false;
    //             currentTimer = 0;
    //         }
    //     }
        
    // }

