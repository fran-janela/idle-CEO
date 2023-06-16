using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialScreens : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tutorialScreen1;

    public GameObject tutorialScreen2;

    public GameObject tutorialScreen3;

    public GameObject tutorialScreen4;

    public GameObject tutorialScreen5;

    public GameObject tutorialScreen6;

    public GameObject tutorialScreen7;
    public GameObject tutorialScreen8;

    public GameObject tutorialScreen9;
    void Start()
    {
        tutorialScreen1.SetActive(true);
        tutorialScreen2.SetActive(false);
        tutorialScreen3.SetActive(false);
        tutorialScreen4.SetActive(false);
        tutorialScreen5.SetActive(false);
        tutorialScreen6.SetActive(false);
        tutorialScreen7.SetActive(false);
        tutorialScreen8.SetActive(false);
        tutorialScreen9.SetActive(false);
    }

    // Update is called once per frame
    public void nextScreen(){
        if(tutorialScreen1.activeSelf){
            tutorialScreen1.SetActive(false);
            tutorialScreen2.SetActive(true);
        }
        else if(tutorialScreen2.activeSelf){
            tutorialScreen2.SetActive(false);
            tutorialScreen3.SetActive(true);
        }
        else if(tutorialScreen3.activeSelf){
            tutorialScreen3.SetActive(false);
            tutorialScreen4.SetActive(true);
        }
        else if(tutorialScreen4.activeSelf){
            tutorialScreen4.SetActive(false);
            tutorialScreen5.SetActive(true);
        }
        else if(tutorialScreen5.activeSelf){
            tutorialScreen5.SetActive(false);
            tutorialScreen6.SetActive(true);
        }
        else if(tutorialScreen6.activeSelf){
            tutorialScreen6.SetActive(false);
            tutorialScreen7.SetActive(true);
        }
        else if(tutorialScreen7.activeSelf){
            tutorialScreen7.SetActive(false);
            tutorialScreen8.SetActive(true);
        }
        else if(tutorialScreen8.activeSelf){
            tutorialScreen8.SetActive(false);
            tutorialScreen9.SetActive(true);
        }
        else if(tutorialScreen9.activeSelf){
            tutorialScreen9.SetActive(true);
            SceneManager.LoadScene(2);
            // UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }

    public void backScreen(){
        if(tutorialScreen1.activeSelf){
            tutorialScreen1.SetActive(true);
            // UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
        else if(tutorialScreen2.activeSelf){
            tutorialScreen2.SetActive(false);
            tutorialScreen1.SetActive(true);
        }
        else if(tutorialScreen3.activeSelf){
            tutorialScreen3.SetActive(false);
            tutorialScreen2.SetActive(true);
        }
        else if(tutorialScreen4.activeSelf){
            tutorialScreen4.SetActive(false);
            tutorialScreen3.SetActive(true);
        }
        else if(tutorialScreen5.activeSelf){
            tutorialScreen5.SetActive(false);
            tutorialScreen4.SetActive(true);
        }
        else if(tutorialScreen6.activeSelf){
            tutorialScreen6.SetActive(false);
            tutorialScreen5.SetActive(true);
        }
        else if(tutorialScreen7.activeSelf){
            tutorialScreen7.SetActive(false);
            tutorialScreen6.SetActive(true);
        }
        else if(tutorialScreen8.activeSelf){
            tutorialScreen8.SetActive(false);
            tutorialScreen7.SetActive(true);
        }
        else if(tutorialScreen9.activeSelf){
            tutorialScreen9.SetActive(false);
            tutorialScreen8.SetActive(true);
        }
    }


    void Update()
    {
        
    }
}
