using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        Shooting.OnGunshot += PlayGonshotSound;
    }
    private void OnDestroy()
    {
        Shooting.OnGunshot -= PlayGonshotSound;
    }  
    
    private void PlayGonshotSound(Transform objectTranform)
    {
        _audioSource.Play();
    }
}
