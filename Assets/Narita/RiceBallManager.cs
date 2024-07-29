using System.Collections;
using UnityEngine;

public class RiceBallManager : MonoBehaviour, IPause
{
    Rigidbody _rb;
    [SerializeField] float _defaultMoveSpeed;  //�X�s�[�h
    float _moveSpeed;
    [SerializeField] float _speedUp;�@//�X�s�[�h�A�b�v
    [SerializeField] float _speedDown;�@//�X�s�[�h�_�E��
    public static int _riceCount; //�Ă��E�����тɑ�����J�E���g
    int _scaleChangeLine;�@//��̃J�E���g������𒴂����
    [SerializeField] int _defaultScaleChangeLine = 5;//�f�t�H���g�̃��C��
    [SerializeField] Vector3 _plusScale;�@//�X�P�[�����傫���Ȃ�
    [SerializeField] string[] _itemTag;�@//�A�C�e���̃^�O.1.�X�s�A�b�v.2.�X�s�_�E��.3.���Ԓ�~.4.�}�O�l�b�g.5.��
    bool _isFlag = true;
    public bool _isMagnet;
    GameManager _gameManager;
    [SerializeField,Header("1,SpeedUp 2,SpeedDown 3,TimeStop 4,Magnet \n��WaitForSeconds�̒l")] float[] _waitTimes;
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
        //�A�C�e���̎�ނɉ����Ă���ɑΉ����鏈�����s��
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
        //�J�����̌����ɂ��킹�đO�㍶�E�����߂�
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * Input.GetAxisRaw("Vertical") + Camera.main.transform.right * Input.GetAxisRaw("Horizontal");
        _rb.velocity = moveForward * _moveSpeed + new Vector3(0, _rb.velocity.y, 0);
    }
    private void OnTriggerEnter(Collider collision)
    {
        //�擾�����A�C�e���̎�ނ��擾����
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
