using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SpotLightManagement : MonoBehaviour
{
    [SerializeField] private Color unspottedColor = Color.white;
    [SerializeField] private Color spottedColor = Color.red;
    [SerializeField] private Color searchingColor = Color.yellow;
    [SerializeField] private float intensity = 10;

    private Light _spotLight;
    private SpotPlayer _spotPlayer;

    private void Awake()
    {
        _spotLight = GetComponentInChildren<Light>();
        _spotPlayer = GetComponent<SpotPlayer>();
        _spotPlayer.OnPlayerSpotted += SetToSpottedLight;
        _spotPlayer.OnLostVisualOnPLayer += SetToLostVisualLight;
        _spotPlayer.OnCannonReset += SetToUnspottedLight;            
    }

    private void OnDestroy()
    {        
        _spotPlayer.OnPlayerSpotted -= SetToSpottedLight;
        _spotPlayer.OnLostVisualOnPLayer -= SetToLostVisualLight;
        _spotPlayer.OnCannonReset -= SetToUnspottedLight;
    }

    private void Start()
    {        
        _spotLight.intensity = 20;
        _spotLight.spotAngle = _spotPlayer.GetSearchLightAngle() * 2;
    }

    private void SetToSpottedLight()
    {
        _spotLight.color = spottedColor;
    }

    private void SetToLostVisualLight()
    {
            _spotLight.color = searchingColor;
    }

    private void SetToUnspottedLight()
    {
            _spotLight.color = unspottedColor;
    }
}
