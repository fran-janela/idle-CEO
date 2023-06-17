using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buyManagers : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup canvasGroup;

    public Button button;

    public float cost = 200f;

    public TextMeshProUGUI costText;

    public int id_manager;

    public bool bought = false;


    void Start()
    {
        costText.text = "Buy $ " + GameManager.formatCash(cost);
        LateStart();
    }

    public void LateStart(){
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.ManagerInfo managerInfo = GameManager.GetManagerInfo(id_manager);

        if (managerInfo != null){
            bought = managerInfo.bought;
            if (bought == true){
                Image buttonImage = button.GetComponent<Image>();
                Color buttonColor = buttonImage.color;
                buttonColor.a = 0.2f;
                buttonImage.color = buttonColor;
                button.interactable = false;
            } else{
                Image buttonImage = button.GetComponent<Image>();
                Color buttonColor = buttonImage.color;
                buttonColor.a = 1.0f;
                buttonImage.color = buttonColor;
                canvasGroup.alpha = 0.2f;
            }
        } else {
            Debug.Log("MANAGER INFO É NULL");
            bought = false;
            canvasGroup.alpha = 0.2f;
        }

        gameManager.SaveManagerData(id_manager, bought);

    }

    public void buyManager(){
        if (GameManager.money >= cost && !bought){
            bought = true;
            canvasGroup.alpha = 1.0f;
            GameManager.DecrementMoney(cost);
            Image buttonImage = button.GetComponent<Image>();
            Color buttonColor = buttonImage.color;
            buttonColor.a = 0.2f;
            buttonImage.color = buttonColor;
        } 

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SaveManagerData(id_manager, bought);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
