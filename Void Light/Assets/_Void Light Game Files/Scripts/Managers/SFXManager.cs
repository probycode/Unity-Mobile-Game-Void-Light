using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour {
    
    public delegate void SFXManagerEventHandler();

    public static event SFXManagerEventHandler SFXStatChanged;

    public static SFXManager Instance;
    public static bool isSFXOn = true;
    public AudioClip leaveSFX;
    public float delaySFXTime;
    public float volume;
    public AudioMixer audioMixer;

    private static AudioSource _audioSource;
    public static float initVolume;

    public static bool OnSFXStatChanged()
    {
        ToggleSFX();
        if (SFXStatChanged != null)
        {
            SFXStatChanged();
            return isSFXOn;
        }
        return isSFXOn;
    }

    static void ToggleSFX()
    {
        print("toggleSFx");
        if (isSFXOn == true)
        {
            isSFXOn = false;
            SFXManager.Instance.audioMixer.SetFloat("sfxVol", -80);
        }
        else if (isSFXOn == false)
        {
            isSFXOn = true;
            SFXManager.Instance.audioMixer.SetFloat("sfxVol", 0);
        }
        GameManager.playerData.isSFXOn = isSFXOn;
        GameManager.SaveGame();
    }

    void InitSFXSetting()
    {
        if (isSFXOn == false)
        {
            audioMixer.SetFloat("sfxVol", -80);
        }
        else if (isSFXOn == true)
        {
            audioMixer.SetFloat("sfxVol", 0);
        }
    }

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        initVolume = _audioSource.volume;
        GameManager.LeftArea += GameManager_LeftArea;
        GameManager.GameLoaded += GameManager_GameLoaded;
    }

    private void GameManager_GameLoaded()
    {
        isSFXOn = GameManager.playerData.isSFXOn;
        InitSFXSetting();
    }

    private void OnDestroy()
    {
        GameManager.GameLoaded -= GameManager_GameLoaded;
        GameManager.LeftArea -= GameManager_LeftArea;
    }

    private void GameManager_LeftArea()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay ()
    {
        yield return new WaitForSeconds(delaySFXTime);
        GetComponent<AudioSource>().PlayOneShot(leaveSFX, volume);
    }

    public static IEnumerator PlayOneShot (AudioClip sfx, float volume)
    {
        if (initVolume != volume)
        {
            float initVolume = _audioSource.volume;
        }
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(sfx);

        while(_audioSource.isPlaying)
        {
            yield return null;
        }
        _audioSource.volume = initVolume;
    }
}
