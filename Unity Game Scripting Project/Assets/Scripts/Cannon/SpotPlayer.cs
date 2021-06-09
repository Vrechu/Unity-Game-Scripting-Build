using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpotPlayer : MonoBehaviour
{
    public float searchLightAngle = 20;
    public float resetTime = 3;

    float resetCounter = 0;
    public Transform cannonBarrel;

    enum CannonState
    {
        SPOTTED, LOSTVISUAL, UNSPOTTED
    }
    CannonState cannonState = CannonState.UNSPOTTED;

    Transform target;
    Transform player;
    Vector3 directionToPlayer;
    Vector3 lookVector;


    public static event Action<Transform> OnPlayerSpotted;
    public static event Action<Transform> OnLostVisualOnPLayer;
    public static event Action<Transform> OnCannonReset;

    void Awake()
    {
        OnPlayerSpotted += SpottedByMe;
        OnLostVisualOnPLayer += VisualLostByMe;
        OnCannonReset += ResetMe;
    }

    void OnDestroy()
    {
        OnPlayerSpotted -= SpottedByMe;
        OnLostVisualOnPLayer -= VisualLostByMe;
        OnCannonReset -= ResetMe;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            target = player;
        }
    }
    void FixedUpdate()
    {
        lookVector = cannonBarrel.forward;
        PlayerSpot();
        LostVisual();
        CountDownToReset();
    }

    bool PlayerIsInTheOpen(RaycastHit hitInfo)
    {
        if (hitInfo.transform.tag == null
            || hitInfo.transform.tag != "Player")
        {
            return false;
        }
        else if (hitInfo.transform.tag == "Player")
        {
            return true;
        }
        else return false;
    }

    bool CanSeePlayer()
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

    RaycastHit ObjectHitInfo(Transform racastObject)
    {
        Vector3 directionToTarget = racastObject.position - transform.position;
        RaycastHit info;
        Physics.Raycast(transform.position, directionToTarget, out info);
        return info;
    }

    void PlayerSpot()
    {
        if ((cannonState == CannonState.UNSPOTTED
            || cannonState == CannonState.LOSTVISUAL)
            && CanSeePlayer())
        {
            OnPlayerSpotted?.Invoke(transform);
        }
    }

    void SpottedByMe(Transform spotter)
    {
        if (spotter == transform)
        {
            resetCounter = resetTime;
            cannonState = CannonState.SPOTTED;
        }
    }

    void LostVisual()
    {
        if (cannonState == CannonState.SPOTTED 
            && !CanSeePlayer())
        {
            OnLostVisualOnPLayer?.Invoke(transform);
        }
    }

    void VisualLostByMe(Transform spotter)
    {
        if (spotter == transform)
        {
            cannonState = CannonState.LOSTVISUAL;
        }
    }

    void CountDownToReset()
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

    void ResetMe(Transform spotter)
    {
        if (spotter == transform)
        {
            resetCounter = resetTime;
            cannonState = CannonState.UNSPOTTED;
        }
    }
}
