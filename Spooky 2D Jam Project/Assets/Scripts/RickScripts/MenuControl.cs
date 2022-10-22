using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public enum GameState {MenuState,LearnState,CreditState}
    public GameState currentGameState;

    public GameObject menu;
    public GameObject learn;
    public GameObject credit;

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = GameState.MenuState;
        ShowScreen(menu);
    }

    public void MainMenu()
    {
        currentGameState = GameState.MenuState;
        ShowScreen(menu);
    }

    public void InstructionsScreen()
    {
        currentGameState = GameState.LearnState;
        ShowScreen(learn);
    }

    public void CreditsScreen()
    {
        currentGameState = GameState.CreditState;
        ShowScreen(credit);
    }

    // used to determine which screen to show based on the current game state
    private void ShowScreen(GameObject gameObjectToShow)
    {
        menu.SetActive(false);
        learn.SetActive(false);
        credit.SetActive(false);

        gameObjectToShow.SetActive(true);
    }
}
