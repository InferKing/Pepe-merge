using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    private bool _isWaiting = false, _isDelay = false;
    [DllImport("__Internal")]
    private static extern void ShowAdvVideo();
    [DllImport("__Internal")]
    private static extern void ShowAdvFull();
    public void RewardButtonClicked()
    {
        ShowAdvVideo();
        Player.Instance.data.money *= 2;
    }
    public void ShowFullAd()
    {
        if (!_isWaiting)
        {
            ShowAdvFull();
            _isWaiting = true;
        }
        else
        {
            if (!_isDelay) StartCoroutine(DelayAd());
        }
    }
    private IEnumerator DelayAd()
    {
        _isDelay = true;
        yield return new WaitForSeconds(30f);
        _isWaiting = false;
        _isDelay = false;
    }
}
