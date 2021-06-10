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

    private Vector3 directionVector;
    private Vector3 lookVector;

    private Quaternion completeAngle;
    private Quaternion baseRotation;
    private Quaternion barrelRotation;

    private Transform player;
    private Vector3 currentTarget;

    private enum CannonState
    {
        SPOTTED, LOSTVISUAL, UNSPOTTED
    }
    CannonState cannonState = CannonState.UNSPOTTED;

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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentTarget = lookLocation1;

        lookLocation1 += transform.position;
        lookLocation2 += transform.position;
    }

    private void FixedUpdate()
    {
        SplitQuaternion();
        FollowTarget();
        RotateBase();
        RotateBarrel();
        lookVector = cannonBarrel.forward;
    }

    private void SplitQuaternion()
    {
        directionVector = currentTarget - transform.position;
        completeAngle = Quaternion.LookRotation(directionVector);
        baseRotation = Quaternion.Euler(0, completeAngle.eulerAngles.y, 0);
        barrelRotation = Quaternion.Euler(completeAngle.eulerAngles.x, 0, cannonBarrel.localRotation.eulerAngles.z);
    }

    private void RotateBase()
    {
        baseRotation = Quaternion.Lerp(transform.rotation, baseRotation, moveSpeed * Time.fixedDeltaTime);
        transform.rotation = baseRotation;
    }

    private void RotateBarrel()
    {
        barrelRotation = Quaternion.Lerp(cannonBarrel.localRotation, barrelRotation, moveSpeed * Time.fixedDeltaTime);
        cannonBarrel.localRotation = barrelRotation;
    }

    private void FollowTarget()
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

    private void SetTargetToPlayer(Transform spotter)
    {
        if (spotter == transform)
        {
            cannonState = CannonState.SPOTTED;
            moveSpeed = trackingSpeed;
        }
    }

    private void ResetCannon(Transform spotter)
    {
        if (spotter == transform)
        {
            cannonState = CannonState.UNSPOTTED;
            currentTarget = lookLocation1;
            moveSpeed = sentrySpeed;
        }
    }
    private void MoveBetweenPoints()
    {
        float currentTargetAngle = Vector3.Angle(lookVector, directionVector);
        if (currentTargetAngle <= snapAngle)
        {
            SwitchTargets();
        }        
    }

    private void SwitchTargets()
    {
        if (currentTarget == lookLocation1)
            currentTarget = lookLocation2;
        else if (currentTarget == lookLocation2)
            currentTarget = lookLocation1;
    }
}
