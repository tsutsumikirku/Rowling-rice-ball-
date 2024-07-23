using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ObjectBase : MonoBehaviour,IPause
{
    public void Pause()
    {
        throw new System.NotImplementedException();
    }

    public void Resume()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void DestroyPro(GameObject obj, float deletetime)
        {
         
        }

}
