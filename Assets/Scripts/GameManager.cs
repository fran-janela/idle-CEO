using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float money;

    // Start is called before the first frame update
    void Start()
    {
        money = 0.0f;
    }

    public static void IncrementMoney(float amount)
    {
        GameManager.money += amount;
    }
}
