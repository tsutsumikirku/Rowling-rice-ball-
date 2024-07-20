using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _player;  // �v���C���[��Transform
    [SerializeField] Vector3 _offset = new Vector3(0, 4.11f, -9.67f);  // �v���C���[�ƃJ�����̑��Έʒu = ���S�ɉf�������Ƃ��͎g��Ȃ�
    [SerializeField] float _smoothTime = 0.3f;  // �J�������v���C���[��ǐՂ���ۂ̃X���[�Y���̒����p�p�����[�^
    private Vector3 velocity = Vector3.zero;  // �J�����ړ����̑��x�x�N�g��
    float th;
    Vector3 cycle;
    Vector3 mousePos;
    float mouse;
    float mousedelta;


    // �v���C���[�ړ���ɃJ�����ړ������������̂ŁALateUpdate���g��
    private void Update()
    {
        
        mousePos = Input.mousePosition;
        mousedelta = mousePos.x - mouse;
        mouse = mousePos.x;
        th += mousedelta /1000;
        
        
        float x =20 * Mathf.Cos(th);     
        float y =20 * Mathf.Sin(th);
        _offset = new Vector3(x,4, y);
        
        
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