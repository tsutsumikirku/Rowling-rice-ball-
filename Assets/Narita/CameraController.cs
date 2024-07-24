using UnityEngine;
public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _player;  // プレイヤーのTransform
    [SerializeField] float _smoothTime = 0.3f;  // カメラがプレイヤーを追跡する際のスムーズさの調整用パラメータ
    [SerializeField] float _cameraradius = 20f;//カメラの回転する半径
    [SerializeField] float _camerahigh;//カメラの高さ
    [SerializeField] static float _mousesensivity;//マウス感度
    private Vector3 velocity = Vector3.zero;  // カメラ移動時の速度ベクトル
    float th;
    float mousePos;
    Vector3 _offset;
   //堤 伎六作シネマシンを使わずにカメラ移動
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
