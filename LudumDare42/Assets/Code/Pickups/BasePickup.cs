using System.Collections;
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
    AudioSource audio;
    public AudioClip PickupSound;
    GameManager gm;
    #endregion

    /// <summary>
    /// starting point
    /// </summary>
    public virtual void Start()
    {
        gameObject.tag = "PickUp";
        gm = FindObjectOfType<GameManager>();
        audio = GetComponent<AudioSource>();
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
                PlayerSound();
                gm.OpenToolTip(this.Name, this.Description);
                Destroy(gameObject);
                break;

        }
    }

    public virtual void PowerupEffect()
    {
        Debug.Log("Give me some effects here");
    }

    public void PlayerSound()
    {
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        audio.clip = PickupSound;
       
        yield return null;
        audio.Play();
    }


}
