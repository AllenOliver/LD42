using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour {
    
    #region Variables

    public string Name;
    public string Description;
    AudioSource audio;
    public AudioClip PickupSound;
    GameManager gm;
    #endregion


    // Use this for initialization
    /// <summary>
    /// Pooled starting point
    /// </summary>
    public void Start()
    {
        gameObject.tag = "PickUp";
        gm = FindObjectOfType<GameManager>();
        audio = GetComponent<AudioSource>();
    }


    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case ("Player"):
                PowerupEffect();
                
                gm.OpenToolTip(this.Name, this.Description);
                PlayerSound();
                break;

        }
    }

    public void PowerupEffect()
    {
        var gm = FindObjectOfType<GameManager>();
        if (gm.CurrentPlayerHealth < gm.MaxPlayerHealth)
        {
            gm.CurrentPlayerHealth += 1;
        }
    }

    public void PlayerSound()
    {
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        audio.clip = PickupSound;
        audio.Play();
        yield return new WaitForSeconds(PickupSound.length);

        Destroy(gameObject);
    }

}
