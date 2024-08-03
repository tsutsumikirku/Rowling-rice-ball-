using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static int _score;
    [SerializeField, Tooltip("ゲーム開始時のカウントダウンに使うText。nullの場合、カウントダウンがなくなります")]
    Text _startTimerText;
    [SerializeField] GameObject _optionsUiPanel;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _timerText;
    [SerializeField, Header("カウントダウン中、非表示にしたいものを入れてください")] GameObject[] _hideObjectAry;
    [SerializeField] float _timeLimit;
    [SerializeField] int _startCountDownTimer;
    [SerializeField] string _resultScene;
    List<Vector3> _stopObjectVelocity = new List<Vector3>();
    List<Rigidbody> _stopObject = new List<Rigidbody>();
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
            StartCoroutine(StartCount(_startCountDownTimer));
        }
        _optionsUiPanel.SetActive(false);
    }
    private void Update()
    {
        if (!_pauseFlg)//タイマーが動く条件
        {
            //スコアの反映
            if (!_timerStop)
            {
                if(_scoreText!=null)_scoreText.text = $"スコア：{RiceBallManager._riceCount}";
                //タイマー機能
                if (Mathf.Floor(_timer) <= 0)
                {
                    GameOver();
                }
                else
                {
                    if (_timerText != null)
                    {
                        _timer -= Time.deltaTime;
                        _timerText.text = "残り時間：" + Mathf.Floor(_timer).ToString();
                    }
                }
            }
        }
        //ポーズの呼び出し
        if (Input.GetKeyDown(KeyCode.Escape) && _inGame)
        {
            PauseResume(); 
            if (_optionsUiPanel != null) _optionsUiPanel.SetActive(_pauseFlg);//_optionsUiPanel?.としたかったが、なぜかできなかった。
        }

        //デバッグ用
        if (Input.GetKeyDown(KeyCode.P)) _score++;
        //この二つは同時に使う予定がありません。そのため予期せぬエラーが起きますが気にしないでください。
        //直すのは簡単
        if (Input.GetKeyDown(KeyCode.O)) ObjectStop();
        if (Input.GetKeyDown(KeyCode.L)) TimerStartOrStop();
    }
    void PauseResume()//ポーズ部分 
    {
        _pauseFlg = !_pauseFlg;
        var i = FindObjectsOfType<GameObject>();
        foreach (var obj in i)
        {
            var pause = obj.GetComponent<IPause>();
            if (_pauseFlg && pause != null)
            {
                pause.Pause();
            }
            else if (!_pauseFlg && pause != null)
            {
                pause.Resume();
            }
            else if (_pauseFlg && !_timerStop)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (obj.layer != 5 && rb != null)//velocityとそのrigidbodyをリストに格納する        layer=5とはUIが存在するレイヤーのこと
                {
                    _stopObject.Add(rb);
                    _stopObjectVelocity.Add(rb.velocity);
                    rb.constraints = RigidbodyConstraints.FreezePosition;   //動きを止める
                }
            }
        }
        if (!_pauseFlg && !_timerStop)
        {
            for (int j = 0; j < _stopObject.Count; j++)//保存したvelocityを再始動させる
            {
                if (_stopObject[j] != null)
                {
                    _stopObject[j].constraints = RigidbodyConstraints.None;
                    _stopObject[j].velocity = _stopObjectVelocity[j];
                }
            }
            //リストのリセット
            _stopObject.Clear();
            _stopObjectVelocity.Clear();
        }
    }
    public void TimerStartOrStop()
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
            if (time <= 0)
            {
                _startTimerText.gameObject.SetActive(false);
                PauseResume();  //コルーチン終了時にポーズ解除
                _inGame = true;
                hideObject(true);
                yield break;
            }
            else
            {
                yield return new WaitForSeconds(1);
                time--;
            }
        }
    }
    public void ObjectStop()
    {
        _timerStop = !_timerStop;
        var i = FindObjectsOfType<GameObject>();
        foreach (var obj in i)
        {
            var pause = obj.GetComponent<IPause>();
            if (_timerStop && obj.gameObject.tag != "Player" && obj.GetComponent<BuffTimer>() == null)
            {
                pause?.Pause();
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (obj.layer != 5 && rb != null && !_pauseFlg && obj.gameObject.tag != "Player")//velocityとそのrigidbodyをリストに格納する        layer=5とはUIが存在するレイヤーのこと
                {
                    _stopObject.Add(rb);
                    _stopObjectVelocity.Add(rb.velocity);
                    rb.constraints = RigidbodyConstraints.FreezePosition;   //動きを止める
                }
            }
            else if (!_timerStop && obj.gameObject.tag != "Player")
            {
                pause?.Resume();
            }
        }
        if (!_timerStop && !_pauseFlg)
        {
            for (int j = 0; j < _stopObject.Count; j++)//保存したvelocityを再始動させる
            {
                if (_stopObject[j] != null)
                {
                    _stopObject[j].constraints = RigidbodyConstraints.None;
                    _stopObject[j].velocity = _stopObjectVelocity[j];
                }
            }
            //リストのリセット
            _stopObject.Clear();
            _stopObjectVelocity.Clear();
        }
    }
    void GameOver()//ゲーム終了時の処理を書く　
    {
        Debug.Log("ゲーム終了");
    }
}