using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SpotLightManagement : MonoBehaviour
{
    public Color unspottedColor = Color.white;
    public Color spottedColor = Color.red;
    public Color searchingColor = Color.yellow;
    public float intensity = 10;

    public Light spotLight;
    public SpotPlayer spotPlayer;

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
        spotLight.spotAngle = spotPlayer.searchLightAngle * 2;
    }

    private void FixedUpdate()
    {
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
