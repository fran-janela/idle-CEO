using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TutorialLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject loadingScreen;
    public Slider slider;

    public Canvas canvasMainGame;

    public Canvas canvasTutorial;

    public TextMeshProUGUI progrssText;

    public void Start(){
        canvasMainGame.enabled = false;
    }

    public void SceneLoader()
    {
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            StartCoroutine(LoadAsynchronously(1));
            // UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
        }
        else
        {
            canvasTutorial.enabled = false;
            canvasMainGame.enabled = true;

            // UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }


    IEnumerator LoadAsynchronously(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            progrssText.text = progress * 100f + "%";
            Debug.Log(progress);
            yield return null;
        }
    }


}