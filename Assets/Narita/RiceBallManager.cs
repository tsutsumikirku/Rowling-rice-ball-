using System.Collections;
using UnityEngine;

public class RiceBallManager : MonoBehaviour, IPause
{
    Rigidbody _rb;
    [SerializeField] float _defaultMoveSpeed;  //スピード
    float _moveSpeed;
    [SerializeField] float _speedUp;　//スピードアップ
    [SerializeField] float _speedDown;　//スピードダウン
    public static int _riceCount; //米を拾うたびに増えるカウント
    int _scaleChangeLine;　//上のカウントがこれを超えると
    [SerializeField] int _defaultScaleChangeLine = 5;//デフォルトのライン
    [SerializeField] Vector3 _plusScale;　//スケールが大きくなる
    [SerializeField] string[] _itemTag;　//アイテムのタグ.1.スピアップ.2.スピダウン.3.時間停止.4.マグネット.5.米
    bool _isFlag = true;
    public bool _isMagnet;
    GameManager _gameManager;
    [SerializeField,Header("1,SpeedUp 2,SpeedDown 3,TimeStop 4,Magnet \nのWaitForSecondsの値")] float[] _waitTimes;
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
        _moveSpeed = _defaultMoveSpeed;
        _isMagnet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFlag)
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
                _moveSpeed = _moveSpeed * _speedUp;
                StartCoroutine(StartSpeedUp());
                break;
            case ItemType.speeddown:
                _moveSpeed = _moveSpeed * _speedDown;
                StartCoroutine(StartSpeedDown());
                break;
            case ItemType.magnet:
                _isMagnet = true;
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
        if (_isFlag)
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
        _isFlag = false;
    }

    public void Resume()
    {
        _isFlag = true;
    }
    IEnumerator StartSpeedUp()
    {
        yield return new WaitForSeconds(_waitTimes[0]);
        _moveSpeed = _defaultMoveSpeed;
    }
    IEnumerator StartSpeedDown()
    {
        yield return new WaitForSeconds(_waitTimes[1]);
        _moveSpeed = _defaultMoveSpeed;
    }
    IEnumerator StartTimerStartOrStop()
    {
        yield return new WaitForSeconds(_waitTimes[2]);
        _gameManager.TimerStartOrStop();
    }
    IEnumerator StartMagnet()
    {
        yield return new WaitForSeconds(_waitTimes[3]);
        _isMagnet = false;
    }
}
