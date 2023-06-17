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
    public TextMeshProUGUI costText;
    public Collider2D roomCollider;

    public float cost_room = 100000f;

    public int id_room;

    private bool bought = false;

    void Start()
    {
        costText.text = "Buy $ " + GameManager.formatCash(cost_room);
        LateStart();
    }

    public void LateStart(){
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.ExpandInfo roomInfo = GameManager.GetExpandInfo(id_room);

        if (roomInfo != null){
            bought = roomInfo.owned;
            if (bought == true){
                Image buttonImage = button.GetComponent<Image>();
                Color buttonColor = buttonImage.color;
                buttonColor.a = 0.2f;
                buttonImage.color = buttonColor;
                button.interactable = false;
                room1.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f, 0f);
                roomCollider.enabled = true;
            } else{
                Image buttonImage = button.GetComponent<Image>();
                Color buttonColor = buttonImage.color;
                buttonColor.a = 1.0f;
                buttonImage.color = buttonColor;
                canvasGroupPopUp.alpha = 0.2f;
            }
        } else{
            bought = false;
            canvasGroupPopUp.alpha = 0.2f;
        }

        gameManager.SaveExpandData(id_room, bought);
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

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SaveExpandData(id_room, bought);
    }
    void Update()
    {
        
    }
}