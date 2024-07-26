using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceBallManager : MonoBehaviour, IPause
{
    Rigidbody _rb;
    [SerializeField] float _moveSpeed; //�X�s�[�h
    [SerializeField] float _speedUp;�@//�X�s�[�h�A�b�v
    [SerializeField] float _speedDown;�@//�X�s�[�h�_�E��
    public static int _riceCount; //�Ă��E�����тɑ�����J�E���g
    int _scaleChangeLine;�@//��̃J�E���g������𒴂����
    [SerializeField] int _defaultScaleChangeLine = 5;//�f�t�H���g�̃��C��
    [SerializeField] Vector3 _plusScale;�@//�X�P�[�����傫���Ȃ�
    [SerializeField] string[] _itemTag;�@//�A�C�e���̃^�O.1.�X�s�A�b�v.2.�X�s�_�E��.3.���Ԓ�~.4.�}�O�l�b�g.5.��
    List<GameObject> _items = new List<GameObject>();
    [SerializeField] float _itemSpeed;�@//�A�C�e�����z���񂹂�X�s�[�h
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
        //�A�C�e���̎�ނɉ����Ă���ɑΉ����鏈�����s��
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
        //�J�����̌����ɂ��킹�đO�㍶�E�����߂�
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = cameraForward * Input.GetAxisRaw("Vertical") + Camera.main.transform.right * Input.GetAxisRaw("Horizontal");
        _rb.velocity = moveForward * _moveSpeed + new Vector3(0, _rb.velocity.y, 0);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //�擾�����A�C�e���̎�ނ��擾����
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
        //�R���C�_�[�̒��ɂ���A�C�e�����z���񂹂�
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
