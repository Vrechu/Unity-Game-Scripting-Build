using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 5;

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime < 0) Destroy(gameObject);
    }
}
