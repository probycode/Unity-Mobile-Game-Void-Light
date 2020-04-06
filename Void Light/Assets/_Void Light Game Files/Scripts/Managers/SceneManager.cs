using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {

    public static void RestartLevel()
    {
        GameManager.OnGameRestart();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
