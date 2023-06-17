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
        public int  room_id;
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
        public int room_id;
    }

    [System.Serializable]
    public class ActionInfo
    {
        public int id;
        public bool bought;
    }

    [System.Serializable]
    public class ManagerInfo
    {
        public int id;
        public bool bought;
    }

    [System.Serializable]

    public class ExpandInfo
    {
        public int room_id;

        public bool owned;
    }


    public static float money;

    public static float multiplier;

    public static bool menuOpen = false;

    public static LaptopInfo[] dictLaptopInfo = new LaptopInfo[48];

    public static LaptopParameters[] dictLaptopParameters = new LaptopParameters[48];

    public static TableParameters[] dictTableParameters = new TableParameters[48];

    public static TableInfo[] dictTableInfo = new TableInfo[48];

    public static ActionInfo[] dictActionInfo = new ActionInfo[4];

    public static ManagerInfo[] dictManagerInfo = new ManagerInfo[10];

    public static ExpandInfo[] dictExpandInfo = new ExpandInfo[4];

    public AudioSource musicSource;

    public GameObject desksRoom2;
    public GameObject desksRoom3;
    public GameObject desksRoom4;

    public GameObject squareRoom2;

    public GameObject squareRoom3;

    public GameObject squareRoom4;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        //ResetGameData();
        money = 0.0f;
        multiplier = 0.0f;
        musicSource.Play();
        LoadGameData(desksRoom2, desksRoom3, desksRoom4, squareRoom2, squareRoom3, squareRoom4);
        // money = 10000000.0f;
        // SaveGameData();
    }

    public static void ResetGameData()
    {
        PlayerPrefs.SetString("ExpandData", "");
        PlayerPrefs.SetString("ManagerData", "");
        PlayerPrefs.SetString("ActionData", "");
        PlayerPrefs.SetString("LaptopData", "");
        PlayerPrefs.SetString("TableData", "");
        PlayerPrefs.SetString("LaptopParametersData", "");
        PlayerPrefs.SetString("TableParametersData", "");
        PlayerPrefs.SetFloat("Money", 0.0f);
        SaveGameData();
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

    public static float CalculateProduction(float multiplication, float upgrade, float growthRate, float balancing, int id_room)
    {
        return multiplication * 10f *id_room* Mathf.Log((upgrade + 1f) * growthRate) + balancing - upgrade;
    }

    public static float CalculateCost(float baseCost, float growthRate, float upgrade, float balancing, int id_room)
    {
        if (upgrade == 0f)
        {
            Debug.Log("Custo total: " + (baseCost*id_room + (baseCost*growthRate*balancing) + 1f));
            return baseCost*id_room + (baseCost*growthRate*balancing) + 1f;
        }
        Debug.Log("Custo total: " + (baseCost*id_room + (baseCost* growthRate * upgrade  * balancing) + 1f));
        return baseCost*id_room + (baseCost* growthRate * upgrade  * balancing) + 1f;
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
        string actionDataToJSON = JsonHelper.ToJson(dictActionInfo, true);
        string managerDataToJSON = JsonHelper.ToJson(dictManagerInfo, true);
        string expandDataToJSON = JsonHelper.ToJson(dictExpandInfo, true);

        PlayerPrefs.SetString("LaptopData", laptopDataToJSON);
        PlayerPrefs.SetString("TableData", tableDataToJSON);
        PlayerPrefs.SetString("LaptopParametersData", laptopParametersDataToJSON);
        PlayerPrefs.SetString("TableParametersData", tableParametersDataToJSON);
        PlayerPrefs.SetString("ActionData", actionDataToJSON);
        PlayerPrefs.SetString("ManagerData", managerDataToJSON);
        PlayerPrefs.SetString("ExpandData", expandDataToJSON);
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

    public static ManagerInfo GetManagerInfo(int id)
    {
        // Retorna as informações do laptop com o ID especificado
        if (dictManagerInfo[id-1].id == id)
        {
            return dictManagerInfo[id-1];
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

    public static ActionInfo GetActionInfo(int actionID)
    {
        // Retorna as informações do laptop com o ID especificado
        if (dictActionInfo[actionID-1].id == actionID)
        {
            return dictActionInfo[actionID-1];
        }
        else
        {
            return null;
        }
    }

   public static ExpandInfo GetExpandInfo(int room_id)
    {
        // Retorna as informações do laptop com o ID especificado
        if (dictExpandInfo[room_id-1].room_id == room_id)
        {
            return dictExpandInfo[room_id-1];
        }
        else
        {
            return null;
        }
    }

    public static void LoadGameData(GameObject desksRoom2, GameObject desksRoom3, GameObject desksRoom4, GameObject squareRoom2, GameObject squareRoom3, GameObject squareRoom4)
    {

        money = PlayerPrefs.GetFloat("Money", 0.0f);

        //

        // Carregar os dados do dicionário dos laptops
        string laptopData = PlayerPrefs.GetString("LaptopData", "");
        string tableData = PlayerPrefs.GetString("TableData", "");
        string laptopParametersData = PlayerPrefs.GetString("LaptopParametersData", "");
        string tableParametersData = PlayerPrefs.GetString("TableParametersData", "");
        string actionData = PlayerPrefs.GetString("ActionData", "");
        string managerData = PlayerPrefs.GetString("ManagerData", "");
        string expandData = PlayerPrefs.GetString("ExpandData", "");


        if (laptopData != "")
        {
            dictLaptopInfo = JsonHelper.FromJson<LaptopInfo>(laptopData);
        }
        else {

            Debug.Log("CRIEI NOVO Laptop Data: YEH" );
            dictLaptopInfo = new LaptopInfo[48];
            for (int i = 0; i < dictLaptopInfo.Length; i++)
            {
                dictLaptopInfo[i] = new LaptopInfo();
            }
        }
        if (tableData != "")
        {
            dictTableInfo = JsonHelper.FromJson<TableInfo>(tableData);
        }
        else {
            // Debug.Log("CRIEI NOVO Table Data: YEH" );
            dictTableInfo = new TableInfo[48];
            for (int i = 0; i < dictTableInfo.Length; i++)
            {
                dictTableInfo[i] = new TableInfo();
            }
        }
        if (laptopParametersData != "")
        {
            dictLaptopParameters = JsonHelper.FromJson<LaptopParameters>(laptopParametersData);
            // Debug.Log("Laptop Parameters: " + dictLaptopParameters[0].id + " | " + dictLaptopParameters[0].earningsBase + " | " + dictLaptopParameters[0].growthRate + " | " + dictLaptopParameters[0].balancing_production + " | " + dictLaptopParameters[0].decreaseTime + " | " + dictLaptopParameters[0].baseCost + " | " + dictLaptopParameters[0].balancing_cost + " | " + dictLaptopParameters[0].multiplier + " | " + dictLaptopParameters[0].level + " | " + dictLaptopParameters[0].buyBar);
            // Debug.Log("Laptop Parameters: " + dictLaptopParameters[1].id + " | " + dictLaptopParameters[1].earningsBase + " | " + dictLaptopParameters[1].growthRate + " | " + dictLaptopParameters[1].balancing_production + " | " + dictLaptopParameters[1].decreaseTime + " | " + dictLaptopParameters[1].baseCost + " | " + dictLaptopParameters[1].balancing_cost + " | " + dictLaptopParameters[1].multiplier + " | " + dictLaptopParameters[1].level + " | " + dictLaptopParameters[1].buyBar);
        }
        else {
            dictLaptopParameters = new LaptopParameters[48];
            for (int i = 0; i < dictLaptopParameters.Length; i++)
            {
                dictLaptopParameters[i] = new LaptopParameters();
            }
            // Debug.Log("CRIEI NOVO LAPTOP PARAMETERS: YEH" );
            // Debug.Log("Elemento id do dictLaptopParameters: " + dictLaptopParameters[0].id);
        }
        if (tableParametersData != "")
        {
            dictTableParameters = JsonHelper.FromJson<TableParameters>(tableParametersData);
            // Debug.Log("Table Parameters: " + dictTableParameters[0].id + " | " + dictTableParameters[0].earningsBase + " | " + dictTableParameters[0].growthRate + " | " + dictTableParameters[0].balancing_production + " | " + dictTableParameters[0].decreaseTime + " | " + dictTableParameters[0].baseCost + " | " + dictTableParameters[0].balancing_cost + " | " + dictTableParameters[0].multiplier + " | " + dictTableParameters[0].level + " | " + dictTableParameters[0].buyBar);
            // Debug.Log("Table Parameters: " + dictTableParameters[1].id + " | " + dictTableParameters[1].earningsBase + " | " + dictTableParameters[1].growthRate + " | " + dictTableParameters[1].balancing_production + " | " + dictTableParameters[1].decreaseTime + " | " + dictTableParameters[1].baseCost + " | " + dictTableParameters[1].balancing_cost + " | " + dictTableParameters[1].multiplier + " | " + dictTableParameters[1].level + " | " + dictTableParameters[1].buyBar);
        }
        else {
            dictTableParameters = new TableParameters[48];
            for (int i = 0; i < dictTableParameters.Length; i++)
            {
                dictTableParameters[i] = new TableParameters();
            }
        }
        if (actionData != "")
        {
            dictActionInfo = JsonHelper.FromJson<ActionInfo>(actionData);
            // Debug.Log("Action Info: " + dictActionInfo[0].id + " | " + dictActionInfo[0].bought);
        }
        else {
            dictActionInfo = new ActionInfo[4];
            for (int i = 0; i < dictActionInfo.Length; i++)
            {
                dictActionInfo[i] = new ActionInfo();
            }
        }
        if (managerData != ""){
            dictManagerInfo = JsonHelper.FromJson<ManagerInfo>(managerData);
        }
        else {
            dictManagerInfo = new ManagerInfo[10];
            for (int i = 0; i < dictManagerInfo.Length; i++)
            {
                dictManagerInfo[i] = new ManagerInfo();
            }
        }
        if (expandData != ""){
            dictExpandInfo = JsonHelper.FromJson<ExpandInfo>(expandData);
            for (int i = 0; i < dictExpandInfo.Length; i++)
            {
                if (dictExpandInfo[i].room_id == 2){
                    if (dictExpandInfo[i].owned){
                        Debug.Log("DEU TRUE E O ID É 2");
                        desksRoom2.SetActive(true);
                        squareRoom2.SetActive(false);
                    } else{
                        Debug.Log("DEU FALSE E O ID É 2");
                        desksRoom2.SetActive(false);
                        squareRoom2.SetActive(true);
                    }
                } else if (dictExpandInfo[i].room_id == 3){
                    if (dictExpandInfo[i].owned){
                        desksRoom3.SetActive(true);
                        squareRoom3.SetActive(false);
                    } else{
                        desksRoom3.SetActive(false);
                        squareRoom3.SetActive(true);
                    }
                } else if (dictExpandInfo[i].room_id == 4){
                    if (dictExpandInfo[i].owned){
                        desksRoom4.SetActive(true);
                        squareRoom4.SetActive(false);
                    } else{
                        desksRoom4.SetActive(false);
                        squareRoom4.SetActive(true);
                    }
                }
                Debug.Log("Expand Info: " + dictExpandInfo[i].room_id + " | " + dictExpandInfo[i].owned);
            }
        }
        else {
            dictExpandInfo = new ExpandInfo[4];
            for (int i = 0; i < dictExpandInfo.Length; i++)
            {
                dictExpandInfo[i] = new ExpandInfo();
            }
        }

    }

    public void SaveLaptopData(int laptopID, float earnings, float delayTime, int room_id)
    {
        // Salvar os dados do laptop no dicionário
        LaptopInfo laptopInfo = new LaptopInfo();
        laptopInfo.id = laptopID;
        laptopInfo.earnings = earnings;
        laptopInfo.delayTime = delayTime;
        laptopInfo.room_id = room_id;

        dictLaptopInfo[laptopID-1] = laptopInfo;

        Debug.Log("Laptop ID: " + laptopID + " | Earnings: " + earnings + " | Decrease Time: " + delayTime + " | Room ID: " + room_id);

        SaveGameData();
    }

    public void SaveTableData(int tableID, float earnings, float delayTime, int room_id)
    {
        TableInfo tableInfo = new TableInfo();
        tableInfo.id = tableID;
        tableInfo.earnings = earnings;
        tableInfo.delayTime = delayTime;
        tableInfo.room_id = room_id;

        dictTableInfo[tableID-1] = tableInfo;
        Debug.Log("Table ID: " + tableID + " | Earnings: " + earnings + " | Decrease Time: " + delayTime + " | Room ID: " + room_id);

        SaveGameData();
    }

    
    public void SaveTableParameters(int tableID, float earningsBase, float growthRate, float balancing_production, float decreaseTime, float baseCost, float balancing_cost, float multiplier, int level, float buyBar)
    {
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

        SaveGameData();
    }

    public void SaveLaptopParameters (int laptopID, float earningsBase, float growthRate, float balancing_production, float decreaseTime, float baseCost, float balancing_cost, float multiplier, int level, float buyBar)
    {
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

        SaveGameData();
    }

    public void SaveActionData(int actionID, bool bought)
    {
        // Salvar os dados do laptop no dicionário
        ActionInfo actionInfo = new ActionInfo();
        actionInfo.id = actionID;
        actionInfo.bought = bought;

        dictActionInfo[actionID-1] = actionInfo;

        Debug.Log("Action ID: " + actionID + " | Bought: " + bought);

        SaveGameData();
    }

    public void SaveManagerData(int id_manager, bool bought)
    {
        // Salvar os dados do laptop no dicionário
        ManagerInfo managerInfo = new ManagerInfo();
        managerInfo.id = id_manager;
        managerInfo.bought = bought;

        dictManagerInfo[id_manager-1] = managerInfo;

        Debug.Log("Manager ID: " + id_manager + " | Managers: " + bought);

        SaveGameData();
    }

    public void SaveExpandData(int room_id, bool owned)
    {
        ExpandInfo expandInfo = new ExpandInfo();
        expandInfo.room_id = room_id;
        expandInfo.owned = owned;

        dictExpandInfo[room_id-1] = expandInfo;

        if (room_id == 2){
            if (owned){
                desksRoom2.SetActive(true);
                squareRoom2.SetActive(false);
            } else{
                desksRoom2.SetActive(false);
                squareRoom2.SetActive(true);
            }
        } else if (room_id == 3){
            if (owned){
                desksRoom3.SetActive(true);
                squareRoom3.SetActive(false);
            } else{
                desksRoom3.SetActive(false);
                squareRoom3.SetActive(true);
            }
        } else if (room_id == 4){
            if (owned){
                desksRoom4.SetActive(true);
                squareRoom4.SetActive(false);
            } else{
                desksRoom4.SetActive(false);
                squareRoom4.SetActive(true);
            }
        }

        Debug.Log("Expand ID: " + room_id + " | Owned: " + owned);

        SaveGameData();
    }


    public void apertaTeclaApagar()
    // se apertar a tecla q
    {  if (Input.GetKeyDown(KeyCode.Q))
        PlayerPrefs.DeleteAll();
    }


}