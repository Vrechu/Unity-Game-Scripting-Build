using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
    }
    private void OnDestroy()
    {
        _audioSource.Play();
    }

}
