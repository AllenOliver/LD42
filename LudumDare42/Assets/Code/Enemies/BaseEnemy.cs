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
    public virtual void Spawned () {
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
                HurtEnemy();
                break;

        }
    }

    /// <summary>
    /// Called when enemy gets hit
    /// </summary>
    public virtual void HurtEnemy()
    {
        CurrentHP--;
        if (CurrentHP <= 0)
        {
            Die();
        }
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
        yield return new WaitForSeconds(.5f);
        EZ_PoolManager.Despawn(gameObject.transform);
    }
}
