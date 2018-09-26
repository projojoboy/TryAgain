using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class EndGameManager : MonoBehaviour {

    [SerializeField] TextMeshProUGUI totalDeathsText;
    [SerializeField] TextMeshProUGUI totalTimeText;

    private int deaths;
    private float time;

    // Use this for initialization
    void Start () {
        time = PlayerPrefs.GetFloat("Time");
        deaths = PlayerPrefs.GetInt("TotalDeaths");

        totalDeathsText.text = "Total Deaths: " + deaths;
        totalTimeText.text = "Total time: " + time;
	}

    public void Restart()
    {
        PlayerPrefs.SetInt("TotalDeaths", 0);
        PlayerPrefs.SetFloat("Time", 0);
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
