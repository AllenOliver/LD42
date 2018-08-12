﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base pickup class
/// handles animation, destroying, and collision
/// detail functionality in specific scripts
/// Allen Oliver 2018
/// </summary>
public class BasePickup : MonoBehaviour {

    #region Variables

    public string Name;
    public string Description;
    GameManager gm;
    #endregion

    /// <summary>
    /// Constuctor
    /// </summary>
    public BasePickup()
    {

    }

    // Use this for initialization
    /// <summary>
    /// Pooled starting point
    /// </summary>
    public virtual void Start()
    {
        gameObject.tag = "PickUp";
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case ("Player"):
                PowerupEffect();
                gm.OpenToolTip(Name, Description);
                Destroy(gameObject);
                break;

        }
    }

    public virtual void PowerupEffect()
    {
        Debug.Log("Give me some effects here");
    }




}
