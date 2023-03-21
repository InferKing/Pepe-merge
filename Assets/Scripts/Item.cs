using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemStatus
{
    Idle,
    Get,
    Merge
}

public class Item : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider2D _box;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private ItemData _data;
    private float _eps = 0.3f;
    private ItemStatus _status = ItemStatus.Idle;
    [HideInInspector] public int mnozh;
    private void Start()
    {
        mnozh = _data.mnozh;
        _spriteRenderer.sprite = _data.sprite;
        StartCoroutine(AddMoney());
        StartCoroutine(Move());
    }
    private IEnumerator Move()
    {
        while (_status is not ItemStatus.Merge)
        {
            yield return new WaitForSeconds(Random.Range(2f,3f));
            yield return StartCoroutine(GoTo());
        }
    }
    private IEnumerator AddMoney()
    {
        while (_status is not ItemStatus.Merge)
        {
            yield return new WaitForSeconds(2f);
            Player.Instance.data.money += Mathf.RoundToInt(Mathf.Pow(2, mnozh) * Player.Instance.data.extraMoney);
            PlayerController.PressedPepe?.Invoke();
        }
    }
    private IEnumerator GoTo()
    {
        Vector3 vect = Vector3.zero;
        vect = new Vector3(Random.Range(transform.position.x - _eps, transform.position.x + _eps),
                Random.Range(transform.position.y - _eps, transform.position.y + _eps), transform.position.z);
        int mn = 1;
        if (vect.x > 5) { vect.x = 5; mn = 5; }
        if (vect.x < -7) vect.x = -7;
        if (vect.y > 4.5f) vect.y = 4.5f;
        if (vect.y < -2f) vect.y = -2f;
        _animator.SetTrigger("Walked");
        while (_status != ItemStatus.Get && _status != ItemStatus.Merge && (vect-transform.position).magnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, vect, Time.deltaTime * 0.1f * mn);
            yield return null;
        }
        yield return null;
    }
    public void UpdateAnim()
    {
        _animator.SetTrigger("Pressed");
    }
    public void SetStatus(ItemStatus stat)
    {
        _status = stat;
    }
    public void CheckNear()
    {
        List<Item> items = new List<Item>();
        Collider2D[] colls = Physics2D.OverlapBoxAll(transform.position, _box.size, 0);
        foreach (Collider2D it in colls)
        {
            if (it != _box)
            {
                items.Add(it.GetComponent<Item>());
            }
        }
        foreach (Item it in items)
        {
            if (it.mnozh == mnozh)
            {
                if (mnozh == Model.Instance.data.Length-1) return;
                Destroy(it.gameObject);
                mnozh = Model.Instance.data[mnozh+1].mnozh;
                _spriteRenderer.sprite = Model.Instance.data[mnozh].sprite;
                _particleSystem.textureSheetAnimation.SetSprite(0, _spriteRenderer.sprite);
                _particleSystem.Play();
                Player.Instance.data.onField[mnozh - 1] -= 2;
                return;
            }
        }
    }
}
