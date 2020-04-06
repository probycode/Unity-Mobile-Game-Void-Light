using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour {

    public delegate void MusicManagerEventHandler();
    public delegate void MusicLoopIncreasedEventHandler(int loopIndex);

    public static event MusicManagerEventHandler MusicStatChanged;
    public static event MusicLoopIncreasedEventHandler MusicLoopIncreased;

    private static bool isMusicOn = true;
    public static int amountOfLoopLayers;

    public static MusicManager Instance = null;
    public GameObject content;
    public GameObject[] loops;
    public int loopIndex;
    public AudioMixer audioMixer;

    public static bool IsMusicOn
    {
        get
        {
            return isMusicOn;
        }

        protected set
        {
            isMusicOn = value;
        }
    }

    public static void OnMusicLoopIncreased(int loopIndex)
    {
        if (MusicLoopIncreased != null)
        {
            MusicLoopIncreased(loopIndex);
        }
    }

    public static bool OnMusicStatChanged()
    {
        ToggleMusic();
        if (MusicStatChanged != null)
        {
            MusicStatChanged();
            return IsMusicOn;
        }
        return IsMusicOn;
    }

    static void ToggleMusic  ()
    {
        if (IsMusicOn == false)
        {
            IsMusicOn = true;
            MusicManager.Instance.audioMixer.SetFloat("musicVol", 0);
        }
        else if (IsMusicOn == true)
        {
            IsMusicOn = false;
            MusicManager.Instance.audioMixer.SetFloat("musicVol", -80);
        }
        GameManager.playerData.isMusicOn = IsMusicOn;
        GameManager.SaveGame();
    }

    void InitMusicSetting ()
    {
        if (IsMusicOn == false)
        {
            audioMixer.SetFloat("musicVol", -80);
        }
        else if (IsMusicOn == true)
        {
            audioMixer.SetFloat("musicVol", 0);
        }
    }

    private void Awake()
    {
        GameManager.LeftArea += GameManager_LeftArea;
        GameManager.GameStart += GameManager_GameStart;
        PlayerController.PlayerDead += PlayerController_PlayerDead;
        GameManager.GameLoaded += GameManager_GameLoaded;
        GameManager.GameSavePlayerCurrentStreak += GameManager_GameSavePlayerCurrentStreak;
        
        InitLoops();
        IncrementMusic();
    }

    private void GameManager_GameSavePlayerCurrentStreak()
    {
        
    }

    private void GameManager_GameLoaded()
    {
        IsMusicOn = GameManager.playerData.isMusicOn;
        InitMusicSetting();
    }

    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        InitMusicSetting();
    }

    void InitLoops ()
    {
        amountOfLoopLayers = content.transform.childCount;
        loops = new GameObject[content.transform.childCount];
        for (int i = 0; i < content.transform.childCount; i++)
        {
            loops[i] = content.transform.GetChild(i).gameObject;
        }
    }

    private void PlayerController_PlayerDead()
    {
        DecrementMusic();
    }

    private void GameManager_GameStart()
    {
        if(loopIndex == 1)
        {
            IncrementMusic();
        }
    }

    private void GameManager_LeftArea()
    {
        IncrementMusic();
    }

    private void OnDestroy()
    {
        GameManager.GameSavePlayerCurrentStreak -= GameManager_GameSavePlayerCurrentStreak;
        GameManager.LeftArea -= GameManager_LeftArea;
        GameManager.GameStart -= GameManager_GameStart;
        PlayerController.PlayerDead -= PlayerController_PlayerDead;
        GameManager.GameLoaded -= GameManager_GameLoaded;
    }

    public void IncrementMusic()
    {
        if (loopIndex < loops.Length)
        {
            for (int p = 0; p < loops[loopIndex].transform.childCount; p++)
            {
                MusicLoop loopAnim = loops[loopIndex].transform.GetChild(p).GetComponent<MusicLoop>();

                loopAnim.TransitionUp();
            }
            loopIndex++;
        }
        OnMusicLoopIncreased(loopIndex);
    }

    public void DecrementMusic()
    {
        if (loopIndex > 1 )
        {
            loopIndex--;
            if (loopIndex < loops.Length)
            {
                for (int p = 0; p < loops[loopIndex].transform.childCount; p++)
                {
                    MusicLoop loopAnim = loops[loopIndex].transform.GetChild(p).GetComponent<MusicLoop>();

                    loopAnim.TransitionDown();
                }
            }
        }
    }
}
