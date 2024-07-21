using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static int _score;
    [SerializeField, Tooltip("ゲーム開始時のカウントダウンに使うText。nullの場合、カウントダウンがなくなります")]
    Text _startTimerText;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _timerText;
    [SerializeField, Header("非表示にしたいものを入れてください")] GameObject[] _hideObjectAry;
    [SerializeField] float _timeLimit;
    [SerializeField] int _startTimer;
    [SerializeField] string _resultScene;
    [SerializeField]List<Vector3> _stopObjectVelocity = new List<Vector3>();
    [SerializeField]List<Rigidbody> _stopObject = new List<Rigidbody>();
    float _timer;
    bool _inGame = true;
    bool _pauseFlg = false;
    bool _timerStop;
    private void Start()
    {
        _timer = _timeLimit + 1;
        _score = 0;
        if (_startTimerText != null)
        {
            StartCoroutine(StartCount(_startTimer));
        }
    }
    private void Update()
    {
        if (!_pauseFlg)//タイマーが動く条件
        {
            //スコアの反映
            _scoreText.text = $"スコア：{_score}";
            if (!_timerStop)
            {
                //タイマー機能
                if (Mathf.Floor(_timer) <= 0)
                {
                    GameOver();
                }
                else
                {
                    _timer -= Time.deltaTime;
                    _timerText.text = "残り時間：" + Mathf.Floor(_timer).ToString();
                }
            }
        }
        //ポーズの呼び出し
        if (Input.GetKeyDown(KeyCode.Escape) && _inGame)
        {
            PauseResume();
        }
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.P)) _score++;
        if (Input.GetKeyDown(KeyCode.O)) TimerStartOrStop();
    }

    void PauseResume()//ポーズ部分
    {
        _pauseFlg = !_pauseFlg;
        var obj = FindObjectsOfType<GameObject>();
        Debug.Log("Pause or Resume");
        foreach (var i in obj)
        {
            var pause = i.GetComponent<IPause>();
            if (_pauseFlg&& pause!=null)
            {
                pause.Pause();
            }
            else if (!_pauseFlg && pause != null)
            {
                pause.Resume();
            }
            else if(_pauseFlg)
            {
                Rigidbody rb= i.GetComponent<Rigidbody>();
                if (i.layer != 5 && _pauseFlg && rb != null)
                {
                    _stopObject.Add(rb);
                    _stopObjectVelocity.Add(rb.velocity);
                    rb.constraints = RigidbodyConstraints.FreezePosition;
                }
            }
        }
        if (!_pauseFlg)
        {
            for (int j = 0; j > _stopObject.Count; j++)
            {
                _stopObject[j].velocity = _stopObjectVelocity[j];
                _stopObject[j].constraints = RigidbodyConstraints.None;
            }
        }
    }
    void TimerStartOrStop()
    {
        _timerStop = !_timerStop;
    }
    void hideObject(bool hide)
    {
        foreach (var obj in _hideObjectAry)
        {
            obj.SetActive(hide);
        }
    }
    IEnumerator StartCount(int time)//ゲーム開始の合図
    {
        _inGame = false;
        PauseResume();//始めた瞬間にポーズ
        hideObject(false);
        while (true)
        {
            _startTimerText.text = time.ToString();
            yield return new WaitForSeconds(1);
            if (time <= 1)
            {
                _startTimerText.gameObject.SetActive(false);
                PauseResume();//コルーチン終了時にポーズ解除
                _inGame=true;
                hideObject(true);
                yield break;
            }
            else time--;
        }
    }
    void GameOver()//ゲーム終了時の処理を書く　
    {
        Debug.Log("ゲーム終了");
    }
}
