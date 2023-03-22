using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    public static System.Action ItemBought;
    [SerializeField] private TMP_Text _money, _moneyPerSec;
    [SerializeField] private GameObject[] _menu;
    private void OnEnable()
    {
        PlayerController.PressedPepe += UpdateMoney;
    }
    private void OnDisable()
    {
        PlayerController.PressedPepe -= UpdateMoney;
    }
    private void Start()
    {
        StartCoroutine(UpdatePerSecUI());
        PlayerController.PressedPepe?.Invoke();
    }

    public void BuyItem(int index)
    {
        if (Player.Instance.data.money >= Model.Instance.data[index].price * (1-Player.Instance.data.sale) * Player.Instance.GetPrice(index))
        {
            Player.Instance.data.money -= Mathf.RoundToInt(Model.Instance.data[index].price * (1 - Player.Instance.data.sale) * Player.Instance.GetPrice(index));
            Player.Instance.data.indexes.Add(index);
            Player.Instance.data.onField[index] += 1;
            GameObject go = Instantiate(Model.Instance.items[index]);
            go.transform.position = new Vector3(Random.Range(-3f, 3f), Random.Range(-2f, 2f), 0);
            ItemBought?.Invoke();
            Player.Instance.Save();
        }
    }
    public void SetMenu(int index)
    {
        for (int i = 0; i < _menu.Length; i++)
        {
            if (i != index) _menu[i].SetActive(false);
        }
        _menu[index].SetActive(!_menu[index].activeSelf);
    }
    private void UpdateMoney()
    {
        _money.text = Player.Instance.data.money.ToString();

    }
    private IEnumerator UpdatePerSecUI()
    {
        while (true)
        {
            int money = Player.Instance.data.money;
            yield return new WaitForSeconds(1f);
            if (Player.Instance.data.money >= money)
            {
                _moneyPerSec.text = (Player.Instance.data.money - money).ToString() + "/сек";
            }
            else _moneyPerSec.text = "0/сек";
        }
    }
}
