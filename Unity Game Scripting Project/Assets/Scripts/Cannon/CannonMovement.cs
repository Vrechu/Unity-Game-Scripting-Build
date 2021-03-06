using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    [SerializeField] private Transform _cannonBarrel;
    [SerializeField] private List<Transform> _lookPositionTransforms = new List<Transform>();
    private Queue<Vector3> _lookPositions = new Queue<Vector3>();

    [SerializeField] private float _moveSpeed = 1;
    [SerializeField] private float _sentrySpeed = 1; //speed when moving between points
    [SerializeField] private float _trackingSpeed = 3; //speed when tracking player
    [SerializeField] private float _snapAngle = 5; 


    private Vector3 _directionVector;

    private Quaternion _baseRotation;
    private Quaternion _barrelRotation;

    private Transform _player;
    private Vector3 _currentTarget;

    private SpotPlayer _spotPlayer;

    private void Awake()
    {
        _spotPlayer = GetComponent<SpotPlayer>();
    }


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        foreach (Transform lookPositionTransform in _lookPositionTransforms)
        {
            _lookPositions.Enqueue(lookPositionTransform.position); //add position transforms to queue
        }
        if (_lookPositions.Count == 0) _lookPositions.Enqueue(new Vector3(0, 0, 1) + transform.position);
        _currentTarget = _lookPositions.Peek(); // if no transforms in queue, look forward
    }

    private void FixedUpdate()
    {        
        RotateBase();
        RotateBarrel();
    }

    private void Update()
    {
        SplitQuaternion();
        SwitchTarget();
    }

    /// <summary>
    /// sets the lookraotation of the cannon and splits this quaternion into barrel and base rotations.
    /// </summary>
    private void SplitQuaternion()
    {
        _directionVector = _currentTarget - transform.position;
        Quaternion completeAngle = Quaternion.LookRotation(_directionVector);
        _baseRotation = Quaternion.Euler(0, completeAngle.eulerAngles.y, 0);
        _barrelRotation = Quaternion.Euler(completeAngle.eulerAngles.x, 0, _cannonBarrel.localRotation.eulerAngles.z);
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

    /// <summary>
    /// if the player is spotted, follow the player.
    /// esle, cyvle through the look positions.
    /// </summary>
    private void SwitchTarget()
    {
        switch (_spotPlayer.GetCannonState())
        {
            case SpotPlayer.CannonState.SPOTTED:
                {
                    _currentTarget = _player.position;
                    _moveSpeed = _trackingSpeed;
                    break;
                }
            case SpotPlayer.CannonState.UNSPOTTED:
                {
                    _currentTarget = _lookPositions.Peek();
                    _moveSpeed = _sentrySpeed;
                    MoveBetweenPoints();
                    break;
                }
        }
    }

    /// <summary>
    /// when the target angle is reached, track the next target in the queue
    /// </summary>
    private void MoveBetweenPoints()
    {
        float currentTargetAngle = Vector3.Angle(_cannonBarrel.forward, _directionVector);
        if (currentTargetAngle <= _snapAngle)
        {
            _lookPositions.Enqueue(_lookPositions.Dequeue());
            _currentTarget = _lookPositions.Peek();
        }
    }
}
