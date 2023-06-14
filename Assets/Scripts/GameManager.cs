using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }


    [System.Serializable]
    public class LaptopInfo
    {
        public int id;
        public float earnings;
        public float delayTime;
    }

    [System.Serializable]
    public class LaptopParameters
    {
        public int id;
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

    [System.Serializable]
    public class TableParameters
    {
        public int id;
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


    [System.Serializable]
    public class TableInfo
    {
        public int id;
        public float earnings;
        public float delayTime;
    }

    public static float money;

    public static float multiplier;

    public static LaptopInfo[] dictLaptopInfo = new LaptopInfo[48];

    public static LaptopParameters[] dictLaptopParameters = new LaptopParameters[48];

    public static TableParameters[] dictTableParameters = new TableParameters[48];

    public static TableInfo[] dictTableInfo = new TableInfo[48];

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

        string laptopDataToJSON = JsonHelper.ToJson(dictLaptopInfo, true);
        Debug.Log(laptopDataToJSON);
        string tableDataToJSON = JsonHelper.ToJson(dictTableInfo, true);
        Debug.Log(tableDataToJSON);
        string laptopParametersDataToJSON = JsonHelper.ToJson(dictLaptopParameters, true);
        Debug.Log(laptopParametersDataToJSON);
        string tableParametersDataToJSON = JsonHelper.ToJson(dictTableParameters, true);
        Debug.Log(tableParametersDataToJSON);

        PlayerPrefs.SetString("LaptopData", laptopDataToJSON);
        PlayerPrefs.SetString("TableData", tableDataToJSON);
        PlayerPrefs.SetString("LaptopParametersData", laptopParametersDataToJSON);
        PlayerPrefs.SetString("TableParametersData", tableParametersDataToJSON);

    }

    public static LaptopInfo GetLaptopInfo(int laptopID)
    {
        // Retorna as informações do laptop com o ID especificado
        if (dictLaptopInfo[laptopID-1].id == laptopID)
        {
            return dictLaptopInfo[laptopID-1];
        }
        else
        {
            return null;
        }

    }

    
    public static TableInfo GetTableInfo(int tableID)
    {
        // Retorna as informações do laptop com o ID especificado
        if (dictTableInfo[tableID-1].id == tableID)
        {
            return dictTableInfo[tableID-1];
        }
        else
        {
            return null;
        }
    }

    public static LaptopParameters GetLaptopParameters(int laptopID)
    {
        // Retorna as informações do laptop com o ID especificado
        Debug.Log("LAPTOP ID AQUI NO PARAMTERES: " + laptopID);
        //print o length do dictLaptopParameters
        Debug.Log("dictLaptopParameters.Length: " + dictLaptopParameters.Length);
        Debug.Log("dictLaptopParameters[laptopID-1].id: " + dictLaptopParameters[0].id + " e laptopID: " + laptopID);
        if (dictLaptopParameters[laptopID-1].id == laptopID)
        {
            return dictLaptopParameters[laptopID-1];
        }
        else
        {
            return null;
        }
    }

    public static TableParameters GetTableParameters(int tableID)
    {
        // Retorna as informações do laptop com o ID especificado
        if (dictTableParameters[tableID-1].id == tableID)
        {
            return dictTableParameters[tableID-1];
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

        if (laptopData != "{}")
        {
            dictLaptopInfo = JsonHelper.FromJson<LaptopInfo>(laptopData);
            Debug.Log("Laptop Data: " + dictLaptopInfo[0].id + " | " + dictLaptopInfo[0].earnings + " | " + dictLaptopInfo[0].delayTime);
            Debug.Log("Laptop Data: " + dictLaptopInfo[1].id + " | " + dictLaptopInfo[1].earnings + " | " + dictLaptopInfo[1].delayTime);
        }
        else {
            dictLaptopInfo = new LaptopInfo[48];
            for (int i = 0; i < dictLaptopInfo.Length; i++)
            {
                dictLaptopInfo[i] = new LaptopInfo();
            }
        }
        if (tableData != "{}")
        {
            Debug.Log("Table Data: " + tableData);
            dictTableInfo = JsonHelper.FromJson<TableInfo>(tableData);
            Debug.Log("Table Data: " + dictTableInfo[0].id + " | " + dictTableInfo[0].earnings + " | " + dictTableInfo[0].delayTime);
            Debug.Log("Table Data: " + dictTableInfo[1].id + " | " + dictTableInfo[1].earnings + " | " + dictTableInfo[1].delayTime);
        }
        else {
            // Debug.Log("CRIEI NOVO Table Data: YEH" );
            dictTableInfo = new TableInfo[48];
            for (int i = 0; i < dictTableInfo.Length; i++)
            {
                dictTableInfo[i] = new TableInfo();
            }
        }
        if (laptopParametersData != "{}")
        {
            dictLaptopParameters = JsonHelper.FromJson<LaptopParameters>(laptopParametersData);
            Debug.Log("Laptop Parameters: " + dictLaptopParameters[0].id + " | " + dictLaptopParameters[0].earningsBase + " | " + dictLaptopParameters[0].growthRate + " | " + dictLaptopParameters[0].balancing_production + " | " + dictLaptopParameters[0].decreaseTime + " | " + dictLaptopParameters[0].baseCost + " | " + dictLaptopParameters[0].balancing_cost + " | " + dictLaptopParameters[0].multiplier + " | " + dictLaptopParameters[0].level + " | " + dictLaptopParameters[0].buyBar);
            Debug.Log("Laptop Parameters: " + dictLaptopParameters[1].id + " | " + dictLaptopParameters[1].earningsBase + " | " + dictLaptopParameters[1].growthRate + " | " + dictLaptopParameters[1].balancing_production + " | " + dictLaptopParameters[1].decreaseTime + " | " + dictLaptopParameters[1].baseCost + " | " + dictLaptopParameters[1].balancing_cost + " | " + dictLaptopParameters[1].multiplier + " | " + dictLaptopParameters[1].level + " | " + dictLaptopParameters[1].buyBar);
        }
        else {
            dictLaptopParameters = new LaptopParameters[48];
            for (int i = 0; i < dictLaptopParameters.Length; i++)
            {
                dictLaptopParameters[i] = new LaptopParameters();
            }
            Debug.Log("CRIEI NOVO LAPTOP PARAMETERS: YEH" );
            Debug.Log("Elemento id do dictLaptopParameters: " + dictLaptopParameters[0].id);
        }
        if (tableParametersData != "{}")
        {
            dictTableParameters = JsonHelper.FromJson<TableParameters>(tableParametersData);
            Debug.Log("Table Parameters: " + dictTableParameters[0].id + " | " + dictTableParameters[0].earningsBase + " | " + dictTableParameters[0].growthRate + " | " + dictTableParameters[0].balancing_production + " | " + dictTableParameters[0].decreaseTime + " | " + dictTableParameters[0].baseCost + " | " + dictTableParameters[0].balancing_cost + " | " + dictTableParameters[0].multiplier + " | " + dictTableParameters[0].level + " | " + dictTableParameters[0].buyBar);
            Debug.Log("Table Parameters: " + dictTableParameters[1].id + " | " + dictTableParameters[1].earningsBase + " | " + dictTableParameters[1].growthRate + " | " + dictTableParameters[1].balancing_production + " | " + dictTableParameters[1].decreaseTime + " | " + dictTableParameters[1].baseCost + " | " + dictTableParameters[1].balancing_cost + " | " + dictTableParameters[1].multiplier + " | " + dictTableParameters[1].level + " | " + dictTableParameters[1].buyBar);
        }
        else {
            dictTableParameters = new TableParameters[48];
            for (int i = 0; i < dictTableParameters.Length; i++)
            {
                dictTableParameters[i] = new TableParameters();
            }
        }

    }

    public void SaveLaptopData(int laptopID, float earnings, float delayTime)
    {
        // Salvar os dados do laptop no dicionário
        LaptopInfo laptopInfo = new LaptopInfo();
        laptopInfo.id = laptopID;
        laptopInfo.earnings = earnings;
        laptopInfo.delayTime = delayTime;

        dictLaptopInfo[laptopID-1] = laptopInfo;

        Debug.Log("Laptop ID: " + laptopID + " | Earnings: " + earnings + " | Decrease Time: " + delayTime + " | Level: " );

        // Salvar os dados do jogo após cada atualização no dicionário dos laptops (opcional)
        SaveGameData();
    }

    public void SaveTableData(int tableID, float earnings, float delayTime)
    {
        // Salvar os dados do laptop no dicionário
        TableInfo tableInfo = new TableInfo();
        tableInfo.id = tableID;
        tableInfo.earnings = earnings;
        tableInfo.delayTime = delayTime;

        dictTableInfo[tableID-1] = tableInfo;
        Debug.Log("Table ID: " + tableID + " | Earnings: " + earnings + " | Decrease Time: " + delayTime + " | Level: " );

        // Salvar os dados do jogo após cada atualização no dicionário dos laptops (opcional)
        SaveGameData();
    }

    
    public void SaveTableParameters(int tableID, float earningsBase, float growthRate, float balancing_production, float decreaseTime, float baseCost, float balancing_cost, float multiplier, int level, float buyBar)
    {
        // Salvar os dados do laptop no dicionário
        TableParameters tableParameters = new TableParameters();
        tableParameters.id = tableID;
        tableParameters.earningsBase = earningsBase;
        tableParameters.growthRate = growthRate;
        tableParameters.balancing_production = balancing_production;
        tableParameters.decreaseTime = decreaseTime;
        tableParameters.baseCost = baseCost;
        tableParameters.balancing_cost = balancing_cost;
        tableParameters.multiplier = multiplier;
        tableParameters.level = level;
        tableParameters.buyBar = buyBar;


        dictTableParameters[tableID-1] = tableParameters;

        Debug.Log("Table ID: " + tableID + " | Earnings: " + earningsBase + " | Decrease Time: " + decreaseTime + " | Level: " + level);

        // Salvar os dados do jogo após cada atualização no dicionário dos laptops (opcional)
        SaveGameData();
    }

    public void SaveLaptopParameters (int laptopID, float earningsBase, float growthRate, float balancing_production, float decreaseTime, float baseCost, float balancing_cost, float multiplier, int level, float buyBar)
    {
        // Salvar os dados do laptop no dicionário
        LaptopParameters laptopParameters = new LaptopParameters();
        laptopParameters.id = laptopID;
        laptopParameters.earningsBase = earningsBase;
        laptopParameters.growthRate = growthRate;
        laptopParameters.balancing_production = balancing_production;
        laptopParameters.decreaseTime = decreaseTime;
        laptopParameters.baseCost = baseCost;
        laptopParameters.balancing_cost = balancing_cost;
        laptopParameters.multiplier = multiplier;
        laptopParameters.level = level;
        laptopParameters.buyBar = buyBar;

        dictLaptopParameters[laptopID-1] = laptopParameters;

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