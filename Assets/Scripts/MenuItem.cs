using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MenuItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _namePepe, _price;
    [SerializeField] private Image _image;
    [SerializeField] private int _index;
    private void Start()
    {
        UpdateInfo();

    }
    public void UpdateInfo()
    {
        _namePepe.text = Model.Instance.data[_index].pepeName;
        _price.text = "Цена: " + Mathf.RoundToInt(Model.Instance.data[_index].price * (1-Player.Instance.data.sale) * Player.Instance.GetPrice(_index)).ToString();
        _image.sprite = Model.Instance.data[_index].sprite;
    }
}
