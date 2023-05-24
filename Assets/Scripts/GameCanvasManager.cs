using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvasManager : MonoBehaviour
{
    // Reference to a text object
    public TMPro.TextMeshProUGUI moneyUI;

    public void Update()
    {
        moneyUI.text = GameManager.money.ToString();
    }
}
