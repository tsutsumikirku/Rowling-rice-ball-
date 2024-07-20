using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceBallManager : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _speedUp;
    [SerializeField] float _speedDown;
    [SerializeField] int _riceCount;
    [SerializeField] int _scaleChangeLine = 5;
    [SerializeField] Vector3 _plusScale;
    [SerializeField] string[] _itemTag;
    ItemType _itemType;
    enum ItemType
    {
        speedup,
        magnet,
        timestop,
        speeddown
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void GetItem()
    {
        switch (_itemType)
        {
            case ItemType.speedup:
                _moveSpeed += _speedUp;
                break;
            case ItemType.speeddown:
                _moveSpeed -= _speedDown;
                break;
            case ItemType.magnet:
                break;
            case ItemType.timestop:
                break;
        }
    }
    void Move()
    {
        _rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * _moveSpeed, 0, Input.GetAxisRaw("Vertical") * _moveSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == _itemTag[0])
        {
            _itemType = ItemType.speedup;
            GetItem();
        }
        if (collision.gameObject.tag == _itemTag[1])
        {
            _itemType = ItemType.speeddown;
            GetItem();
        }
        if (collision.gameObject.tag == _itemTag[2])
        {
            _itemType = ItemType.timestop;
            GetItem();
        }
        if (collision.gameObject.tag == _itemTag[3])
        {
            _itemType = ItemType.magnet;
            GetItem();
        }
        if (collision.gameObject.tag == _itemTag[4])
        {
            _riceCount++;
            if (_riceCount >= _scaleChangeLine)
            {
                transform.localScale += _plusScale;
            }
        }
        
    }
}
