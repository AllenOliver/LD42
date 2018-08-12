using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartScript : MonoBehaviour {

    public List<GameObject> ItemsToActivate;
    AudioSource audio;
    public AudioClip Scream;
    bool HasStarted;

    private void Start()
    {
        HasStarted = false;
        audio = GetComponent<AudioSource>();
        foreach (GameObject item in ItemsToActivate)
        {
            item.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!HasStarted)
        {
            if (col.gameObject.tag == "Player")
            {
                StartCoroutine(StartBossEncounter());
                HasStarted = true;
            }
        }

    }

    IEnumerator StartBossEncounter()
    {
        var gm = FindObjectOfType<GameManager>();

        audio.clip = Scream;
        audio.Play();
        var player = FindObjectOfType<Player>();
        player.canMove = false;
        yield return new WaitForSeconds(2f);
        
        gm.GetComponent<AudioSource>().clip = gm.BossSong;
        gm.GetComponent<AudioSource>().Play();
        foreach (GameObject item in ItemsToActivate)
        {
            item.gameObject.SetActive(true);
        }
        player.canMove = true;
    }

}
