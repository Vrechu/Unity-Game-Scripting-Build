using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpotPlayer : MonoBehaviour
{
    [SerializeField] private Transform _cannonBarrel;

    [SerializeField] private float _searchLightAngle = 20;
    public float GetSearchLightAngle()
    {
        return _searchLightAngle;
    }

    [SerializeField] private float _resetTime = 3;
    private float _resetCounter = 0;

    public enum CannonState
    {
        SPOTTED, LOSTVISUAL, UNSPOTTED
    }
    private CannonState _cannonState = CannonState.UNSPOTTED;

    public CannonState GetCannonState()
    {
        return _cannonState;
    }
    public void SetCannonState(CannonState cannonState)
    {
        _cannonState = cannonState;
    }

    private Transform _target;
    private Transform _player;
    private Vector3 _directionToPlayer;


    public event Action OnPlayerSpotted;
    public event Action OnLostVisualOnPLayer;
    public event Action OnCannonReset;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_player != null)
        {
            _target = _player;
        }
    }

    private void Update()
    {
        PlayerSpot();
        LostVisual();
        CountDownToReset();
    }

    /// <summary>
    /// switches the cannonstate to the determined state and sends out the related events
    /// </summary>
    /// <param name="cannonState">state to set the cannonstate to</param>
    private void SwitchCannonState(CannonState cannonState)
    {
        _cannonState = cannonState;
        switch (_cannonState)
        {
            case CannonState.UNSPOTTED:
                {
                    _resetCounter = _resetTime;
                    OnCannonReset?.Invoke();
                    break;
                }
            case CannonState.SPOTTED:
                {
                    _resetCounter = _resetTime;
                    OnPlayerSpotted?.Invoke();
                    break;
                }
            case CannonState.LOSTVISUAL:
                {
                    OnLostVisualOnPLayer?.Invoke();
                    break;
                }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="hitInfo">raycast hitinfo</param>
    /// <returns>returns true  if the raycast hits a player</returns>
    private bool PlayerIsInTheOpen(RaycastHit hitInfo)
    {
        if (hitInfo.transform.tag is null
            || !(hitInfo.transform.tag is "Player"))
        {
            return false;
        }
        else if (hitInfo.transform.tag is "Player")
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>returns true if the the player is not obstructed by a wall and withing the searchlight angle</returns>
    private bool CanSeePlayer()
    {
        RaycastHit playerInfo = ObjectHitInfo(_target);
        _directionToPlayer = _target.position - transform.position;
        float angleToPlayer = Vector3.Angle(_cannonBarrel.forward, _directionToPlayer);
        if (PlayerIsInTheOpen(playerInfo)
             && angleToPlayer < _searchLightAngle)
        {
            return true;
        }
        else return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="racastObject">object to identify</param>
    /// <returns>returns hitinfo of the determined object</returns>
    private RaycastHit ObjectHitInfo(Transform racastObject)
    {
        Vector3 directionToTarget = racastObject.position - transform.position;
        RaycastHit info;
        Physics.Raycast(transform.position, directionToTarget, out info);
        return info;
    }

    /// <summary>
    /// sets the cannonstate to spotted if the player is spotted
    /// </summary>
    private void PlayerSpot()
    {
        if ((_cannonState == CannonState.UNSPOTTED
            || _cannonState == CannonState.LOSTVISUAL)
            && CanSeePlayer())
        {
            SwitchCannonState(CannonState.SPOTTED);
        }
    }

    /// <summary>
    /// sets the cannonstate to lost vidual if visual on the player is lost while the player is being tracked
    /// </summary>
    private void LostVisual()
    {
        if (_cannonState == CannonState.SPOTTED
            && !CanSeePlayer())
        {
            SwitchCannonState(CannonState.LOSTVISUAL);
        }
    }

    /// <summary>
    /// counts down until the cannonstate should be set to unspotted
    /// </summary>
    private void CountDownToReset()
    {
        if (_cannonState == CannonState.LOSTVISUAL)
        {
            _resetCounter -= Time.deltaTime;
        }
        if (_resetCounter <= 0)
        {
            SwitchCannonState(CannonState.UNSPOTTED);
        }
    }
}
