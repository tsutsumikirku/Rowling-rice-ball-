using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSystem : MonoBehaviour, IPause
{
    bool _isFlag = true;
    RiceBallManager _ballManager;
    [SerializeField] float _itemSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _ballManager = GameObject.Find("RiceBall").GetComponent<RiceBallManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _ballManager.transform.position;
        transform.localScale = _ballManager.transform.localScale;
    }
    private void OnTriggerStay(Collider collision)
    {
        if (_isFlag)
        {
            if (collision.gameObject.tag != "Ground" && _ballManager._isMagnet && collision.gameObject.tag != "Player")
            {
                var rb = collision.gameObject.GetComponent<Rigidbody>();
                rb.AddForce((transform.position - collision.transform.position) * _itemSpeed);
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
}
