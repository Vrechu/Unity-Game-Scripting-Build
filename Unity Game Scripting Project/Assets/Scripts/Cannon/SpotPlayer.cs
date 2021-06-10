using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpotPlayer : MonoBehaviour
{
    public float searchLightAngle = 20;
    public float resetTime = 3;

    private float resetCounter = 0;
    public Transform cannonBarrel;

    private enum CannonState
    {
        SPOTTED, LOSTVISUAL, UNSPOTTED
    }
    private CannonState cannonState = CannonState.UNSPOTTED;

    private Transform target;
    private Transform player;
    private Vector3 directionToPlayer;
    private Vector3 lookVector;


    public static event Action<Transform> OnPlayerSpotted;
    public static event Action<Transform> OnLostVisualOnPLayer;
    public static event Action<Transform> OnCannonReset;

    private void Awake()
    {
        OnPlayerSpotted += SpottedByMe;
        OnLostVisualOnPLayer += VisualLostByMe;
        OnCannonReset += ResetMe;
    }

    private void OnDestroy()
    {
        OnPlayerSpotted -= SpottedByMe;
        OnLostVisualOnPLayer -= VisualLostByMe;
        OnCannonReset -= ResetMe;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            target = player;
        }
    }
    private void FixedUpdate()
    {
        lookVector = cannonBarrel.forward;
        PlayerSpot();
        LostVisual();
        CountDownToReset();
    }

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

    private bool CanSeePlayer()
    {
        RaycastHit playerInfo = ObjectHitInfo(target);
        directionToPlayer = target.position - transform.position;
        float angleToPlayer = Vector3.Angle(lookVector, directionToPlayer);
        if (PlayerIsInTheOpen(playerInfo)
             && angleToPlayer < searchLightAngle)
        {
            return true;
        }        
        else return false;
    }

    private RaycastHit ObjectHitInfo(Transform racastObject)
    {
        Vector3 directionToTarget = racastObject.position - transform.position;
        RaycastHit info;
        Physics.Raycast(transform.position, directionToTarget, out info);
        return info;
    }

    private void PlayerSpot()
    {
        if ((cannonState == CannonState.UNSPOTTED
            || cannonState == CannonState.LOSTVISUAL)
            && CanSeePlayer())
        {
            OnPlayerSpotted?.Invoke(transform);
        }
    }

    private void SpottedByMe(Transform spotter)
    {
        if (spotter == transform)
        {
            resetCounter = resetTime;
            cannonState = CannonState.SPOTTED;
        }
    }

    private void LostVisual()
    {
        if (cannonState == CannonState.SPOTTED 
            && !CanSeePlayer())
        {
            OnLostVisualOnPLayer?.Invoke(transform);
        }
    }

    private void VisualLostByMe(Transform spotter)
    {
        if (spotter == transform)
        {
            cannonState = CannonState.LOSTVISUAL;
        }
    }

    private void CountDownToReset()
    {
        if (cannonState == CannonState.LOSTVISUAL)
        {
            resetCounter -= Time.deltaTime;
        }
        if (resetCounter <= 0)
        {
            OnCannonReset?.Invoke(transform);
        }
    }

    private void ResetMe(Transform spotter)
    {
        if (spotter == transform)
        {
            resetCounter = resetTime;
            cannonState = CannonState.UNSPOTTED;
        }
    }
}
