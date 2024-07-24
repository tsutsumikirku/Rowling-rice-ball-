using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _player;  // �v���C���[��Transform
    [SerializeField] float _smoothTime = 0.3f;  // �J�������v���C���[��ǐՂ���ۂ̃X���[�Y���̒����p�p�����[�^
    [SerializeField] float _cameraradius = 20f;//�J�����̉�]���锼�a
    [SerializeField] float _camerahigh;//�J�����̍���
    [SerializeField] static float _mousesensivity;//�}�E�X���x
    private Vector3 velocity = Vector3.zero;  // �J�����ړ����̑��x�x�N�g��
    float th;
    float mousePos;
    Vector3 _offset;
   //�� ��Z��V�l�}�V�����g�킸�ɃJ�����ړ�
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mousePos= Input.GetAxis("Mouse X");
        if(_mousesensivity >= 0.1f)
        {
            th -= mousePos *( _mousesensivity / 100);
        }
        else
        {
            th -= mousePos;
        }
        float x =_cameraradius * Mathf.Cos(th);
        float z =_cameraradius * Mathf.Sin(th);
        _offset = new Vector3(x,_camerahigh, z);
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
