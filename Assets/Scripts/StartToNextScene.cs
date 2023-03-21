using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartToNextScene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(StartTransition());
    }
    private IEnumerator StartTransition()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }
}
