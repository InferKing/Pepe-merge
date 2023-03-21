using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
[System.Serializable]
public class Data
{
    public int money = 5000, perClick = 1;
    public float extraMoney = 1f, sale = 0f;
    public List<int> indexes = new List<int>();
    public Dictionary<int, int> onField = new Dictionary<int, int>()
    {
        {0, 0},
        {1, 0},
        {2, 0},
        {3, 0},
        {4, 0},
        {5, 0},
        {6, 0},
        {7, 0},
        {8, 0},
        {9, 0},
        {10, 0},
        {11, 0},
        {12, 0}
    };
    public Dictionary<int, int> upgradeMenu = new Dictionary<int, int>()
    {
        {0, 0},
        {1, 0},
        {2, 0}
    };
}
public class Player : MonoBehaviour
{
    public Data data;
    public static Player Instance;
    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);
    [DllImport("__Internal")]
    private static extern void LoadExtern();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadExtern();
        }
        else Destroy(gameObject);
    }
    public int GetPrice(int index)
    {
        int c = 0;
        foreach (int item in Instance.data.indexes)
        {
            if (item == index) c++;
        }
        return Mathf.RoundToInt(Mathf.Pow(1.1f, c));
    }
    public void Save()
    {
        string d = JsonConvert.SerializeObject(data);
        SaveExtern(d);
    }
    public void SetData(string value)
    {
        data = JsonConvert.DeserializeObject<Data>(value);
    }
}
