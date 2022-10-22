using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseControl : MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene("MainScene");
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
