using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public class LaptopInfo
    {
        public float earnings;
        public float decreaseTime;
    }

    public static float money;

    public static float multiplier;

    public static Dictionary<int, LaptopInfo > laptopDictionary = new Dictionary<int, LaptopInfo>();
 
    void Start()
    {
        money = 0.0f;
        multiplier = 0.0f;
        // LoadGameData();
    }

    public static void IncrementMoney(float amount)
    {
        GameManager.money += amount;
        GameManager.money = Mathf.Round(GameManager.money * 100f) / 100f;
        // SaveGameData();
        
    }

    public static void SaveGameData()
    {
        // Salvar os dados do jogo, incluindo o dicionário dos laptops, usando PlayerPrefs ou outra forma de armazenamento persistente
        // Aqui está um exemplo usando PlayerPrefs:

        // Salvar o dinheiro do jogador
        PlayerPrefs.SetFloat("Money", money);

        // Salvar os dados do dicionário dos laptops
        string laptopData = JsonUtility.ToJson(laptopDictionary);
        PlayerPrefs.SetString("LaptopData", laptopData);
        // Salvar outros dados do jogo, se necessário
    }

    public static LaptopInfo GetLaptopInfo(int laptopID)
    {
        // Retorna as informações do laptop com o ID especificado
        if (laptopDictionary.ContainsKey(laptopID))
        {
            return laptopDictionary[laptopID];
        }
        else
        {
            return null;
        }
    }

    public static void LoadGameData()
    {
        // Carregar os dados do jogo, incluindo o dicionário dos laptops

        // Carregar o dinheiro do jogador
        money = PlayerPrefs.GetFloat("Money", 0.0f);

        //

        // Carregar os dados do dicionário dos laptops
        string laptopData = PlayerPrefs.GetString("LaptopData", "");
        if (!string.IsNullOrEmpty(laptopData))
        {
            laptopDictionary = JsonUtility.FromJson<Dictionary<int, LaptopInfo>>(laptopData);
        }
        else
        {
            // Se não houver dados salvos, inicializar o dicionário dos laptops vazio
            laptopDictionary = new Dictionary<int, LaptopInfo>();
        }

        // Carregar outros dados do jogo, se necessário
    }

    public void SaveLaptopData(int laptopID, float earnings, float decreaseTime)
    {
        // Salvar os dados do laptop no dicionário
        LaptopInfo laptopInfo = new LaptopInfo();
        laptopInfo.earnings = earnings;
        laptopInfo.decreaseTime = decreaseTime;

        laptopDictionary[laptopID] = laptopInfo;
        Debug.Log("Laptop ID: " + laptopID + " | Earnings: " + earnings + " | Decrease Time: " + decreaseTime);

        // Salvar os dados do jogo após cada atualização no dicionário dos laptops (opcional)
        SaveGameData();
    }

    public void apertaTeclaApagar()
    // se apertar a tecla q
    {  if (Input.GetKeyDown(KeyCode.Q))
        PlayerPrefs.DeleteAll();
        // acho que não deletou o money
        PlayerPrefs.DeleteKey("Money");
        PlayerPrefs.DeleteKey("LaptopData");

    }


    public static void DecrementMoney(float amount)
    {
        if (GameManager.money >= amount)
        {
            GameManager.money -= amount;
            GameManager.money = Mathf.Round(GameManager.money * 100f) / 100f;
        }
    }

    public static float CalculateProduction(float multiplication, float upgrade, float growthRate, float balancing)
    {
        return multiplication * 10f * Mathf.Log((upgrade + 1f) * growthRate) + balancing - upgrade;
    }

    public static float CalculateCost(float baseCost, float growthRate, float upgrade, float balancing)
    {
        if (upgrade == 0f)
        {
            Debug.Log("Custo total: " + baseCost * (growthRate  * balancing) + 1f);
            return baseCost*growthRate*balancing + 1f;
        }
        Debug.Log("Custo total: " + baseCost * (growthRate * upgrade * balancing) + 1f);
        return baseCost * (growthRate * upgrade  * balancing) + 1f;
    }
}


    // multiplicacao*100*ln((upgrade+1)*taxa de crescimento) + balanceamento - producao por ciclo  | PRODUCAO
    // multiplicacao = 0
    // upgrade = 1
    // taxa de crescimento = 1.1
    // balanceamento = 5

    // custoBase*(taxa de crescimento*n_upgrade*taxa de crescimento*balanceamento) + 1  - custo por ciclo | CUSTO

    // Start is called before the first frame update