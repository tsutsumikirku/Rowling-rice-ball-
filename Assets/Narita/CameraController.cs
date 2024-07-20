using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _player;  // プレイヤーのTransform
    [SerializeField] Vector3 _offset;  // プレイヤーとカメラの相対位置 = 中心に映したいときは使わない
    [SerializeField] float _smoothTime = 0.3f;  // カメラがプレイヤーを追跡する際のスムーズさの調整用パラメータ
    private Vector3 velocity = Vector3.zero;  // カメラ移動時の速度ベクトル
    // プレイヤー移動後にカメラ移動をさせたいので、LateUpdateを使う
    void LateUpdate()
    {
        // プレイヤーの位置にカメラを追従させる
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
//調べたやつ