using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    private ManageScenes _manageScenes;

    private void Start()
    {
        _manageScenes = GetComponent<ManageScenes>();
        _manageScenes.OnSceneLoad += SetMouseType; 
    }

    private void OnDestroy()
    {
        if (_manageScenes != null) _manageScenes.OnSceneLoad -= SetMouseType;
    }

    /// <summary>
    /// locks or unlocks the mouse depending on the scene type
    /// </summary>
    private void SetMouseType()
    {
        if (_manageScenes.GetSceneType() == ManageScenes.SceneType.MENU)
            Cursor.lockState = CursorLockMode.Confined;
        else if (_manageScenes.GetSceneType() == ManageScenes.SceneType.INGAME)
            Cursor.lockState = CursorLockMode.Locked;
    }

}
