using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    void Start(){
         Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Current scene name: " + currentScene.name);

        if (currentScene.name == "WelcomeScene" || currentScene.name == "InstructionScene")
        {
            GameObject.Find("MainBackgroundMusic").GetComponent<AudioSource>().Play();
        }

    }

    public void PlayButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void InstructionButton()
    {
        SceneManager.LoadScene("InstructionScene");
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("WelcomeScene");
    }
}
