using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageHealth : MonoBehaviour
{
    [SerializeField] private float _startingHealth = 10;

    public float GetStartingHealth()
    {
        return _startingHealth;
    }

    [SerializeField] private float _currentHealth;
    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public static ManageHealth GetManageHealth()
    {
        return ManageHealthSingleton; 
    }
    static ManageHealth ManageHealthSingleton = null;

    private void Awake()
    {
        if (ManageHealthSingleton == null) ManageHealthSingleton = this;

        BulletImpact.OnPlayerHit += TakeDamage;
    }

    private void OnDestroy()
    {
        BulletImpact.OnPlayerHit -= TakeDamage;
    }

    private void Start()
    {
        _currentHealth = _startingHealth;
    }

    private void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }

}
