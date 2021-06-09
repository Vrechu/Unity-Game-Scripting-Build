using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotSound : MonoBehaviour
{
    public AudioSource audioSource;

    void Awake()
    {
        Shooting.OnGunshot += PlayGonshotSound;
    }
    void OnDestroy()
    {
        Shooting.OnGunshot -= PlayGonshotSound;
    }

    void Start()
    {
         
    }

    void Update()
    {
        
    }
    void PlayGonshotSound(Transform objectTranform)
    {
        audioSource.Play();
    }
}
