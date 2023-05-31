using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float money;

    public static float multiplier;
 
    // multiplicacao*100*ln((upgrade+1)*taxa de crescimento) + balanceamento - producao por ciclo  | PRODUCAO
    // multiplicacao = 0
    // upgrade = 1
    // taxa de crescimento = 1.1
    // balanceamento = 5

    // custoBase*(taxa de crescimento*n_upgrade*taxa de crescimento*balanceamento) + 1  - custo por ciclo | CUSTO

    // Start is called before the first frame update
    void Start()
    {
        money = 0.0f;
        multiplier = 0.0f;
    }

    public static void IncrementMoney(float amount)
    {
        GameManager.money += amount*multiplier + amount;
        
    }

    public static void DecrementMoney(float amount)
    {
        if (GameManager.money >= amount)
        {
            GameManager.money -= amount;
        }
    }
}
