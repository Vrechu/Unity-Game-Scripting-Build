using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{   
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
    [SerializeField] private string _menuSceneName = "MenuScene";

    public event Action OnSceneLoad;
    public static event Action OnGameStart;

    //---------------------- METHODS ------------------------------- 

    private void Awake()
    {        
        DoorInteraction.OnDoorInteract += LoadScene;
        ManageHealth.OnPlayerDeath += BackToMenu;
        SceneManager.sceneLoaded += OnSceneLoadEvent;
    }

    private void OnDestroy()
    {
        DoorInteraction.OnDoorInteract -= LoadScene;
        ManageHealth.OnPlayerDeath -= BackToMenu;
        SceneManager.sceneLoaded -= OnSceneLoadEvent;
    }

    private void Start()
    {
        OnSceneLoad?.Invoke();
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

    private void BackToMenu()
    {
        LoadScene(_menuSceneName);
    }

    private void OnSceneLoadEvent(Scene scene, LoadSceneMode loadMode)
    {
        CheckSceneType();
        OnSceneLoad?.Invoke();
        Debug.Log("scene loaded: " + GetSceneType());
    }
}
