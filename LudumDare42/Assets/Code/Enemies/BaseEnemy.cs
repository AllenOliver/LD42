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

    public string Name;
    public int MaxHP;
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

   IEnumerator DieRoutine()
    {
        //play anim
        gm.Memory -= MemorySpace;
        int randomChance = UnityEngine.Random.Range(0,100);
        if (randomChance > 85 && ItemsToDrop.Count > 0)
        {
            Instantiate(ItemsToDrop[UnityEngine.Random.Range(1, ItemsToDrop.Count)], gameObject.transform.position, Quaternion.identity);

        }
        yield return new WaitForSeconds(.5f);
        EZ_PoolManager.Despawn(gameObject.transform);
    }
}
