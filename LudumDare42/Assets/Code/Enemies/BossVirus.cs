using EZ_Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVirus : MonoBehaviour {


    #region Variables

    public string Name;
    public int MaxHP;
    public GameObject DeathParticles;
    public AudioClip deathSound;
    AudioSource audio;
    [Header("This is how much memory he clears when killed")]
    public int MemorySpace;
    public int CurrentHP;
    GameManager gm;

    #endregion
    // Use this for initialization
    /// <summary>
    /// Pooled starting point
    /// </summary>
    public virtual void Start()
    {
        CurrentHP = MaxHP;
        gameObject.tag = "Enemy";
        gm = FindObjectOfType<GameManager>();
        gm.Memory += MemorySpace;
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case ("PlayerProjectile"):
                StartCoroutine(HurtEnemy());
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
    /// Public caller for die routine
    /// </summary>
    public virtual void Die()
    {

        StartCoroutine(DieRoutine());
    }



    public virtual IEnumerator PlaySound(AudioClip sound)
    {
        Debug.Log("Played");
        audio.clip = sound;
        yield return null;
        audio.Play();
    }
    public IEnumerator DieRoutine()
    {
        EZ_PoolManager.Spawn(DeathParticles.transform, gameObject.transform.position, gameObject.transform.rotation);
        //play anim
        gm.Memory -= MemorySpace;

        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
        Destroy(gameObject);
    }
}
