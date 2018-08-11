using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class PlayerDistanceSpawner : BaseSpawner
{
    [Tooltip("If not assigned, will try to locate by tag name: 'Player'")]
    public GameObject Player;
    public GameObject prefabToSpawn;

    [Tooltip("The number of world units that equal to 1 second spawn time added")]
    public float multiplier = 0.5f; // every half world unit adds a second to the spawn time
    [Tooltip("The minimum distance in world units to cut spawner time to prevent too fast spawns")]
    public float distanceCap = 1;

    float _secondsSinceLastSpawn = 0f;
    float _distanceToPlayer = 999999f; // 1 world unit is equivalent to 1 second waiting time

    void Start()
    {
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (isActive)
        {
            _secondsSinceLastSpawn += Time.deltaTime;
            _distanceToPlayer = (Player.transform.position - transform.position).magnitude;

            // Set a minimum distance that the player no longer has an effect on spawns. 
            // Prevents spawns that are too fast (approaching infinity!) as you get closer.
            _distanceToPlayer = _distanceToPlayer <= distanceCap ? distanceCap : _distanceToPlayer;

            if (_secondsSinceLastSpawn > _distanceToPlayer * multiplier)
            {
                EZ_PoolManager.Spawn(prefabToSpawn.transform, gameObject.transform.position, gameObject.transform.rotation);
                _secondsSinceLastSpawn = 0f;
                Debug.Log("PlayerDistanceSpawner.Update(): New enemy spawned");
            }
        }
    }
}
