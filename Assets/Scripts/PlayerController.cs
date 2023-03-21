using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public static System.Action PressedPepe;
    private GameObject selectedObject;
    private Vector3 offset;
    public void OnMousePressed()
    {
        Player.Instance.data.money += Player.Instance.data.perClick;
        _animator.SetTrigger("Pressed");
        PressedPepe?.Invoke();
        Player.Instance.Save();
    }
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
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
