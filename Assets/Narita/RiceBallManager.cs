using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceBallManager : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        _rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * _moveSpeed, 0, Input.GetAxisRaw("Vertical") * _moveSpeed);
    }
}
