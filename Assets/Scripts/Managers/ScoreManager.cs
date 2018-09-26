using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class ScoreManager : MonoBehaviour {

    public int levelDeaths;
    public int totalDeaths;

    public float levelTime;
    public float totalTime;

    [SerializeField] TextMeshProUGUI timeText;
    private float startTime;
    private bool finished = false;

    private void Start()
    {
        //-------------Death------------\\
        totalDeaths = PlayerPrefs.GetInt("TotalDeaths");

        levelDeaths = 0;

        //-------------Timer------------\\
        startTime = Time.time;

        totalTime = PlayerPrefs.GetFloat("Time");
    }

    private void Update()
    {
        Timer();
    }

    public void AddDeaths ()
    {
        levelDeaths++;
        totalDeaths++;

        PlayerPrefs.SetInt("TotalDeaths", totalDeaths);
    }

    public void Timer()
    {
        if (finished)
            return;

        levelTime += 1 * Time.deltaTime;
    }

    public void Finish()
    {
        finished = true;

        totalTime += levelTime;
        totalTime = Mathf.Round(totalTime * 100f) / 100f;
        levelTime = Mathf.Round(levelTime * 100f) / 100f;

        PlayerPrefs.SetFloat("Time", totalTime);

        timeText.text = "" + levelTime;
        Debug.Log(totalTime);
    }
}
