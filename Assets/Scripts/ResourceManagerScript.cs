using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceManagerScript : MonoBehaviour
{
    [SerializeField] int coins = 0;

    [SerializeField] TextMeshProUGUI coinText;

    // Start is called before the first frame update
    void Start()
    {
        coinText.text = "coins: " + coins.ToString();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        coinText.text = "coins: " + coins.ToString();
    }

}
