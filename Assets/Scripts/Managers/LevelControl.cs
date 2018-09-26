using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class LevelControl : MonoBehaviour {

    [SerializeField] GameObject winScreen;

    [SerializeField] TextMeshProUGUI deathText;

    ScoreManager scoreMan;

    public static LevelControl instance = null;
    int sceneIndex, levelPassed;

    public bool levelEnd;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        if(GameObject.Find("ScoreManager"))
            scoreMan = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();

        //TextMeshPro deathText = GetComponent<TextMeshPro>();

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        
	}

    public void WinScreen()
    {
        if (sceneIndex == 9)
        {
            levelEnd = true;
            deathText.text = "" + scoreMan.levelDeaths;
            winScreen.SetActive(true);
        }
        else
        {
            if (levelPassed < sceneIndex)
                PlayerPrefs.SetInt("LevelPassed", sceneIndex);

            levelEnd = true;
            deathText.text = "" + scoreMan.levelDeaths;
            winScreen.SetActive(true);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
