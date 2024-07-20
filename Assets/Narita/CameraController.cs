using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _player;  // �v���C���[��Transform
    [SerializeField] Vector3 _offset = new Vector3(0, 4.11f, -9.67f);  // �v���C���[�ƃJ�����̑��Έʒu = ���S�ɉf�������Ƃ��͎g��Ȃ�
    [SerializeField] float _smoothTime = 0.3f;  // �J�������v���C���[��ǐՂ���ۂ̃X���[�Y���̒����p�p�����[�^
    [SerializeField] float _cameraradius = 20f;
    private Vector3 velocity = Vector3.zero;  // �J�����ړ����̑��x�x�N�g��
    float th;
    Vector3 cycle;
    Vector3 mousePos;
    float mousex;
    float mousedeltax;




    // �v���C���[�ړ���ɃJ�����ړ������������̂ŁALateUpdate���g��
    private void Update()
    {
        Cursor.visible = false;
        mousePos = Input.mousePosition;
        mousedeltax = mousePos.x - mousex;
        mousex = mousePos.x;
        th += mousedeltax /300;
        mousePos = new Vector3(0, 0, 0);
        float x =_cameraradius * Mathf.Cos(th);
        float z = _cameraradius * Mathf .Sin(th);


        _offset = new Vector3(x,10, z);
        
        
        
    }
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
//���ׂ����