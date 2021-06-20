using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    [SerializeField] private Vector3 _lookLocation1;
    [SerializeField] private Vector3 _lookLocation2;

    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _sentrySpeed = 1;
    [SerializeField] private float _trackingSpeed = 3;
    [SerializeField] private float _snapAngle = 5;

    [SerializeField] private Transform _cannonBarrel;

    private Vector3 _directionVector;
    private Vector3 _lookVector;

    private Quaternion _completeAngle;
    private Quaternion _baseRotation;
    private Quaternion _barrelRotation;

    private Transform _player;
    private Vector3 _currentTarget;

    private enum CannonState
    {
        SPOTTED, LOSTVISUAL, UNSPOTTED
    }
    private CannonState _cannonState = CannonState.UNSPOTTED;

    private void Awake()
    {
        SpotPlayer.OnPlayerSpotted += SetTargetToPlayer;
        SpotPlayer.OnCannonReset += ResetCannon;
    }

    private void OnDestroy()
    {
        SpotPlayer.OnPlayerSpotted -= SetTargetToPlayer;
        SpotPlayer.OnCannonReset -= ResetCannon;
    }


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _currentTarget = _lookLocation1;

        _lookLocation1 += transform.position;
        _lookLocation2 += transform.position;
    }

    private void FixedUpdate()
    {
        SplitQuaternion();
        FollowTarget();
        RotateBase();
        RotateBarrel();
        _lookVector = _cannonBarrel.forward;
    }

    private void SplitQuaternion()
    {
        _directionVector = _currentTarget - transform.position;
        _completeAngle = Quaternion.LookRotation(_directionVector);
        _baseRotation = Quaternion.Euler(0, _completeAngle.eulerAngles.y, 0);
        _barrelRotation = Quaternion.Euler(_completeAngle.eulerAngles.x, 0, _cannonBarrel.localRotation.eulerAngles.z);
    }

    private void RotateBase()
    {
        _baseRotation = Quaternion.Slerp(transform.rotation, _baseRotation, _moveSpeed * Time.fixedDeltaTime);
        transform.rotation = _baseRotation;
    }

    private void RotateBarrel()
    {
        _barrelRotation = Quaternion.Slerp(_cannonBarrel.localRotation, _barrelRotation, _moveSpeed * Time.fixedDeltaTime);
        _cannonBarrel.localRotation = _barrelRotation;
    }

    private void FollowTarget()
    {
        if (_cannonState == CannonState.SPOTTED
            && _player != null)
        {
            _currentTarget = _player.position;
        }
        else if (_cannonState == CannonState.UNSPOTTED)
        {
            MoveBetweenPoints();
        }
    }

    private void SetTargetToPlayer(Transform spotter)
    {
        if (spotter == transform)
        {
            _cannonState = CannonState.SPOTTED;
            _moveSpeed = _trackingSpeed;
        }
    }

    private void ResetCannon(Transform spotter)
    {
        if (spotter == transform)
        {
            _cannonState = CannonState.UNSPOTTED;
            _currentTarget = _lookLocation1;
            _moveSpeed = _sentrySpeed;
        }
    }
    private void MoveBetweenPoints()
    {
        float currentTargetAngle = Vector3.Angle(_lookVector, _directionVector);
        if (currentTargetAngle <= _snapAngle)
        {
            SwitchTargets();
        }        
    }

    private void SwitchTargets()
    {
        if (_currentTarget == _lookLocation1)
            _currentTarget = _lookLocation2;
        else if (_currentTarget == _lookLocation2)
            _currentTarget = _lookLocation1;
    }
}
