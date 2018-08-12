using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class FixedRateSpawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public GameObject DoorToOpen;
    public GameObject DeathParticles;
    [SerializeField] float SecondsBetweenSpawns;

    public int MemorySpace;
    public int MaxHP;
    int CurrentHP;
    float _secondsSinceLastSpawn = 0f;
    GameManager gm;

  


    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gm.Memory += MemorySpace;
        CurrentHP = MaxHP;
    }
    void Update()
    {
        _secondsSinceLastSpawn += Time.deltaTime;

        if (_secondsSinceLastSpawn > SecondsBetweenSpawns)
        {
            EZ_PoolManager.Spawn(prefabToSpawn.transform, gameObject.transform.position, gameObject.transform.rotation);
            _secondsSinceLastSpawn = 0f;
            Debug.Log("FixedRateSpawner.Update(): New enemy spawned");
        }
    }

    IEnumerator HurtSpawner()
    {
        CurrentHP--;
        if (CurrentHP <= 0)
        {
            Die();
        }

        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(.5f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }

    public void Die()
    {
        EZ_PoolManager.Spawn(DeathParticles.transform, gameObject.transform.position, gameObject.transform.rotation);
        StartCoroutine(OpenDoor());
        StartCoroutine(DieRoutine());
    }

    public IEnumerator DieRoutine()
    {

        gm.Memory -= MemorySpace;

        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case"PlayerProjectile":
                StartCoroutine(HurtSpawner());
                break;
        }
    }

    IEnumerator OpenDoor()
    {
        if (DoorToOpen)
        {
            gm.OpenToolTip("Door Opened!","You have opened more rooms to clean.");
            DoorToOpen.SetActive(false);
        }
            
        yield return null;
    }

}
