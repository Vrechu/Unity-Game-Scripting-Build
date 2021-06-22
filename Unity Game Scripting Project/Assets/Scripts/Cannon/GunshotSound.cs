using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private Shooting _shooting;

    private void Awake()
    {
        _shooting = FindObjectOfType<Shooting>();
        _shooting.OnGunshot += PlayGonshotSound;
    }
    private void OnDestroy()
    {
        _shooting.OnGunshot -= PlayGonshotSound;
    }  
    
    private void PlayGonshotSound(Transform objectTranform)
    {
        _audioSource.Play();
    }
}
