using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static int _score;
    [SerializeField, Tooltip("�Q�[���J�n���̃J�E���g�_�E���Ɏg��Text�Bnull�̏ꍇ�A�J�E���g�_�E�����Ȃ��Ȃ�܂�")]
    Text _startTimerText;
    [SerializeField] Text _scoreText;
    [SerializeField] Text _timerText;
    [SerializeField, Header("��\���ɂ��������̂����Ă�������")] GameObject[] _hideObjectAry;
    [SerializeField] float _timeLimit;
    [SerializeField] int _startTimer;
    [SerializeField] string _resultScene;
    List<Vector3> _stopObjectVelocity = new List<Vector3>();
    List<Rigidbody> _stopObject = new List<Rigidbody>();
    float _timer;
    bool _inGame = true;
    bool _pauseFlg = false;
    bool _timerStop;
    Vector3 _stopObjectTest;
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
        if (!_pauseFlg)//�^�C�}�[����������
        {
            //�X�R�A�̔��f
            _scoreText.text = $"�X�R�A�F{_score}";
            if (!_timerStop)
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
            }
        }
        //�|�[�Y�̌Ăяo��
        if (Input.GetKeyDown(KeyCode.Escape) && _inGame)
        {
            PauseResume();
        }
        //�f�o�b�O�p
        if (Input.GetKeyDown(KeyCode.P)) _score++;
        if (Input.GetKeyDown(KeyCode.O)) TimerStartOrStop();
    }
    void PauseResume()//�|�[�Y���� 
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
            else if (_pauseFlg)
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (obj.layer != 5 && rb != null)//velocity�Ƃ���rigidbody�����X�g�Ɋi�[����        layer=5�Ƃ�UI�����݂��郌�C���[�̂���
                {
                    _stopObject.Add(rb);
                    _stopObjectVelocity.Add(rb.velocity);
                    rb.constraints = RigidbodyConstraints.FreezePosition;   //�������~�߂�
                }
            }
        }
        if (!_pauseFlg)
        {
            for (int j = 0; j < _stopObject.Count; j++)//�ۑ�����velocity���Ďn��������
            {
                if (_stopObject[j] != null)
                {
                    _stopObject[j].constraints = RigidbodyConstraints.None;
                    _stopObject[j].velocity = _stopObjectVelocity[j];
                }
            }
            //���X�g�̃��Z�b�g
            _stopObject.Clear();
            _stopObjectVelocity.Clear();
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
    IEnumerator StartCount(int time)//�Q�[���J�n�̍��}
    {
        _inGame = false;
        PauseResume();//�n�߂��u�ԂɃ|�[�Y
        hideObject(false);
        while (true)
        {
            _startTimerText.text = time.ToString();
            yield return new WaitForSeconds(1);
            if (time <= 1)
            {
                _startTimerText.gameObject.SetActive(false);
                PauseResume();  //�R���[�`���I�����Ƀ|�[�Y����
                _inGame = true;
                hideObject(true);
                yield break;
            }
            else time--;
        }

    }
    void GameOver()//�Q�[���I�����̏����������@
    {
        Debug.Log("�Q�[���I��");
    }
    
}
