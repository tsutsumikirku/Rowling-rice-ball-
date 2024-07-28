using System.Collections;
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
    bool _flag = true;
    public bool _magnet;
    GameManager _gameManager;
    [SerializeField] float _waitTimeTimerStop = 5;
    [SerializeField] float _waitTimeMagnet = 5;
    GameObject _magnetObj;
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
        _gameManager = FindObjectOfType<GameManager>();
        _magnetObj = GameObject.Find("MagnetArea");
    }

    // Update is called once per frame
    void Update()
    {
        if (_flag)
        {
            Move();
            //_magnetObj.transform.position = transform.position;
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
                _magnet = true;
                StartCoroutine(StartMagnet());
                break;
            case ItemType.timestop:
                _gameManager.TimerStartOrStop();
                StartCoroutine(StartTimerStartOrStop());
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
    private void OnTriggerEnter(Collider collision)
    {
        //取得したアイテムの種類を取得する
        if (_flag)
        {
            if (collision.gameObject.tag == _itemTag[0]) //SpeedUp
            {
                _itemType = ItemType.speedup;
                GetItem();
            }
            if (collision.gameObject.tag == _itemTag[1]) //SpeedDown
            {
                _itemType = ItemType.speeddown;
                GetItem();
            }
            if (collision.gameObject.tag == _itemTag[2]) //TimeStop
            {
                _itemType = ItemType.timestop;
                GetItem();
            }
            if (collision.gameObject.tag == _itemTag[3]) //Magnet
            {
                _itemType = ItemType.magnet;
                GetItem();
            }
            if (collision.gameObject.tag == _itemTag[4]) //Rice
            {
                _riceCount++;
                if (_riceCount >= _scaleChangeLine)
                {
                    transform.localScale += _plusScale;
                    _scaleChangeLine += _defaultScaleChangeLine;
                    Debug.Log(_scaleChangeLine);
                }
            }
            if (collision.gameObject.tag != "Ground" && collision.gameObject.tag != "MagnetArea")
            {
                Destroy(collision.gameObject);
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
    IEnumerator StartTimerStartOrStop()
    {
        yield return new WaitForSeconds(_waitTimeTimerStop);
        _gameManager.TimerStartOrStop();
    }
    IEnumerator StartMagnet()
    {
        yield return new WaitForSeconds(_waitTimeMagnet);
        _magnet = false;
    }
}
