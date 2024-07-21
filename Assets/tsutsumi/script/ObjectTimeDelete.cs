using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimeDelete : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float distime = 0.1f;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        }
        else
        {
            Destroy(collision.gameObject,distime);
        }
    }
}
