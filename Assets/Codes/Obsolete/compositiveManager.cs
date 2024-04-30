using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class compositiveManager : MonoBehaviour
{
    public static compositiveManager instanc;
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp
    }

    public GameState currentState;
    public GameState previousState;

    [Header("UI")] 
    public GameObject pauseScreen;
    public GameObject resultsScreen;
    public GameObject LevelUpScreen;
    [Header("Current Stat Displays")]
    public TMP_Text currentHealthDisplay;
    public TMP_Text currentRecoveryDisplay;
    public TMP_Text currentMoveSpeedDisplay;
    public TMP_Text currentMightDisplay;
    public TMP_Text currentProjectileSpeedDisplay;
    public TMP_Text currentMagnetDisplay;

    [Header("Result Screen Displays")] 
    public Image chosenCharacterImage;
    public Text chosenCharacterName;
    public Text levelReachedDisplay;
    
    public bool choosingUpgrade;

    public bool isGameOver = false;

    public GameObject playerObject;
    private void Awake()
    {
        instanc = GetComponent<compositiveManager>();
        isGameOver = false;
        DisableScreens();
        Time.timeScale = 1f;
    }

    void DisableScreens()
    {
        resultsScreen.SetActive(false);
        pauseScreen.SetActive(false);
        LevelUpScreen.SetActive(false);
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;
            
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Debug.Log("GAME IS OVER");
                    DisplayResults();
                    Time.timeScale = 0f;
                }
                break;
            
            case GameState.LevelUp:
                if (!choosingUpgrade)
                {
                    choosingUpgrade = true;
                    Time.timeScale = 0f;
                    Debug.Log("LevelUp");
                    LevelUpScreen.SetActive(true);
                }
                break;
            default:
                    Debug.LogWarning("State dose not exist");
                    break;
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
    void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState; 
            ChangeState(GameState.Paused);
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        Debug.Log("Game is Paused");
        }
            
    }
    
    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("Game is resumed");
        }
    }

    void CheckForPauseAndResume()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterData chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }
    

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp()
    {
        choosingUpgrade = false;
        Time.timeScale = 1f;
        LevelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
    
}
