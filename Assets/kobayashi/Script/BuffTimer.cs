using UnityEngine;
using UnityEngine.UI;

public class BuffTimer : MonoBehaviour, IPause
{
    [SerializeField, Range(1, 100)] int _buffTime;
    public float _buffTimer;
    Image _image;
    Animator _animator;
    private void Awake()
    {
        _buffTimer = _buffTime * 0.01f;
        _animator?.SetFloat("TimerSpeed", _buffTimer);
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
        _animator?.SetFloat("TimerSpeed",_buffTimer);
    }
}
