using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
   
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
