using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//Dont add to list from middle b/c it will update the scene thus
//lose connections so add to the bottom of the list only 
public enum View
{
    Title,
    Options,
    InGame,
    CodexSelect,
    Codex,
    CustomizePlayer,
    ActionsMenu,
    ColorUnlock,
}

[RequireComponent(typeof(AudioSource))]
public class ViewController : MonoBehaviour {

    public delegate void ViewUIChangedEventHandler(View view);
    public delegate void InitiatingViewChangeEventHandler(View destiniationView);
    public delegate void UIUpdateEventHandler();

    public static event ViewUIChangedEventHandler ViewChanged;
    public static event InitiatingViewChangeEventHandler InitiatingViewChange;
    public static event UIUpdateEventHandler UIUpdate;

    public static ViewController Instance;

    //[Header("View System Inits")]
    public GameObject PopUpPrefab;
    public View startView;
    public static GameObject ViewsGO;
    public static GameObject[] ViewList;
    public static View currentView;
    public static ViewUI currentViewUI;
    public static int menuViewIndex;

    [Header("Audio for all buttons")] 
    //Audio for all buttons unless overided by button
    public AudioClip buttonSFX;
    public static AudioSource audioSource;
    public static AudioClip _buttonSFX;
    public static AudioClip clipToPlay;

    public static void OnInitiatingViewChange(View destiniationView)
    {
        if (InitiatingViewChange != null)
        {
            InitiatingViewChange(destiniationView);
        }
    }

    public static void OnUIUpdate()
    {
        if (UIUpdate != null)
        {
            UIUpdate();
        }
    }

    public static void OnViewChanged(View view)
    {
        if (ViewChanged != null)
        {
            ViewChanged(view);
        }
    }

    private void Awake()
    {
        Instance = this;

        _buttonSFX = buttonSFX;
        clipToPlay = _buttonSFX;

        ViewsGO = this.gameObject;
        audioSource = GetComponent<AudioSource>();

        CreateViewList();
        InitSubEvents();
    }

    private void LoadViewFirst ()
    {
        ShowView(View.CustomizePlayer);
    }

    private void InitSubEvents()
    {
        GameManager.GameLoaded += GameManager_GameLoaded;
        GameManager.GameStart += GameManager_GameStart;
    }

    private void GameManager_GameStart()
    {
        
    }

    private void GameManager_GameLoaded()
    {
        //Run Mothed cus of lazy errer dont use this in any other gamei create
        LoadViewFirst();
        currentViewUI = ShowView(startView);
    }

    private void CreateViewList()
    {
        ViewList = new GameObject[ViewsGO.transform.childCount];

        for (int i = 0; i < ViewsGO.transform.childCount; i++)
        {
            /*
            if (Views.transform.GetChild(i).GetComponents<ViewUI>() == null)
            {
                Debug.LogError("There is a non ViewUI in the view list on the gameobject: " + this.name);
                return;
            }
            */

            ViewList[i] = ViewsGO.transform.GetChild(i).gameObject;
        }
    }

    /// <summary>
    /// Shows desired view
    /// </summary>
    public static ViewUI ShowView(View view)
    {
        CheckIfViewExists(view);
        OnInitiatingViewChange(view);

        for (int i = 0; i < ViewList.Length; i++)
        {
            ViewUI viewUI = ViewList[i].gameObject.GetComponent<ViewUI>();
            CloseViewsExceptFor(viewUI, view);

            if(viewUI.view == view)
            {
                currentViewUI = viewUI;
                currentView = currentViewUI.view;
            }

            if(i == (ViewList.Length - 1))
            {
                //currentView = view;
                OnViewChanged(view);
                OnUIUpdate();
                return currentViewUI;
            }
        }
        return null;
    }

    /// <summary>
    /// Checks if the desired view exists in the scene
    /// </summary>
    private static bool CheckIfViewExists (View view)
    {
        bool viewExist = false;
        for (int i = 0; i < ViewList.Length; i++)
        {
            ViewUI viewUI = ViewList[i].gameObject.GetComponent<ViewUI>();
            if (viewUI.view == view)
            {
                viewExist = true;
                break;
            }
        }

        if (viewExist == false)
        {
            Debug.LogError("View " + view.ToString() + " DOES NOT exist in scene");
            return false;
        }
        else
        {
            //Debug.Log("View " + view.ToString() + " DOES exist in scene");
            return true;
        }
    }
    
    private static void CloseViewsExceptFor(ViewUI viewUI, View viewToOpen)
    {
        if (viewUI.view == viewToOpen)
        {
            viewUI.Open();
        }
        else
        {
            viewUI.Close();
        }
    }

    public static void ShowRuneCodex(CodexData codexData)
    {
        CodexUIController runeCodexInstance = ShowView(View.Codex) as CodexUIController;
        runeCodexInstance.CodexData = codexData;
    }

    public static void PlaySound(AudioClip clip, bool overrideDefault, float volume)
    {
        if (overrideDefault)
        {
            clipToPlay = clip;
        }
        else
        {
            clipToPlay = _buttonSFX;
        }

        if (clipToPlay != null)
        {
            audioSource.PlayOneShot(clipToPlay, volume);
        }
        else
        {
            print("AudioClip is missing");
        }
    }

    private void OnDestroy()
    {
        GameManager.GameLoaded -= GameManager_GameLoaded;
        GameManager.GameStart -= GameManager_GameStart;
    }
}
