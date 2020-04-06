using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    public delegate void PlayerControllerEventHandler();

    public static event PlayerControllerEventHandler PlayerStartMoveing;
    public static event PlayerControllerEventHandler PlayerStopMoveing;
    public static event PlayerControllerEventHandler PlayerReachedPlat;
    public static event PlayerControllerEventHandler PlayerReachedStartPlat;
    public static event PlayerControllerEventHandler PlayerLeftedPlat;
    public static event PlayerControllerEventHandler PlayerLeftedStartPlat;
    public static event PlayerControllerEventHandler PlayerDead;

    [Header("Player Attributes")]
    public float moveSpeed = 8;
    public float startOffSet = 5;
    public bool shouldMove;
    //For Centering Player 
    public float smoothSpeed;
    public bool centerPlayer;
    public bool isDead;
    public bool canMove = true;

    [Header("Init Data")]
    public static Transform initPos;
    public bool testMode;
    public GameObject hitParticle;
    private GameObject hitParticleInstance;

    [Header("Audio Settings")]
    public AudioClip dieSFX;
    public AudioClip moveSFX;
    public AudioClip reachedSFX;
    public float volume;

    private static bool isOnStartPlat = false;

    #region Player Events

    public static void OnPlayerStartMoveing()
    {
        if (PlayerStartMoveing != null)
        {
            PlayerStartMoveing();
        }
    }

    public static void OnPlayerStopMoveing()
    {
        if (PlayerStopMoveing != null)
        {
            PlayerStopMoveing();
        }
    }

    public static void OnPlayerReachedPlat()
    {
        if (PlayerReachedPlat != null)
        {
            PlayerReachedPlat();
        }
    }

    public static void OnPlayerReachedStartPlat()
    {
        isOnStartPlat = true;
        if (PlayerReachedStartPlat != null)
        {
            PlayerReachedStartPlat();
        }
    }

    public static void OnPlayerLeftedPlat()
    {
        if (PlayerLeftedPlat != null)
        {
            PlayerLeftedPlat();
        }
    }

    public static void OnPlayerLeftedStartPlat()
    {
        if (PlayerLeftedStartPlat != null)
        {
            PlayerLeftedStartPlat();
        }
    }

    public static void OnPlayerDead()
    {
        if (PlayerDead != null)
        {
            PlayerDead();
        }
    }

    #endregion Player Events

    private void Awake()
    {
        InitSubEvents();
        //OPTIMIZE
        if (initPos == null)
        {
            if (GameObject.FindGameObjectWithTag("StartPlatform"))
            {
                initPos = GameObject.FindGameObjectWithTag("StartPlatform").transform;
            }
        }
        if (initPos)
        {
            transform.position = initPos.position - new Vector3(0, startOffSet, 0);
        }
    }

    void InitSubEvents ()
    {
        GameManager.LeftArea += GameManager_LeftArea;
    }

    private void GameManager_LeftArea()
    {
        MoveToCenter();
    }
    
    private void OnDestroy()
    {
        GameManager.LeftArea -= GameManager_LeftArea;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlatformTrigger")
        {
            if (!testMode)
            {
                Platform plat = collision.GetComponentInParent<Platform>();
                plat.PlayerLeftPlatform();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
        {
            return;
        }

        if (collision.gameObject.tag == "Wall")
        {
            if (!testMode)
            {
                Death();
                return;
            }
        }

        if (isDead)
        {
            return;
        }

        if (collision.gameObject.tag == "PlatformTrigger")
        {
            if (!testMode)
            {
                ReachedPlatSucc(collision.gameObject, false);
            }
        }
        if (collision.gameObject.tag == "StartPlatformTrigger")
        {
            ReachedPlatSucc(collision.gameObject, true);
        }
    }

    public void ReachedPlatSucc (GameObject platform, bool isStartPlatform)
    {
        PlayerSFXController.Instance.PlaySFX(reachedSFX);
        if (isStartPlatform == false)
        {
            if (platform.GetComponentInParent<Platform>())
            {
                //Just in case
                if (isDead == false)
                {
                    shouldMove = false;
                    platform.GetComponentInParent<Platform>().PlayerCollided();
                    transform.position = platform.GetComponent<PlatformTrigger>().playerSitPos.transform.position;
                    OnPlayerReachedPlat();
                }
            }
        }
        else
        {
            shouldMove = false;
            transform.position = platform.GetComponent<PlatformTrigger>().playerSitPos.transform.position;
            OnPlayerReachedStartPlat();
        }
    }

    private void Update()
    {
        if (canMove == false)
        {
            return;
        }
        if(centerPlayer)
        {
            CenterPlayer();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(PopUpViewController.PopUpExistInGame)
            {
                return;
            }
            if (EventSystem.current.currentSelectedGameObject)
            {
                return;
            }

            if (shouldMove == false)
            {
                PlayerSFXController.Instance.PlaySFX(moveSFX);
                shouldMove = true;
                OnPlayerStartMoveing();
            }
        }
        if(shouldMove)
        {
            if(isOnStartPlat)
            {
                OnPlayerLeftedStartPlat();
                isOnStartPlat = false;
            }
            MovePlayer();
        }
    }

    void CenterPlayer ()
    {
        if (transform.position.x <= 0 && transform.position.x >= 0)
        {
            centerPlayer = false;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, transform.position.y), smoothSpeed * Time.deltaTime);
        }
    }


    void MovePlayer()
    {
        Vector2 moveDir = new Vector3(0, moveSpeed * Time.deltaTime);
        transform.Translate(moveDir);
    }

    public void Death ()
    {
        isDead = true;
        PlayerSFXController.Instance.PlaySFX(dieSFX);

        StartCoroutine(DeathEffect( () =>
        {
            OnPlayerDead();
        }));
    }

    IEnumerator DeathEffect (System.Action OnComplete)
    {
        hitParticleInstance = Instantiate(hitParticle, transform.position, Quaternion.identity) as GameObject;
        canMove = false;
        GameObject graphics = GetComponent<PlayerGraphicsController>().graphics;
        for (int i = 0; i < graphics.transform.childCount; i++)
        {
            graphics.transform.GetChild(i).gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        OnComplete();
    }

    public void MoveToCenter ()
    {
        centerPlayer = true;
    }
}
