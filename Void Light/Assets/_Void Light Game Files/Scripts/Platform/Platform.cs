using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Right,
    Left
}

public enum PlatformType
{
    Simple,
    Juke,
    RandomSpeed,
    Fade,
    Wild
}

[RequireComponent(typeof(FadePlatform))]
[RequireComponent(typeof(AudioSource))]

public class Platform : MonoBehaviour {

    public delegate void PlatformEventHandler();
    public delegate void ReachedLastPlatEventHandler(GameObject platformGO);

    public static event PlatformEventHandler PlatformTypeChanged;
    public static event ReachedLastPlatEventHandler ReachedLastPlat;


    [Header("Platform General Settings")]
    public float maxClamp;
    public float minClamp;
    public float moveSpeed;
    public SpriteRenderer runeRenderer;
    private PlatformType platformType;
    private int id;

    private bool shouldMove;
    private Direction platDirection;

    [Header("Random Start Speed Settings")]
    public bool isStartWithRanSpeeds;
    public float ranSpeed;
    public float ranSpeedMax = 4;
    public float ranSpeedMin = 2;

    [Header("Random Speed Over Time Settings")]
    public bool isRanChangeSpeedInterval;
    public float ranIntervalTimeMax = 4;
    public float ranIntervalTimeMin = 2;
    public float ranChangeSpeedDelay;

    [Header("Random Direction Over Time Settings")]
    public bool isRanDir;
    private int ranDir;

    [Header("Rotate Over Time Settings")]
    public bool isRotateType;

    [Header("Fade Over Time Settings")]
    public bool isFadePlateType;
    public float fadeSpeed;
    public Animator runeAnimator;
    [HideInInspector]
    public FadePlatform fadePlatform;

    [Header("Init Data")]
    [Tooltip("Put in same order as PlatformType to keep them, alined")]
    public Sprite[] runesSprites;
    public Dictionary<PlatformType, Sprite> runes;
    private AudioSource _AudioSource;

    public GameObject platformPartsSprites;
    public SpriteRenderer ClosedSprite;
    public AudioClip breakSFX;
    public GameObject breakPart;
    public GameObject cover;
    [HideInInspector]
    public bool isHighScorePlat = false;
    private static int idCounter;

    public PlatformType PlatformType
    {
        get
        {
            return platformType;
        }
        set
        {
            platformType = value;
            InitPlatformType();
            OnPlatformTypeChanged();
        }
    }

    public int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    public static void OnPlatformTypeChanged()
    {
        //print("Platform: OnPlatformTypeChanged()");
        if (PlatformTypeChanged != null)
        {
            PlatformTypeChanged();
        }
    }

    public static void OnReachedLastPlat(GameObject platformGO)
    {
        //print("Platform: OnPlatformTypeChanged()");
        if (ReachedLastPlat != null)
        {
            ReachedLastPlat(platformGO);
        }
    }

    private void Awake()
    {
        PlayerController.PlayerReachedStartPlat += PlayerController_PlayerReachedStartPlat;
        runes = new Dictionary<PlatformType, Sprite>();
        PlayerController.PlayerDead += GameManager_PlayerDead;
        fadePlatform = GetComponent<FadePlatform>();
        _AudioSource = GetComponent<AudioSource>();
        InitRuneSprites();
    }

    private void PlayerController_PlayerReachedStartPlat()
    {
        
    }

    private void OnDestroy()
    {
        PlayerController.PlayerReachedStartPlat -= PlayerController_PlayerReachedStartPlat;
        PlayerController.PlayerDead -= GameManager_PlayerDead;
    }

    private void GameManager_PlayerDead()
    {
        ResetObj();
    }

    void Start()
    {
        platDirection = RandomDirection();
        StartCoroutine(Delay());
    }

    void InitRuneSprites ()
    {
        for (int i = 0; i < runesSprites.Length; i++)
        {
            //Adds an extra 1 so PlatformType simple gets skiped
            //print(PlatformType + i + 1);
            runes.Add(PlatformType + i + 1, runesSprites[i]);
        }
    }

    void InitPlatformType ()
    {
        if (PlatformType == PlatformType.Simple)
        {
            isStartWithRanSpeeds = false;
            isRanChangeSpeedInterval = false;
            isRanDir = false;
            isFadePlateType = false;
            GetComponent<Animator>().enabled = false;
            runeAnimator.enabled = true;
            platformPartsSprites.SetActive(true);
            //runeRenderer.sprite = null;
        }
        else if (PlatformType == PlatformType.RandomSpeed)
        {
            //Atrabutts
            ranIntervalTimeMin = 2;
            ranIntervalTimeMax = 4f;

            isStartWithRanSpeeds = false;
            isRanChangeSpeedInterval = true;
            isRanDir = false;
            isFadePlateType = false;
            runeAnimator.enabled = true;
            GetComponent<Animator>().enabled = false;
            platformPartsSprites.SetActive(true);
            runeRenderer.sprite = runes[PlatformType.RandomSpeed];
        }
        else if (PlatformType == PlatformType.Juke)
        {
            //Atrabutts
            ranIntervalTimeMin = 0;
            ranIntervalTimeMax = 3f;


            isStartWithRanSpeeds = false;
            isRanChangeSpeedInterval = false;
            isRanDir = true;
            isFadePlateType = false;
            GetComponent<Animator>().enabled = false;
            runeAnimator.enabled = true;
            platformPartsSprites.SetActive(true);
            runeRenderer.gameObject.transform.localScale = new Vector3(0.16f, 0.17f, 0.17f);
            runeRenderer.sprite = runes[PlatformType.Juke];
        }
        else if (PlatformType == PlatformType.Fade)
        {
            StopAllCoroutines();
            //Atrabutts
            ranIntervalTimeMin = 3;
            ranIntervalTimeMax = 5;

            isStartWithRanSpeeds = false;
            isRanChangeSpeedInterval = false;
            isRanDir = false;
            isFadePlateType = true;
            runeAnimator.enabled = false;
            platformPartsSprites.SetActive(false);
            runeRenderer.sprite = runes[PlatformType.Fade];
        }
        else if (PlatformType == PlatformType.Wild)
        {
            //Atrabutts
            ranIntervalTimeMin = 3;
            ranIntervalTimeMax = 5f;

            isStartWithRanSpeeds = true;
            isRanChangeSpeedInterval = true;
            isRanDir = true;
            isFadePlateType = true;
            runeAnimator.enabled = false;
            platformPartsSprites.SetActive(true);
            runeRenderer.sprite = runes[PlatformType.Wild];
        }
    }

