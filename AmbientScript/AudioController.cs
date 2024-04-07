using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField] private Sprite _musicOn;
    [SerializeField] private Sprite _musicOff;
    [SerializeField] private Sprite _volumeOn;
    [SerializeField] private Sprite _volumeOff;
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderVolume;
    [SerializeField] private Image _musicImage;
    [SerializeField] private Image _volumeImage;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _volumeSource;

    private float musicValueSave;
    private float volumeValueSave;
    void Start()
    {
        musicValueSave = PlayerPrefs.GetFloat("MusicValue",0.5f);
        volumeValueSave = PlayerPrefs.GetFloat("VolumeValue",1f);
        _sliderMusic.value = musicValueSave;
        _sliderVolume.value = volumeValueSave;
        _musicImage.sprite = _musicOn;
        _volumeImage.sprite = _volumeOn;
    }

    void FixedUpdate()
    {
        _musicSource.volume = _sliderMusic.value;
        PlayerPrefs.SetFloat("MusicValue", _musicSource.volume);
        _volumeSource.volume = _sliderVolume.value;
        PlayerPrefs.SetFloat("VolumeValue", _volumeSource.volume);
        if (_sliderMusic.value > 0)
            _musicImage.sprite = _musicOn;
        else
            _musicImage.sprite = _musicOff;
        if (_sliderVolume.value > 0)
            _volumeImage.sprite = _volumeOn;
        else
            _volumeImage.sprite = _volumeOff;
    }

    public void ButtonClick()
    {
        _volumeSource.PlayOneShot(_volumeSource.clip);
    }
}
