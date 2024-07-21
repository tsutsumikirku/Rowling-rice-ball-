using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static int _score;
    [SerializeField,Tooltip("ゲーム開始時のカウントダウンに使うText。nullの場合、カウントダウンがなくなります")] 
    Text _startTimerText;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _timerText;
    [SerializeField, Header("非表示にしたいものを入れてください")] GameObject[] _hideObjectAry;
    [SerializeField] float _timeLimit;
    [SerializeField] int _startTimer;
    [SerializeField] string _resultScene;
    bool _pauseFlg = false;
    float _timer;
    Coroutine _fastCoroutine;
    private void Start()
    {
        _timer = _timeLimit + 1;
        _score = 0;
        if (_startTimerText != null)
        {
            _fastCoroutine = StartCoroutine(StartCount(_startTimer));
        }
    }
    private void Update()
    {
        if (!_pauseFlg)
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
            //スコアの反映
            _scoreText.text = $"スコア：{_score}";
        }
        //ポーズの呼び出し
        if (Input.GetKeyDown(KeyCode.Escape) && _fastCoroutine == null)
        {
            PauseResume();
        }
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.R)) _score++;
    }

    void PauseResume()//ポーズ部分
    {
        _pauseFlg = !_pauseFlg;
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
    void hideObject(bool hide)
    {
        foreach(var obj in _hideObjectAry)
        {
            obj.SetActive(hide);
        }
    }
    IEnumerator StartCount(int time)//ゲーム開始の合図
    {
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
                hideObject(true);
                yield break;
            }
            else time--;
        }
    }
    void GameOver()//ゲーム終了時の処理を書く　
    {
        SceneManager.LoadScene(_resultScene);
    }
}
