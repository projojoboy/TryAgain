using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour {

    public int level;
    public int levelPassed;

    [SerializeField] Slider slider;
    [SerializeField] GameObject loadingScreen, mainMenu, levelSelector;
    [SerializeField] Button level2Button, level3Button, level4Button, level5Button, level6Button, level7Button, level8Button;

	// Use this for initialization
	void Start () {
        levelPassed = PlayerPrefs.GetInt("LevelPassed");
        level2Button.interactable = false;
        level3Button.interactable = false;
        level4Button.interactable = false;
        level5Button.interactable = false;
        level6Button.interactable = false;
        level7Button.interactable = false;
        level8Button.interactable = false;

        switch (levelPassed)
        {
            case 1:
                level2Button.interactable = true;
                break;
            case 2:
                level2Button.interactable = true;
                level3Button.interactable = true;
                break;
            case 3:
                level2Button.interactable = true;
                level3Button.interactable = true;
                level4Button.interactable = true;
                break;
            case 4:
                level2Button.interactable = true;
                level3Button.interactable = true;
                level4Button.interactable = true;
                level5Button.interactable = true;
                break;
            case 5:
                level2Button.interactable = true;
                level3Button.interactable = true;
                level4Button.interactable = true;
                level5Button.interactable = true;
                level6Button.interactable = true;
                break;
            case 6:
                level2Button.interactable = true;
                level3Button.interactable = true;
                level4Button.interactable = true;
                level5Button.interactable = true;
                level6Button.interactable = true;
                level7Button.interactable = true;
                break;
            case 7:
                level2Button.interactable = true;
                level3Button.interactable = true;
                level4Button.interactable = true;
                level5Button.interactable = true;
                level6Button.interactable = true;
                level7Button.interactable = true;
                level8Button.interactable = true;
                break;
        }
    }
	
	public void LoadLevel(int level)
    {
        StartCoroutine(LoadAsync(level));
    }

    IEnumerator LoadAsync(int level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        mainMenu.SetActive(false);
        levelSelector.SetActive(false);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            GameObject.Find("ProgressText").GetComponent<TextMeshProUGUI>().SetText(progress * 98f + "%");

            yield return null;
        }
    }

    public void ResetLevels()
    {
        level2Button.interactable = false;
        level3Button.interactable = false;
        level4Button.interactable = false;
        level5Button.interactable = false;
        level6Button.interactable = false;
        level7Button.interactable = false;
        level8Button.interactable = false;
        PlayerPrefs.SetInt("LevelPassed", 0);
    }
}
