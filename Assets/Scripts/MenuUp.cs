using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MenuUp : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _index, _mnozh;

    private void Start()
    {
        SetText();
        for (int i = 0; i < Player.Instance.data.upgradeMenu[_index]; i++)
        {
            _price = Mathf.RoundToInt(_price * _mnozh);
        }
    }
    public void SetText()
    {
        _text.text = "Цена: " + _price.ToString();
    }
    public void BuyUp()
    {
        if (Player.Instance.data.money >= _price)
        {
            Player.Instance.data.money -= _price;
            _price = Mathf.RoundToInt(_price * _mnozh);
            Player.Instance.data.upgradeMenu[0] += 1;
            switch (_index)
            {
                case 0:
                    Player.Instance.data.perClick += 1;
                    break;
                case 1:
                    Player.Instance.data.extraMoney += 0.1f;
                    break;
                case 2:
                    Player.Instance.data.sale += 0.05f;
                    break;
            }
            Player.Instance.Save();
        }
    }
}
