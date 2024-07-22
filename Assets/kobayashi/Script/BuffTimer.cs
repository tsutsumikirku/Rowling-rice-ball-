using UnityEngine;
using UnityEngine.UI;

public class BuffTimer : MonoBehaviour, IPause
{
    [SerializeField] float _buffTime;
    Image _image;
    Animator _animator;
    private void Start()
    {
        _image = GetComponent<Image>();
        _animator = GetComponent<Animator>();
        _animator.SetFloat("TimerSpeed", _buffTime);
        AnimationPlay();
    }
    void Update()
    {
        if (_image.fillAmount<=0)
        {
            Destroy(gameObject);
        }
    }
    void AnimationPlay()
    {
        _animator.Play("BuffTimer");
    }
    void IPause.Pause()
    {
        _animator.SetFloat("TimerSpeed", 0.0f);
    }
    void IPause.Resume()
    {
        _animator.SetFloat("TimerSpeed",_buffTime);
    }
}
