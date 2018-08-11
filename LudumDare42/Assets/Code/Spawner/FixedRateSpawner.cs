using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class FixedRateSpawner : BaseSpawner
{
    public GameObject prefabToSpawn;
    [SerializeField] float SecondsBetweenSpawns = 1f;

    float _secondsSinceLastSpawn = 0f;

    void Update()
    {
        if (isActive)
        {
            _secondsSinceLastSpawn += Time.deltaTime;

            if (_secondsSinceLastSpawn > SecondsBetweenSpawns)
            {
                EZ_PoolManager.Spawn(prefabToSpawn.transform, gameObject.transform.position, gameObject.transform.rotation);
                _secondsSinceLastSpawn = 0f;
                Debug.Log("FixedRateSpawner.Update(): New enemy spawned");
            }
        }
    }
}
