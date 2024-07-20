using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _player;  // �v���C���[��Transform
    [SerializeField] Vector3 _offset;  // �v���C���[�ƃJ�����̑��Έʒu = ���S�ɉf�������Ƃ��͎g��Ȃ�
    [SerializeField] float _smoothTime = 0.3f;  // �J�������v���C���[��ǐՂ���ۂ̃X���[�Y���̒����p�p�����[�^
    private Vector3 velocity = Vector3.zero;  // �J�����ړ����̑��x�x�N�g��
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
//���ׂ����