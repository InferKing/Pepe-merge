using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "ItemData", order = 51)]
public class ItemData : ScriptableObject
{
    public Sprite sprite;
    public string pepeName;
    public int mnozh;
    public int price;
}
