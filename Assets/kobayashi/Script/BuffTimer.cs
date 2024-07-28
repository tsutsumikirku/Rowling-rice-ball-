using UnityEngine;
using UnityEngine.UI;

public class BuffTimer : MonoBehaviour, IPause
{
    [HideInInspector]public float _buffTimer;
    Image _image;
    Animator _animator;
    private void Awake()
    {
        _animator?.SetFloat("TimerSpeed", _buffTimer/100);
    }
    private void Start()
    {
        _image = GetComponent<Image>();
        _animator = GetComponent<Animator>();
        AnimationPlay();
    }
    private void Update()
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
        _animator?.SetFloat("TimerSpeed", 0.0f);
    }
    void IPause.Resume()
    {
        _animator?.SetFloat("TimerSpeed",_buffTimer/100);
    }
}
