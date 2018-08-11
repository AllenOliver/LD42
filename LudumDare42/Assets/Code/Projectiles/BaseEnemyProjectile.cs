using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;

public class BaseEnemyProjectile : BaseProjectile {

    public BaseEnemyProjectile()
    {

    }

    // Use this for initialization
    public override void Spawned()
    {
        gameObject.tag = "EnemyProjectile";
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
