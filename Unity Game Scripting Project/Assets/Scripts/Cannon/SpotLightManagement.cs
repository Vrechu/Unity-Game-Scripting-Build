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

    [SerializeField] private Light spotLight;
    [SerializeField] private SpotPlayer spotPlayer;

    private void Awake()
    {
        SpotPlayer.OnPlayerSpotted += SetToSpottedLight;
        SpotPlayer.OnLostVisualOnPLayer += SetToLostVisualLight;
        SpotPlayer.OnCannonReset += SetToUnspottedLight;            
    }

    private void OnDestroy()
    {
        SpotPlayer.OnPlayerSpotted -= SetToSpottedLight;
        SpotPlayer.OnLostVisualOnPLayer -= SetToLostVisualLight;
        SpotPlayer.OnCannonReset -= SetToUnspottedLight;
    }

    private void Start()
    { 
        spotLight.intensity = 20;
        spotLight.spotAngle = spotPlayer.GetSearchLightAngle() * 2;
    }

    private void SetToSpottedLight(Transform spotter)
    {
        if (spotter == transform)
        spotLight.color = spottedColor;
    }

    private void SetToLostVisualLight(Transform spotter)
    {
        if (spotter == transform)
            spotLight.color = searchingColor;
    }

    private void SetToUnspottedLight(Transform spotter)
    {
        if (spotter == transform)
            spotLight.color = unspottedColor;
    }
}
