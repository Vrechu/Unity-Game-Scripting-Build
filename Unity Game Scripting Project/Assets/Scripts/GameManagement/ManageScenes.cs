using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageScenes : MonoBehaviour
{

    private void Awake()
    {
        DoorInteraction.OnDoorInteract += LoadScene;
    }

    private void OnDestroy()
    {
        DoorInteraction.OnDoorInteract -= LoadScene;
    }

    /// <summary>
    /// loads the specified scene
    /// </summary>
    /// <param name="sceneName">scene to be loaded</param>
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
