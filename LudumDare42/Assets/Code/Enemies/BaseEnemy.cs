using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;


/// <summary>
/// Base class for all enemies
/// Allen Oliver 2018
/// </summary>
public class BaseEnemy : MonoBehaviour {

    #region Variables

    private Player player;
    public string Name;
    public int MaxHP;
    public GameObject DeathParticles;
    public AudioClip deathSound;
    public float moveSpeed;
    [Header("Enemy move frequency is a random number between these values")]
    public float frequencyMin;
    public float frequencyMax;
    [Header("Use this for standard mob behavior")]
    public bool isMob;
     AudioSource audio;
    [Header("This is how much memory he clears when killed")]
    public int MemorySpace;
    int CurrentHP;
    GameManager gm;

    public List<BasePickup> ItemsToDrop;
    #endregion

    /// <summary>
    /// Constuctor
    /// </summary>
    public BaseEnemy()
    {
        
    }

    // Use this for initialization
    /// <summary>
    /// Pooled starting point
    /// </summary>
    public virtual void OnSpawned() {
        CurrentHP = MaxHP;
        gameObject.tag = "Enemy";
        gm = FindObjectOfType<GameManager>();
        gm.Memory += MemorySpace;
        audio = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
        if (isMob) {
            StartCoroutine("Move");
        }
	}

    // Update is called once per frame
    public virtual void Update () {
		
	}

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case ("PlayerProjectile"):
                StartCoroutine( HurtEnemy());
                break;

        }
    }

    /// <summary>
    /// Called when enemy gets hit
    /// </summary>
    public virtual IEnumerator HurtEnemy()
    {
        CurrentHP--;
        if (CurrentHP <= 0)
        {
            StartCoroutine(PlaySound(deathSound));
            Die();
        }

        GetComponent<SpriteRenderer>().color = Color.red;
     
        yield return new WaitForSeconds(.5f);
        GetComponent<SpriteRenderer>().color = Color.white;

    }

    /// <summary>
    /// Controls enemy movement
    /// <summary>

    public virtual IEnumerator Move() {
        while (CurrentHP > 0) {
            GetComponent<Rigidbody2D>().AddForce((player.transform.position - transform.position) * moveSpeed);
            yield return new WaitForSeconds (UnityEngine.Random.Range(.25f, .75f));
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            yield return new WaitForSeconds (UnityEngine.Random.Range(frequencyMin, frequencyMax));
        }
    }

    /// <summary>
    /// Public caller for die routine
    /// </summary>
    public virtual void Die()
    {
        
        StartCoroutine(DieRoutine());
    }

   public virtual IEnumerator DieRoutine()
    {
        EZ_PoolManager.Spawn(DeathParticles.transform, gameObject.transform.position, gameObject.transform.rotation);
        //play anim
        gm.Memory -= MemorySpace;
        int randomChance = UnityEngine.Random.Range(0,100);
        if (randomChance > 85 && ItemsToDrop.Count > 0)
        {
            Instantiate(ItemsToDrop[UnityEngine.Random.Range(0, ItemsToDrop.Count)], gameObject.transform.position, Quaternion.identity);

        }
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        EZ_PoolManager.Despawn(gameObject.transform);
    }

    IEnumerator PlaySound(AudioClip sound)
    {
        Debug.Log("Played");
        audio.clip = sound;
        yield return null;
        audio.Play();
    }
}
