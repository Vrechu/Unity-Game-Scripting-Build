using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{   
    public static ManageScenes GetManageScenes()
    {
        return ManageScenesSingleton;
    }
    static ManageScenes ManageScenesSingleton = null;

    public enum SceneType
    {
        INGAME, MENU
    }
    private SceneType _sceneType;
    public SceneType GetSceneType()
    {
        return _sceneType;
    }

    [SerializeField] private string _startingSceneName = "SampleScene";
    [SerializeField] private string _loseSceneName = "LoseScene";

    public event Action OnSceneLoad;
    public static event Action OnGameStart;

    //---------------------- METHODS ------------------------------- 

    private void Awake()
    {
        if (ManageScenesSingleton == null) ManageScenesSingleton = this;
        DoorInteraction.OnDoorInteract += LoadScene;
        ManageHealth.OnPlayerDeath += ToLoseScreen;
        SceneManager.sceneLoaded += OnSceneLoadEvent;
    }

    private void OnDestroy()
    {
        DoorInteraction.OnDoorInteract -= LoadScene;
        ManageHealth.OnPlayerDeath -= ToLoseScreen;
        SceneManager.sceneLoaded -= OnSceneLoadEvent;
    }

    private void Start()
    {
        OnSceneLoad?.Invoke();
    }

    private void Update()
    {
        BackToMenu();
    }

    /// <summary>
    /// loads the specified scene
    /// </summary>
    /// <param name="sceneName">scene to be loaded</param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        if (sceneName == _startingSceneName) OnGameStart?.Invoke();
    }

    /// <summary>
    /// checks wether the scene has a player and sets the scene type accordingly
    /// </summary>
    private void CheckSceneType()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)  _sceneType = SceneType.MENU;        
        else _sceneType = SceneType.INGAME;
    }

    private void ToLoseScreen()
    {
        LoadScene(_loseSceneName);
    }

    private void OnSceneLoadEvent(Scene scene, LoadSceneMode loadMode)
    {
        CheckSceneType();
        OnSceneLoad?.Invoke();
        Debug.Log("Scene loaded: " + scene.name + " , Type: " + GetSceneType());
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    private void BackToMenu()
    {
        if (SceneManager.GetActiveScene().name != "MenuScene" &&  Input.GetAxis("Cancel") == 1)
        {
            LoadScene("MenuScene");
        }
    }
}
