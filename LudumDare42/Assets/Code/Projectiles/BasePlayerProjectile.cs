using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class BasePlayerProjectile : BaseProjectile {

    public BasePlayerProjectile()
    {
        
    }

	// Use this for initialization
	public override void Spawned () {
        gameObject.tag = "PlayerProjectile";
	}
	
    public override void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            default:
                Die();
                break;
        }
    }

    public virtual void Die()
    {
        StartCoroutine(KillProjectile());
    }

    IEnumerator KillProjectile()
    {
        yield return null;
        EZ_PoolManager.Despawn(gameObject.transform);
    }
}
