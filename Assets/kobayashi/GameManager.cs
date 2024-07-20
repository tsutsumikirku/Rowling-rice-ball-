using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector]public static int _score;
    [SerializeField] float _timeLimit;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _timerText;
    bool _pauseFlg = false;
    float _timer;
    private void Start()
    {
        _timer = _timeLimit+1;
        _score = 0;
    }
    private void Update()
    {
        //タイマー機能
        if(Mathf.Floor(_timer) <= 0)
        {
            GameOver();
        }
        else if(!_pauseFlg)
        {
            _timer -= Time.deltaTime;
            _timerText.text = "残り時間："+Mathf.Floor(_timer).ToString();
        }
        //スコアの反映
        _scoreText.text = $"スコア：{_score}";
        //ポーズの呼び出し
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResume();
        }
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.R)) _score++;
    }

    void PauseResume()//ポーズ部分
    {
        _pauseFlg=!_pauseFlg;
        var obj = FindObjectsOfType<GameObject>();
        Debug.Log("Pause or Resume");
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
    void GameOver()//ゲーム終了時の処理を書く　
    {

        Debug.Log("終了");
    }
}
