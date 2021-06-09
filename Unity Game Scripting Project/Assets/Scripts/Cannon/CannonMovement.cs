using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    public Vector3 lookLocation1;
    public Vector3 lookLocation2;

    public float moveSpeed = 1;
    public float sentrySpeed = 1;
    public float trackingSpeed = 3;
    public float snapAngle = 5;

    public Transform cannonBarrel;

    Vector3 directionVector;
    Vector3 lookVector;

    Quaternion completeAngle;
    Quaternion baseRotation;
    Quaternion barrelRotation;

    Transform player;
    Vector3 currentTarget;

    enum CannonState
    {
        SPOTTED, LOSTVISUAL, UNSPOTTED
    }
    CannonState cannonState = CannonState.UNSPOTTED;

    void Awake()
    {
        SpotPlayer.OnPlayerSpotted += SetTargetToPlayer;
        SpotPlayer.OnCannonReset += ResetCannon;
    }

    void OnDestroy()
    {
        SpotPlayer.OnPlayerSpotted -= SetTargetToPlayer;
        SpotPlayer.OnCannonReset -= ResetCannon;
    }


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentTarget = lookLocation1;
    }

    void FixedUpdate()
    {
        SplitQuaternion();
        FollowTarget();
        RotateBase();
        RotateBarrel();
        lookVector = cannonBarrel.forward;
    }

    void SplitQuaternion()
    {
        directionVector = currentTarget - transform.position;
        completeAngle = Quaternion.LookRotation(directionVector);
        baseRotation = Quaternion.Euler(0, completeAngle.eulerAngles.y, 0);
        barrelRotation = Quaternion.Euler(completeAngle.eulerAngles.x, 0, cannonBarrel.localRotation.eulerAngles.z);
    }

    void RotateBase()
    {
        baseRotation = Quaternion.Lerp(transform.rotation, baseRotation, moveSpeed * Time.fixedDeltaTime);
        transform.rotation = baseRotation;
    }

    void RotateBarrel()
    {
        barrelRotation = Quaternion.Lerp(cannonBarrel.localRotation, barrelRotation, moveSpeed * Time.fixedDeltaTime);
        cannonBarrel.localRotation = barrelRotation;
    }

    void FollowTarget()
    {
        if (cannonState == CannonState.SPOTTED
            && player != null)
        {
            currentTarget = player.position;
        }
        else if (cannonState == CannonState.UNSPOTTED)
        {
            MoveBetweenPoints();
        }
    }

    void SetTargetToPlayer(Transform spotter)
    {
        if (spotter == transform)
        {
            cannonState = CannonState.SPOTTED;
            moveSpeed = trackingSpeed;
        }
    }

    void ResetCannon(Transform spotter)
    {
        if (spotter == transform)
        {
            cannonState = CannonState.UNSPOTTED;
            currentTarget = lookLocation1;
            moveSpeed = sentrySpeed;
        }
    }
    void MoveBetweenPoints()
    {
        float currentTargetAngle = Vector3.Angle(lookVector, directionVector);
        if (currentTargetAngle <= snapAngle)
        {
            SwitchTargets();
        }        
    }

    void SwitchTargets()
    {
        if (currentTarget == lookLocation1)
            currentTarget = lookLocation2;
        else if (currentTarget == lookLocation2)
            currentTarget = lookLocation1;
    }
}
