using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    LevelSelectManager lvlSlct;

    private void Start()
    {
        PlayerPrefs.SetInt("TotalDeaths", 0);
        PlayerPrefs.SetFloat("Time", 0);
        lvlSlct = GameObject.Find("Canvas").GetComponent<LevelSelectManager>();
    }

    public void PlayGame()
    {
        lvlSlct.LoadLevel(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
