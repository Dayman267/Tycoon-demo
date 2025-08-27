using UnityEngine;
using UnityEngine.UI;

public sealed class SoundButtonController : MonoBehaviour
{
    private bool _isSoundOn = true;
    
    private Image _image;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void ChangeSoundBoolean()
    {
        _isSoundOn = !_isSoundOn;
        
        if (_isSoundOn)
        {
            _image.sprite = soundOnSprite;
        }
        else
        {
            _image.sprite = soundOffSprite;
        }
    }
}
