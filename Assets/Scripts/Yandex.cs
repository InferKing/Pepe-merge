using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAdvVideo();
    public void RewardButtonClicked()
    {
        ShowAdvVideo();
        Player.Instance.data.money *= 2;
    }
}
