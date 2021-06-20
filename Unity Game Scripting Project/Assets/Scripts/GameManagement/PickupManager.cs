using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] private float _startingScore = 0;
    [SerializeField] private float _currentScore;

    private void Awake()
    {
        PickupInteract.OnPointsPickup += AddScore;
    }

    private void OnDestroy()
    {
        PickupInteract.OnPointsPickup -= AddScore;
    }

    private void Start()
    {
        _currentScore = _startingScore;
    }

    private void AddScore(float scoreToAdd)
    {
        _currentScore += scoreToAdd;
    }
}
