using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static int _score;
    [SerializeField,Tooltip("�Q�[���J�n���̃J�E���g�_�E���Ɏg��Text�Bnull�̏ꍇ�A�J�E���g�_�E�����Ȃ��Ȃ�܂�")] 
    Text _startTimerText;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _timerText;
    [SerializeField, Header("��\���ɂ��������̂����Ă�������")] GameObject[] _hideObjectAry;
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
            //�^�C�}�[�@�\
            if (Mathf.Floor(_timer) <= 0)
            {
                GameOver();
            }
            else
            {
                _timer -= Time.deltaTime;
                _timerText.text = "�c�莞�ԁF" + Mathf.Floor(_timer).ToString();
            }
            //�X�R�A�̔��f
            _scoreText.text = $"�X�R�A�F{_score}";
        }
        //�|�[�Y�̌Ăяo��
        if (Input.GetKeyDown(KeyCode.Escape) && _fastCoroutine == null)
        {
            PauseResume();
        }
        //�f�o�b�O�p
        if (Input.GetKeyDown(KeyCode.R)) _score++;
    }

    void PauseResume()//�|�[�Y����
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
    IEnumerator StartCount(int time)//�Q�[���J�n�̍��}
    {
        PauseResume();//�n�߂��u�ԂɃ|�[�Y
        hideObject(false);
        while (true)
        {
            _startTimerText.text = time.ToString();
            yield return new WaitForSeconds(1);
            if (time <= 1)
            {
                _startTimerText.gameObject.SetActive(false);
                PauseResume();//�R���[�`���I�����Ƀ|�[�Y����
                hideObject(true);
                yield break;
            }
            else time--;
        }
    }
    void GameOver()//�Q�[���I�����̏����������@
    {
        SceneManager.LoadScene(_resultScene);
    }
}
