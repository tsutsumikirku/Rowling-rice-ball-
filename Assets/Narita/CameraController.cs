using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _player;  // �v���C���[��Transform
    [SerializeField] float _smoothTime = 0f;  // �J�������v���C���[��ǐՂ���ۂ̃X���[�Y���̒����p�p�����[�^
    [SerializeField] float _cameraradius = 20f;//�J�����̉�]���锼�a
    [SerializeField] float _camerahigh;//�J�����̍���
    [SerializeField] Text _testtext;
    [SerializeField] bool _test;
    public static float _mousesensivity = 30f;//�}�E�X���x
    private Vector3 velocity = Vector3.zero;  // �J�����ړ����̑��x�x�N�g��
    float th;
    float fi;
    float mousePosX;
    float mousePosY;
    Vector3 _offset;
   //�� ��Z��V�l�}�V�����g�킸�ɃJ�����ړ�
    private void Update()
    {
        if(_testtext && _test)
        {
            _testtext.text = _mousesensivity.ToString();
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mousePosX = Input.GetAxis("Mouse X");
        mousePosY = Input.GetAxis("Mouse Y");
        if(_mousesensivity >= 0.1f)
        {
            th -= mousePosX *( _mousesensivity / 100);
        }
        else
        {
            th -= mousePosX;
        }
        if (_mousesensivity >= 0.1f)
        {
            fi += mousePosY * (_mousesensivity / 100);
        }
        else
        {
            fi += mousePosY;
        }

        float x =_cameraradius * Mathf.Cos(th);
        float y = _cameraradius * Mathf.Cos(fi);
        float z =_cameraradius * Mathf.Sin(th);
        _offset = new Vector3(x,y, z);
    }
    // �v���C���[�ړ���ɃJ�����ړ������������̂ŁALateUpdate���g��
    void LateUpdate()
    {
        // �v���C���[�̈ʒu�ɃJ������Ǐ]������
        if (_player != null)
        {
            Vector3 targetPosition = _player.position + _offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _smoothTime);
            var rotation = Quaternion.LookRotation(_player.transform.position - transform.position);
            transform.rotation = rotation;
        }

        if (_player == null)
        {
            this.gameObject.transform.parent = null;
        }
    }
}
