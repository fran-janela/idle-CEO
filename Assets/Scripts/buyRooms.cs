using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buyRooms : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject room1;
    public CanvasGroup canvasGroupPopUp;
    public Button button;
    public Collider2D roomCollider;

    public float cost_room = 100000f;

    private bool bought = false;

    void Start()
    {
        canvasGroupPopUp.alpha = 0.2f;

        Image buttonImage = button.GetComponent<Image>();
        Color buttonColor = buttonImage.color;
        buttonColor.a = 1.0f;
        buttonImage.color = buttonColor;

        roomCollider.enabled = false;

        //mudar o alfa da cor do room1
        room1.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0.64f);
        
    }

    // Update is called once per frame

    public void buyRoom()
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
            room1.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
            roomCollider.enabled = true;
        }
    }
    void Update()
    {
        
    }
}
