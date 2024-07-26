using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceBallManager : MonoBehaviour, IPause
{
    Rigidbody _rb;
    [SerializeField] float _moveSpeed; //スピード
    [SerializeField] float _speedUp;　//スピードアップ
    [SerializeField] float _speedDown;　//スピードダウン
    public static int _riceCount; //米を拾うたびに増えるカウント
    int _scaleChangeLine;　//上のカウントがこれを超えると
    [SerializeField] int _defaultScaleChangeLine = 5;//デフォルトのライン
    [SerializeField] Vector3 _plusScale;　//スケールが大きくなる
    [SerializeField] string[] _itemTag;　//アイテムのタグ.1.スピアップ.2.スピダウン.3.時間停止.4.マグネット.5.米
    List<GameObject> _items = new List<GameObject>();
    [SerializeField] float _itemSpeed;　//アイテムを吸い寄せるスピード
    bool _flag;
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
        _scaleChangeLine = _defaultScaleChangeLine;
    }

    // Update is called once per frame
    void Update()
    {
        if (_flag)
        {
            Move();
        }
    }
    void GetItem()
    {
        //アイテムの種類に応じてそれに対応する処理を行う
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
        //カメラの向きにあわせて前後左右を決める
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * Input.GetAxisRaw("Vertical") + Camera.main.transform.right * Input.GetAxisRaw("Horizontal");
        _rb.velocity = moveForward * _moveSpeed + new Vector3(0, _rb.velocity.y, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //取得したアイテムの種類を取得する
        if (_flag)
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
                    _scaleChangeLine += _defaultScaleChangeLine;
                    Debug.Log(_scaleChangeLine);
                }
            }
            if (collision.gameObject.tag != "Ground")
            {
                Destroy(collision.gameObject);
            }
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        //コライダーの中にあるアイテムを吸い寄せる
        if (_flag)
        {
            if (collision.gameObject.tag != "Ground")
            {
                _items.Add(collision.gameObject);
                foreach (var item in _items)
                {
                    var rb = item.GetComponent<Rigidbody>();
                    rb.AddForce((transform.position - item.transform.position) * _itemSpeed);
                }
            }
        }
    }

    public void Pause()
    {
        _flag = false;
    }

    public void Resume()
    {
        _flag = true;
    }
}
