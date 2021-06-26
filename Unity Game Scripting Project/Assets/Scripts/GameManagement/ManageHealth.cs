using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageHealth : MonoBehaviour
{
    [SerializeField] private float _startingHealth = 10;
    public static event Action OnPlayerDeath;

    //---------------------- GETTERS -------------------------------

    public static ManageHealth GetManageHealth()
    {
        return ManageHealthSingleton; 
    }
    static ManageHealth ManageHealthSingleton = null;

    public float GetStartingHealth()
    {
        return _startingHealth;
    }

    [SerializeField] private float _currentHealth;
    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    //---------------------- METHODS ------------------------------- 


    private void Awake()
    {
        if (ManageHealthSingleton == null) ManageHealthSingleton = this;

        BulletImpact.OnPlayerHit += TakeDamage;
        ManageScenes.OnGameStart += ResetHealth;
    }

    private void OnDestroy()
    {
        BulletImpact.OnPlayerHit -= TakeDamage;
        ManageScenes.OnGameStart -= ResetHealth;
    }

    private void Start()
    {
        ResetHealth();
    }

    private void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth < 0) OnPlayerDeath?.Invoke();
    }

    private void ResetHealth()
    {
        _currentHealth = _startingHealth;
    }
}
