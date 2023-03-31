using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MoneyText : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button _button;
    [SerializeField] private TMP_Text m_Text;
    [HideInInspector] public bool isEnabled = false;
    public void ShowAnim(string text)
    {
        gameObject.SetActive(true);
        isEnabled = true;
        m_Text.text = text;
        StartCoroutine(Anim());
    }
    private IEnumerator Anim()
    {
        float time = 0f;
        m_Text.transform.position = new Vector3(_button.transform.position.x, -1f, 0);
        while (time < 1)
        {
            m_Text.transform.position = new Vector3(m_Text.transform.position.x, m_Text.transform.position.y + Time.deltaTime, 0);
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        isEnabled = false;
        m_Text.text = "";
        gameObject.SetActive(isEnabled);
    }
}