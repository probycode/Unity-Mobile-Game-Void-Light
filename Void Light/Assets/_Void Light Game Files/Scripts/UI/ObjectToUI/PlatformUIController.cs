using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformUIController : MonoBehaviour {

    public GameObject highStreakUIPrefab;
    public GameObject highStreakInstance;

    public void CreateHighStreakUI ()
    {
        highStreakInstance = Instantiate(highStreakUIPrefab)as GameObject;
        highStreakInstance.transform.SetParent(transform, false);
        highStreakInstance.GetComponentInChildren<Text>().text = "Highest" + "\n" + "Streak" + "\n" + ScoreManager.HighScore.ToString();
        GetComponent<Platform>().isHighScorePlat = true;
    }
}
