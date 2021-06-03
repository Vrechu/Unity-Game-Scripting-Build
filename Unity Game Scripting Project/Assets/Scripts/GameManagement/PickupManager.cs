using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public float StartingScore = 0;
    public float CurrentScore;
    private void Awake()
    {
        PickupInteract.OnPointsPickup += AddScore;
    }

    private void OnDestroy()
    {
        PickupInteract.OnPointsPickup -= AddScore;
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentScore = StartingScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddScore(float scoreToAdd)
    {
        CurrentScore += scoreToAdd;
    }
}
