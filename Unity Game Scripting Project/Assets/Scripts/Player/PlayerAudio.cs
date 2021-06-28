using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _hurtSound;
    [SerializeField] private AudioClip _pickupSound;


    private AudioSource _audioSource;
    private JumpPlayer _jumpPlayer;

    private void Start()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
        _jumpPlayer = JumpPlayer.GetJumpPlayer();

        _jumpPlayer.OnJump += PlayJumpSound;
        BulletImpact.OnPlayerHit += PlayHurtSound;
        PickupInteract.OnSprintPickup += PlayPickupSound;
    }

    private void OnDestroy()
    {
        _jumpPlayer.OnJump -= PlayJumpSound;
        BulletImpact.OnPlayerHit -= PlayHurtSound;
        PickupInteract.OnSprintPickup -= PlayPickupSound;
    }

    private void PlayJumpSound()
    {
        _audioSource.PlayOneShot(_jumpSound);
    }

    private void PlayHurtSound(float damage)
    {
        _audioSource.PlayOneShot(_hurtSound);
    }

    private void PlayPickupSound()
    {
        _audioSource.PlayOneShot(_pickupSound);
    }
}
