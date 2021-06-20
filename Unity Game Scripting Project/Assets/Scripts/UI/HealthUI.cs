using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Slider _slider;

    private float _maxHealth;

    private void Start()
    {
        _slider = FindObjectOfType<Slider>();
        _maxHealth = ManageHealth.GetManageHealth().GetStartingHealth();
        _slider.maxValue = _maxHealth;
    }

    private void Update()
    {
        _slider.value = ManageHealth.GetManageHealth().GetCurrentHealth();
    }
}
