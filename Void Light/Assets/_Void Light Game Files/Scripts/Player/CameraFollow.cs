using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow Instance;
    public Transform target;
    public float followSpeed;
    public GameObject camTitleScreenPivot;

    private Vector3 initPosCorrected;

    private void Awake()
    {
        Instance = this;
    }

    void InitSubEvents ()
    {
        PlayerController.PlayerDead += GameManager_PlayerDead;
        GameManager.GameStart += GameManager_GameStart;
        PlayerController.PlayerStartMoveing += PlayerController_PlayerStartMoveing;
        ViewController.ViewChanged += ViewController_ViewChanged;
    }

    private void OnDestroy()
    {
        PlayerController.PlayerDead -= GameManager_PlayerDead;
        GameManager.GameStart -= GameManager_GameStart;
        PlayerController.PlayerStartMoveing -= PlayerController_PlayerStartMoveing;
        ViewController.ViewChanged -= ViewController_ViewChanged;
    }

    private void ViewController_ViewChanged(View view)
    {
        if (view == View.Title)
        {
            target = camTitleScreenPivot.transform;
        }
    }

    private void PlayerController_PlayerStartMoveing()
    {
        CameraFollow.Instance.followSpeed = 1;
    }

    private void GameManager_GameStart()
    {
        target = GameManager.playerGO.transform;
    }

    private void GameManager_PlayerDead()
    {
        followSpeed = 2;
        target = GameManager.playerGO.transform;
    }

    void Start ()
    {
        InitSubEvents();
        initPosCorrected = transform.position;
    }
	
	void LateUpdate()
    {
        if (target)
        {
            FollowTarget();
        }
    }

    void FollowTarget()
    {
        Vector3 newPos =  Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
        transform.position = newPos;
        transform.position = new Vector3(newPos.x, newPos.y, initPosCorrected.z);
    }
}
