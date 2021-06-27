using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotSound : MonoBehaviour
{
    private AudioSource _audioSource;
    private Shooting _shooting;

    [SerializeField] private AudioClip _shootingSound;
    [SerializeField] private AudioClip _spottedSound;
    [SerializeField] private AudioClip _resetSound;

    private SpotPlayer _spotPlayer;
    private SpotPlayer.CannonState _cannonState;

    private void Awake()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
        _shooting = FindObjectOfType<Shooting>();
        _spotPlayer = GetComponent<SpotPlayer>();
        _shooting.OnGunshot += PlayGonshotSound;
    }
    private void OnDestroy()
    {
        _shooting.OnGunshot -= PlayGonshotSound;
    }

    private void Update()
    {
        PlaySpottedSound();
    }

    private void PlayGonshotSound(Transform objectTranform)
    {
        _audioSource.PlayOneShot(_shootingSound);
    }

    private void PlaySpottedSound()
    {
        if (_spotPlayer.GetCannonState() != _cannonState)
        {
            _cannonState = _spotPlayer.GetCannonState();
            if (_cannonState == SpotPlayer.CannonState.SPOTTED) _audioSource.PlayOneShot(_spottedSound);
        }
    }
}
