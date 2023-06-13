using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public class LaptopInfo
    {
        public float earnings;
        public float delayTime;
    }

    public class LaptopParameters
    {
        public float earningsBase;
        public float growthRate;
        public float balancing_production;
        public float decreaseTime;
        public float baseCost;
        public float balancing_cost;

        public float multiplier;
        public int level;

        public float buyBar;
    }

    public class TableParameters
    {
        public float earningsBase;
        public float growthRate;
        public float balancing_production;
        public float decreaseTime;
        public float baseCost;
        public float balancing_cost;

        public float multiplier;

        public int level;

        public float buyBar;
    }

    public class TableInfo
    {
        public float earnings;
        public float delayTime;
    }

    public static float money;

    public static float multiplier;

    public static Dictionary<int, LaptopInfo > laptopDictionary = new Dictionary<int, LaptopInfo>();
    public static Dictionary<int, LaptopParameters > laptopParametersDictionary = new Dictionary<int, LaptopParameters>();
    public static Dictionary<int, TableParameters > tableParametersDictionary = new Dictionary<int, TableParameters>();
    public static Dictionary<int, TableInfo > tableDictionary = new Dictionary<int, TableInfo>();

    void Start()
    {
        money = 0.0f;
        multiplier = 0.0f;
        LoadGameData();
    }

    public static void IncrementMoney(float amount)
    {
        GameManager.money += amount;
        GameManager.money = Mathf.Round(GameManager.money * 100f) / 100f;
        SaveGameData();
        
    }

    public static void DecrementMoney(float amount)
    {
        if (GameManager.money >= amount)
        {
            GameManager.money -= amount;
            GameManager.money = Mathf.Round(GameManager.money * 100f) / 100f;
            SaveGameData();
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


    // Funções para salvar e carregar os dados do jogo
    public static void SaveGameData()
    {
        PlayerPrefs.SetFloat("Money", money);

        string laptopData = JsonUtility.ToJson(laptopDictionary);
        string tableData = JsonUtility.ToJson(tableDictionary);
        string laptopParametersData = JsonUtility.ToJson(laptopParametersDictionary);
        string tableParametersData = JsonUtility.ToJson(tableParametersDictionary);

        PlayerPrefs.SetString("LaptopData", laptopData);
        PlayerPrefs.SetString("TableData", tableData);
        PlayerPrefs.SetString("LaptopParametersData", laptopParametersData);
        PlayerPrefs.SetString("TableParametersData", tableParametersData);
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

    
    public static TableInfo GetTableInfo(int tableID)
    {
        // Retorna as informações do laptop com o ID especificado
        if (tableDictionary.ContainsKey(tableID))
        {
            return tableDictionary[tableID];
        }
        else
        {
            return null;
        }
    }

    public static LaptopParameters GetLaptopParameters(int laptopID)
    {
        // Retorna as informações do laptop com o ID especificado
        if (laptopParametersDictionary.ContainsKey(laptopID))
        {
            return laptopParametersDictionary[laptopID];
        }
        else
        {
            return null;
        }
    }

    
    public static TableParameters GetTableParameters(int tableID)
    {
        // Retorna as informações do laptop com o ID especificado
        if (tableParametersDictionary.ContainsKey(tableID))
        {
            return tableParametersDictionary[tableID];
        }
        else
        {
            return null;
        }
    }

    public static void LoadGameData()
    {

        money = PlayerPrefs.GetFloat("Money", 0.0f);

        //

        // Carregar os dados do dicionário dos laptops
        string laptopData = PlayerPrefs.GetString("LaptopData", "");
        string tableData = PlayerPrefs.GetString("TableData", "");
        string laptopParametersData = PlayerPrefs.GetString("LaptopParametersData", "");
        string tableParametersData = PlayerPrefs.GetString("TableParametersData", "");

        if (!string.IsNullOrEmpty(laptopData))
        {
            laptopDictionary = JsonUtility.FromJson<Dictionary<int, LaptopInfo>>(laptopData);
            Debug.Log("Laptop Dictionary: " + laptopDictionary);
        }
        else {
            laptopDictionary = new Dictionary<int, LaptopInfo>();
        }
        if (!string.IsNullOrEmpty(tableData))
        {
            tableDictionary = JsonUtility.FromJson<Dictionary<int, TableInfo>>(tableData);
            Debug.Log("Table Dictionary: " + tableDictionary);
        }
        else {
            tableDictionary = new Dictionary<int, TableInfo>();
        }
        if (!string.IsNullOrEmpty(laptopParametersData))
        {
            laptopParametersDictionary = JsonUtility.FromJson<Dictionary<int, LaptopParameters>>(laptopParametersData);
            Debug.Log("Laptop Parameters Dictionary: " + laptopParametersDictionary);
        }
        else
        {
            laptopParametersDictionary = new Dictionary<int, LaptopParameters>();
        }
        if (!string.IsNullOrEmpty(tableParametersData))
        {
            tableParametersDictionary = JsonUtility.FromJson<Dictionary<int, TableParameters>>(tableParametersData);
            Debug.Log("Table Parameters Dictionary: " + tableParametersDictionary);
        }
        else
        {
            tableParametersDictionary = new Dictionary<int, TableParameters>();
        }
    }

    public void SaveLaptopData(int laptopID, float earnings, float delayTime)
    {
        // Salvar os dados do laptop no dicionário
        LaptopInfo laptopInfo = new LaptopInfo();
        laptopInfo.earnings = earnings;
        laptopInfo.delayTime = delayTime;

        laptopDictionary[laptopID] = laptopInfo;
        Debug.Log("Laptop ID: " + laptopID + " | Earnings: " + earnings + " | Decrease Time: " + delayTime + " | Level: " );

        // Salvar os dados do jogo após cada atualização no dicionário dos laptops (opcional)
        SaveGameData();
    }

    public void SaveTableData(int tableID, float earnings, float delayTime)
    {
        // Salvar os dados do laptop no dicionário
        TableInfo tableInfo = new TableInfo();
        tableInfo.earnings = earnings;
        tableInfo.delayTime = delayTime;
        Debug.Log("Table ID: " + tableID + " | Earnings: " + earnings + " | Decrease Time: " + delayTime + " | Level: " );

        tableDictionary[tableID] = tableInfo;
        Debug.Log("Laptop ID: " + tableID + " | Earnings: " + earnings + " | Decrease Time: " + delayTime + " | Level: " );

        // Salvar os dados do jogo após cada atualização no dicionário dos laptops (opcional)
        SaveGameData();
    }

    
    public void SaveTableParameters(int tableID, float earningsBase, float growthRate, float balancing_production, float decreaseTime, float baseCost, float balancing_cost, float multiplier, int level, float buyBar)
    {
        // Salvar os dados do laptop no dicionário
        TableParameters tableParameters = new TableParameters();
        tableParameters.earningsBase = earningsBase;
        tableParameters.growthRate = growthRate;
        tableParameters.balancing_production = balancing_production;
        tableParameters.decreaseTime = decreaseTime;
        tableParameters.baseCost = baseCost;
        tableParameters.balancing_cost = balancing_cost;
        tableParameters.multiplier = multiplier;
        tableParameters.level = level;
        tableParameters.buyBar = buyBar;


        tableParametersDictionary[tableID] = tableParameters;

        Debug.Log("Table ID: " + tableID + " | Earnings: " + earningsBase + " | Decrease Time: " + decreaseTime + " | Level: " + level);

        // Salvar os dados do jogo após cada atualização no dicionário dos laptops (opcional)
        SaveGameData();
    }

    public void SaveLaptopParameters (int laptopID, float earningsBase, float growthRate, float balancing_production, float decreaseTime, float baseCost, float balancing_cost, float multiplier, int level, float buyBar)
    {
        // Salvar os dados do laptop no dicionário
        LaptopParameters laptopParameters = new LaptopParameters();
        laptopParameters.earningsBase = earningsBase;
        laptopParameters.growthRate = growthRate;
        laptopParameters.balancing_production = balancing_production;
        laptopParameters.decreaseTime = decreaseTime;
        laptopParameters.baseCost = baseCost;
        laptopParameters.balancing_cost = balancing_cost;
        laptopParameters.multiplier = multiplier;
        laptopParameters.level = level;
        laptopParameters.buyBar = buyBar;

        laptopParametersDictionary[laptopID] = laptopParameters;

        Debug.Log("Laptop ID: " + laptopID + " | Earnings: " + earningsBase + " | Decrease Time: " + decreaseTime + " | Level: " + level);

        // Salvar os dados do jogo após cada atualização no dicionário dos laptops (opcional)
        SaveGameData();
    }

    public void apertaTeclaApagar()
    // se apertar a tecla q
    {  if (Input.GetKeyDown(KeyCode.Q))
        PlayerPrefs.DeleteAll();
    }


}


    // multiplicacao*100*ln((upgrade+1)*taxa de crescimento) + balanceamento - producao por ciclo  | PRODUCAO
    // multiplicacao = 0
    // upgrade = 1
    // taxa de crescimento = 1.1
    // balanceamento = 5

    // custoBase*(taxa de crescimento*n_upgrade*taxa de crescimento*balanceamento) + 1  - custo por ciclo | CUSTO

    // Start is called before the first frame update