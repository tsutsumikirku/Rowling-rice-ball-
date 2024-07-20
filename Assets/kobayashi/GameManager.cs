using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int _score;
    bool _pauseFlg = false;
    void StaticrReset()
    {
        _score = 0;
    }
    void PauseResume()
    {
        _pauseFlg=!_pauseFlg;
        var obj = FindObjectsOfType<GameObject>();
        foreach (var i in obj)
        {
            var pause = i.GetComponent<IPause>();
            if (_pauseFlg)
            {
                pause?.Pause();
            }
            else if (!_pauseFlg)
            {
                pause?.Resume();
            }
        }
    }
}
