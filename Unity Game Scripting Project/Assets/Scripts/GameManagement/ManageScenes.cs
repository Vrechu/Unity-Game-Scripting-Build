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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
