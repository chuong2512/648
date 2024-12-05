using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [SerializeField] AudioSource _bgMusicSource;

    [Space(10)]
    [Range(0, 1)] [SerializeField] float _musicVolume = 1f;
    [Range(0, 1)] [SerializeField] float _fxVolume = 1f;

    [Header("Audio Data")]
    [SerializeField] AudioDataSO _audioData;

    bool _musicEnable = true;
    bool _fxEnable = true;

    GameObject oneShotGameObject;
    AudioSource oneShotAudioSource;

    private void Start()
    {
        _fxEnable = PlayerPrefs.GetInt("sfxState") == 0;
        _musicEnable = PlayerPrefs.GetInt("musicState") == 0;

        if (_musicEnable) PlayBackgroundMusic();
    }

    public void PlayAudio(AudioType type)
    {
        // return if audio fx is disable
        if (!_fxEnable) return;

        if (oneShotGameObject == null)
        {
            oneShotGameObject = new GameObject("Sound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
        }

        AudioClip clip = GetClip(type);
        oneShotAudioSource.volume = _fxVolume;
        oneShotAudioSource.PlayOneShot(clip);
    }

    public void ToggleMusic(ref bool state)
    {
        _musicEnable = !_musicEnable;
        UpdateMusic();
        state = _musicEnable;
        PlayerPrefs.SetInt("musicState", _musicEnable ? 0 : 1);
    }

    public void ToggleFX(ref bool state)
    {
        _fxEnable = !_fxEnable;
        state = _fxEnable;
        PlayerPrefs.SetInt("sfxState", _fxEnable ? 0 : 1);
    }

    // private method
    void PlayBackgroundMusic()
    {
        _bgMusicSource.Stop();
        _bgMusicSource.clip = _audioData.BackgroundMusic;
        _bgMusicSource.volume = _musicVolume;
        _bgMusicSource.Play();
    }

    void UpdateMusic()
    {
        if (!_musicEnable)
            _bgMusicSource.Stop();
        else
            PlayBackgroundMusic();
    }

    AudioClip GetClip(AudioType type)
    {
        switch (type)
        {
            case AudioType.BACKGROUND:
                return _audioData.BackgroundMusic;
            case AudioType.MOVE:
                return _audioData.MoveClip;
            case AudioType.ROTATE:
                return _audioData.RotateClip;
            case AudioType.DROP:
                return _audioData.DropClip;
            case AudioType.CLEARLINE:
                return _audioData.ClearLineClip;
            case AudioType.GAMEOVER:
                return _audioData.GameOverClip;
            default:
                return _audioData.FailClip;
        }
    }
}
