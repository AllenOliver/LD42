using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZ_Pooling;
/// <summary>
/// BAse projectile class 
/// will be inherited by both enemy and player projectiles
/// set tag either 'EnemyProjectile' or 'PlayerProjectile'
/// Allen Oliver 2018
/// </summary>
public class BaseProjectile : MonoBehaviour {

    #region Vars
    public int Damage;
    #endregion

    public BaseProjectile()
    {

    }

    // Use this for initialization
    public virtual void Spawned () {
		
	}

    // Update is called once per frame
    public virtual void Update () {
		
	}

    public virtual void OnCollisionEnter2D(Collision2D col)
    {

    }

}
