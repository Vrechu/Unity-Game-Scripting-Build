using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheats : MonoBehaviour
{
    [SerializeField] private string _nextSceneToBeLoaded;
    private ManageScenes _manageScenes;
    private PickupManager _pickupManager;
    void Start()
    {
        _manageScenes = ManageScenes.GetManageScenes();
        _pickupManager = PickupManager.GetPickupManager();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) _manageScenes.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.N)) _manageScenes.LoadScene(_nextSceneToBeLoaded);
        if (Input.GetKeyDown(KeyCode.G)) _pickupManager.PickupBoots();
    }
}
