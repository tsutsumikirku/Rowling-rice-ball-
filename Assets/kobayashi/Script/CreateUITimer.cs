using UnityEngine;
using UnityEngine.UI;

public class CreateUITimer : MonoBehaviour, IPause
{
    [SerializeField] GameObject _pealent;
    [SerializeField] GameObject _prefab;
    [SerializeField]Sprite _sprite;
    bool _isPause;
    public void CreateBuffTimer(float timer)
    {
        GameObject createObj=Instantiate(_prefab, _pealent.transform);
        createObj.GetComponent<BuffTimer>()._buffTimer=timer;
        Image image=createObj.GetComponent<Image>();
        image.sprite=_sprite;
        if (_isPause)
        {
            createObj.GetComponent<Animator>().SetFloat("TimerSpeed", 0.0f);
        }
    }
    public void CreateBuffTimer(GameObject prefab)
    {
        GameObject createObj = Instantiate(prefab, _pealent.transform);
        if (_isPause)
        {
            createObj.GetComponent<Animator>().SetFloat("TimerSpeed", 0.0f);
        }
    }
    public void CreateBuffTimer(float timer,Sprite sprite)
    {
        GameObject createObj = Instantiate(_prefab, _pealent.transform);
        createObj.GetComponent<BuffTimer>()._buffTimer = timer;
        Image image = createObj.GetComponent<Image>();
        image.sprite = sprite;
        if (_isPause)
        {
            createObj.GetComponent<Animator>().SetFloat("TimerSpeed", 0.0f);
        }
    }
    void IPause.Pause()
    {
        _isPause = true;
    }
    void IPause.Resume()
    {
        _isPause=false;
    }
}
