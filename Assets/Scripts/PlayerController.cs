using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
public class PlayerController : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void SetCurScore(int value);
    [SerializeField] private Animator _animator;
    public static System.Action PressedPepe;
    public static System.Action PressedPepe2;
    private GameObject selectedObject;
    private Vector3 offset;
    private void Start()
    {
        for (int i = 0; i < Model.Instance.items.Length; i++)
        {
            for (int j = 0; j < Player.Instance.data.onField[i]; j++)
            {
                GameObject go = Instantiate(Model.Instance.items[i]);
                go.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0);
            }
        }
    }
    public void ResetData()
    {
        Player.Instance.data.ResetData();
    }
    public void OnMousePressed()
    {
        Player.Instance.data.money += Player.Instance.data.perClick;
        _animator.SetTrigger("Pressed");
        PressedPepe?.Invoke();
        PressedPepe2?.Invoke();
        Player.Instance.Save();
    }
    void Update()
    {
        if (Player.Instance.data.maxMoney < Player.Instance.data.money)
        {
            Player.Instance.data.maxMoney = Player.Instance.data.money;
            SetCurScore(Player.Instance.data.maxMoney);
            Player.Instance.Save();
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition, LayerMask.GetMask("Item"));
            if (targetObject)
            {
                selectedObject = targetObject.transform.gameObject;
                selectedObject.GetComponent<Renderer>().sortingOrder += 1;
                selectedObject.GetComponent<Item>().SetStatus(ItemStatus.Get);
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            Item item = selectedObject.GetComponent<Item>();
            selectedObject.GetComponent<Renderer>().sortingOrder -= 1;
            item.CheckNear();
            item.SetStatus(ItemStatus.Idle);
            selectedObject = null;
            Player.Instance.Save();
        }
    }
}