    IEnumerator FadeIterval()
    {
        ranChangeSpeedDelay = Random.Range(ranIntervalTimeMin, ranIntervalTimeMax);
        fadePlatform.StartFadeIn(0);
        yield return new WaitForSeconds(ranChangeSpeedDelay);
        fadePlatform.StartFadeOut(0);

        StartCoroutine(FadeIterval());
    }

    IEnumerator Delay ()
    {
        ranChangeSpeedDelay = Random.Range(ranIntervalTimeMin, ranIntervalTimeMax);

        yield return new WaitForSeconds(ranChangeSpeedDelay);

        if (isRanChangeSpeedInterval)
        {
            moveSpeed = RandomSpeed();
        }
        if (isRanDir)
        {
            platDirection = RandomDirection();
        }

        StartCoroutine(Delay());
    }

    float RandomSpeed()
    {
        ranSpeed = Random.Range(ranSpeedMin, ranSpeedMax);
        return ranSpeed;
    }

    Direction RandomDirection ()
    {
        ranDir = Random.Range(0, 2);
        if (ranDir == 0)
        {
            return Direction.Left;
        }
        else
        {
            return Direction.Right;
        }
    }

    private void OnEnable()
    {
        ResetObj();
    }

    void Update ()
    {
        if(shouldMove)
        {
            MovePlayer();
        }
    }

    void MovePlayer ()
    {
        if (platDirection == Direction.Left)
        {
            MovePlayerLeft();
        }
        else
        {
            MovePlayerRight();
        }
    }

    void MovePlayerLeft()
    {
        if (transform.position.x >= minClamp)
        {
            transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            platDirection = Direction.Right;
        }
    }

    void MovePlayerRight ()
    {
        if (transform.position.x <= maxClamp)
        {
            transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        }
        else
        {
            platDirection = Direction.Left;
        }
    }

    public void ResetObj()
    {
        shouldMove = true;
        if (isStartWithRanSpeeds)
        {
            moveSpeed = RandomSpeed();
        }
    }

    public void PlayerCollided ()
    {
        if(id == (PlatformSpawner.Instance.AmountOfPlatformPerSet - 1))
        {
            OnReachedLastPlat(this.gameObject);
        }
        ClosedSprite.enabled = false;
        idCounter++;
        shouldMove = false;
        StopAllCoroutines();
        breakPart.gameObject.SetActive(true);
        cover.GetComponent<SpriteRenderer>().enabled = false;
        runeRenderer.GetComponent<SpriteRenderer>().enabled = false;
        //GetComponentInParent<Platform>().enabled = false;
        GetComponentInParent<Animator>().enabled = false;
        transform.eulerAngles = Vector3.zero;
        _AudioSource.PlayOneShot(breakSFX);
        if(isFadePlateType)
        {
            platformPartsSprites.SetActive(true);
            fadePlatform.enabled = false;
            
            foreach (var sprite in fadePlatform.sprites)
            {
                sprite.color = new Color(1, 1, 1, 1);
            }
        }

        //If Fade Rune
        StartFadeingNextPlatform();
    }

    private void StartFadeingNextPlatform()
    {
        if (id == (PlatformSpawner.Instance.AmountOfPlatformPerSet - 1))
        {
            return;
        }

        Platform nextPlat = PlatformSpawner.Instance.PlatformInstances[id + 1].GetComponent<Platform>();

        if (nextPlat.platformType == PlatformType.Fade || nextPlat.platformType == PlatformType.Wild)
        {
            print(PlatformSpawner.Instance.PlatformInstances[id + 1].name);
            StartCoroutine(nextPlat.FadeIterval());
        }
    }

    public IEnumerator PlayerLeftPlat ()
    {
        float timer = Time.deltaTime;
        breakPart.GetComponent<Animator>().SetTrigger("ClosePlat");
        float animTime = breakPart.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        animTime -= Time.deltaTime * 8;
        while (timer <= animTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        //Finished Closing Anim
        fadePlatform.enabled = true;
        isFadePlateType = false;
        platformPartsSprites.SetActive(false);
        ClosedSprite.enabled = true;
        breakPart.gameObject.SetActive(false);
        cover.GetComponent<SpriteRenderer>().enabled = true;
        fadePlatform.StartFadeIn(0);
    }

    public void PlayerLeftPlatform ()
    {
        StartCoroutine(PlayerLeftPlat());
    }

    public void ChangeRuneSprite (Sprite sprite)
    {
        runeRenderer.sprite = sprite;
        //print(name + " Has changed Sprites to " + sprite.name);
    }
}
