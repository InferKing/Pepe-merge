using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextController : MonoBehaviour
{
    [SerializeField] private MoneyText[] _textsMoney;
    private void OnEnable()
    {
        PlayerController.PressedPepe2 += UpdateText;
    }
    private void OnDisable()
    {
        PlayerController.PressedPepe2 -= UpdateText;
    }
    private void UpdateText()
    {
        foreach (var text in _textsMoney)
        {
            if (!text.isEnabled)
            {
                text.ShowAnim("+" + Player.Instance.data.perClick.ToString());
                return;
            }
        }
    }
}