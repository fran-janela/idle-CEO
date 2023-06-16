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

    public TextMeshProUGUI progrssText;

    public void SceneLoader()
    {
        Debug.Log(PlayerPrefs.GetInt("Tutorial") );
        if (PlayerPrefs.GetInt("Tutorial") == 0)
        {
            Debug.Log("ENTREIIIIIIII AQUIIII");
            PlayerPrefs.SetInt("Tutorial", 1);
            StartCoroutine(LoadAsynchronously(1));
            // UnityEngine.SceneManagement.SceneManager.LoadScene("Tutorial");
        }
        else
        {
            SceneManager.LoadScene(2);
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
