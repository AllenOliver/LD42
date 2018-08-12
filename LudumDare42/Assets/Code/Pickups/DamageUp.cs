using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : BasePickup
{

    // Use this for initialization
    public DamageUp()
    {
        Name = "Damage Up";
        Description = "Adds to your firepower! Rip and Tear!";
    }

    public override void PowerupEffect()
    {
        StartCoroutine(PowerupTime());
    }

    IEnumerator PowerupTime()
    {
        
        var playerBullet = FindObjectOfType<Player>();
        var originalDamage = playerBullet.EquippedProjectile.GetComponent<BasePlayerProjectile>().Damage;
        playerBullet.EquippedProjectile.GetComponent<BasePlayerProjectile>().Damage++;
        yield return new WaitForSeconds(5f);
        playerBullet.EquippedProjectile.GetComponent<BasePlayerProjectile>().Damage = originalDamage;

    }
}
