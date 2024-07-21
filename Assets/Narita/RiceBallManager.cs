using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceBallManager : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _moveSpeed; //スピード
    [SerializeField] float _speedUp;　//スピードアップ
    [SerializeField] float _speedDown;　//スピードダウン
    [SerializeField] int _riceCount;　//米を拾うたびに増えるカウント
    [SerializeField] int _scaleChangeLine = 5;　//上のカウントがこれを超えると
    [SerializeField] Vector3 _plusScale;　//スケールが大きくなる
    [SerializeField] string[] _itemTag;　//アイテムのタグ.1.スピアップ.2.スピダウン.3.時間停止.4.マグネット.5.米
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
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * Input.GetAxisRaw("Vertical") + Camera.main.transform.right * Input.GetAxisRaw("Horizontal");
        _rb.velocity = moveForward * _moveSpeed + new Vector3(0, _rb.velocity.y, 0);
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
