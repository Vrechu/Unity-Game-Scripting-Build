using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageHealth : MonoBehaviour
{

    [SerializeField] private float _startingHealth = 10;
    [SerializeField] private float _currentHealth;

    private void Awake()
    {
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
        Debug.Log("ouch! " + _currentHealth);
    }

}
