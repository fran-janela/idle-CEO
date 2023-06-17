using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buyManagers : MonoBehaviour
{
    // Start is called before the first frame update
    public Image BuyBar; 
    public CanvasGroup canvasGroup;

    public Button button;

    public TextMeshProUGUI costText;


    public TextMeshProUGUI num_managers_text;

    public int managers_room = 0;

    public int total_managers_room = 0;

    public float cost = 200f;

    public int room_id;

    private bool max_managers = false;

    void Start()
    {
        BuyBar.fillAmount = 0;
        num_managers_text.text = managers_room.ToString() + "/" + total_managers_room.ToString();
        costText.text = "Buy $ " + GameManager.formatCash(cost);
        Debug.Log("MANAGERS ROOM: " + managers_room);
        LateStart();
    }

    public void LateStart(){
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameManager.ManagerInfo managerInfo = GameManager.GetManagerInfo(room_id);

        if (managerInfo != null){
            Debug.Log("MANAGER INFO É DIFERENTE DE NULL");
            managers_room = managerInfo.managers_room;
            BuyBar.fillAmount = (float)managers_room/(float)total_managers_room;
            if (managers_room == total_managers_room){
                max_managers = true;
            } else {
                max_managers = false;
                if (managers_room > 0){
                    canvasGroup.alpha = 1.0f;
                } else {
                    canvasGroup.alpha = 0.2f;
                }
            }
        } else {
            Debug.Log("MANAGER INFO É NULL");
            canvasGroup.alpha = 0.2f;
        }

        gameManager.SaveManagerData(room_id, managers_room);

    }

    public void buyManager(){
        if (GameManager.money >= cost && !max_managers){
            canvasGroup.alpha = 1.0f;
            GameManager.DecrementMoney(cost);
            managers_room += 1;
            num_managers_text.text = managers_room.ToString() + "/" + total_managers_room.ToString();
            BuyBar.fillAmount += 1.0f/(float)total_managers_room; 
        }else if (managers_room == total_managers_room){
            max_managers = true;
            Debug.Log("Não tem mais espaço para gerentes");
        } else {
            Debug.Log("Não tem dinheiro suficiente");
        }

        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.SaveManagerData(room_id, managers_room);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
