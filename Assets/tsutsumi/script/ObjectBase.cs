using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ObjectBase : MonoBehaviour,IPause
{
    public void Pause()
    {
        
    }

    public void Resume()
    {
       
    }

    // Start is called before the first frame update
    void DestroyPro(GameObject obj, float deletetime)
        {
        StartCoroutine("dest");
        }
    IEnumerator dest()
    {
        yield return null;
    }

}
