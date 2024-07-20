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
        //�^�C�}�[�@�\
        if(Mathf.Floor(_timer) <= 0)
        {
            GameOver();
        }
        else if(!_pauseFlg)
        {
            _timer -= Time.deltaTime;
            _timerText.text = "�c�莞�ԁF"+Mathf.Floor(_timer).ToString();
        }
        //�X�R�A�̔��f
        _scoreText.text = $"�X�R�A�F{_score}";
        //�|�[�Y�̌Ăяo��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResume();
        }
        //�f�o�b�O�p
        if (Input.GetKeyDown(KeyCode.R)) _score++;
    }

    void PauseResume()//�|�[�Y����
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
    void GameOver()//�Q�[���I�����̏����������@
    {

        Debug.Log("�I��");
    }
}
