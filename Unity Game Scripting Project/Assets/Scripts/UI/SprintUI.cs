using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;


public class SprintUI : MonoBehaviour
{
    [SerializeField] private GameObject SprintUISprite;
    private PickupManager _pickupManager;
    private UnityEngine.UI.Image _sprintImage;

    private void Start()
    {
        _pickupManager = PickupManager.GetPickupManager();
        _sprintImage = GetComponentInChildren<UnityEngine.UI.Image>();
        _sprintImage.gameObject.SetActive(false);
    }

     private void Update()
    {
        if (_pickupManager.CanSprint())
        {
            _sprintImage.gameObject.SetActive(true);
        }
    }

}