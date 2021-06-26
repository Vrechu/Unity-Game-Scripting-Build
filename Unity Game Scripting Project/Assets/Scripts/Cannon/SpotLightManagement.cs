using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SpotLightManagement : MonoBehaviour
{
    [SerializeField] private Color _unspottedColor = Color.white;
    [SerializeField] private Color _spottedColor = Color.red;
    [SerializeField] private Color _searchingColor = Color.yellow;
    [SerializeField] private float _intensity = 20;

    private Light _spotLight;
    private SpotPlayer _spotPlayer;
    private SpotPlayer.CannonState _currentState;

    private void Awake()
    {
        _spotLight = GetComponentInChildren<Light>();
        _spotPlayer = GetComponent<SpotPlayer>();
    }

    private void Start()
    {
        _spotLight.intensity = _intensity;
        _spotLight.spotAngle = _spotPlayer.GetSearchLightAngle() * 2;
    }

    private void Update()
    {
        SwitchColor();
    }

    /// <summary>
    /// switches the spolight color depending on the cannon state
    /// </summary>
    private void SwitchColor()
    {
        if (_currentState != _spotPlayer.GetCannonState())
        {
            _currentState = _spotPlayer.GetCannonState();
            switch (_currentState)
            {
                case SpotPlayer.CannonState.SPOTTED:
                    {
                        _spotLight.color = _spottedColor;
                        break;
                    }
                case SpotPlayer.CannonState.LOSTVISUAL:
                    _spotLight.color = _searchingColor;
                    {
                        break;
                    }
                case SpotPlayer.CannonState.UNSPOTTED:
                    {
                        _spotLight.color = _unspottedColor;
                        break;
                    }
            }
        }
    }
}
