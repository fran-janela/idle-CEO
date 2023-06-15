using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buyStyle : MonoBehaviour
{
    
    public CanvasGroup canvasGroupPopUp;
    public Button button;

    public float cost_room = 100000f;

    private bool bought = false;

    void Start()
    {
        canvasGroupPopUp.alpha = 0.2f;

        Image buttonImage = button.GetComponent<Image>();
        Color buttonColor = buttonImage.color;
        buttonColor.a = 1.0f;
        buttonImage.color = buttonColor;        
    }

    // Update is called once per frame
    public void buyStyleItem()
    {
        if (GameManager.money >= cost_room && !bought)
        {
            bought = true;
            canvasGroupPopUp.alpha = 1f;
            Image buttonImage = button.GetComponent<Image>();
            Color buttonColor = buttonImage.color;
            buttonColor.a = 0.2f;
            buttonImage.color = buttonColor;
            GameManager.DecrementMoney(cost_room);
        }
    }
}
