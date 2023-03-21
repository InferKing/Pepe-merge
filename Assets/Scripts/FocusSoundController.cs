using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusSoundController : MonoBehaviour
{
    void OnApplicationFocus(bool hasFocus)
    {
        Silence(!hasFocus);
    }

    void OnApplicationPause(bool isPaused)
    {
        Silence(isPaused);
    }

    private void Silence(bool silence)
    {
        AudioListener.volume = silence ? 0 : 1;
    }
    public void OffMusic()
    {
        AudioListener.volume = 0;
    }
    public void OnMusic()
    {
        AudioListener.volume = 1;
    }
}
