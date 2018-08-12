using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manager for all game files
/// </summary>
public class GameManager : MonoBehaviour {


    #region Data Variables

    [Header("The memory to fill up")]
    public int Memory;
    [Header("How many hits can I take?")]
    public int MaxPlayerHealth;
    [Header("How many hits have I taken?")] 
    public int CurrentPlayerHealth;


  
    Player player;

    #endregion


    #region UI Variables
    
    public GameObject RestartPanel;
    public GameObject WinPanel;
    public GameObject DeathPanel;
    public GameObject FadePanel;

    public Image MemoryFill;
    public Text MemoryText;
    public Text HealthText;

    bool winOpened;
    bool loseOpened;
    bool memoryOpened;
    #endregion

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        CurrentPlayerHealth = MaxPlayerHealth;
        
    }

    // Use this for initialization
    void Start () {
        FadePanel.GetComponent<Animation>().Play("FadeIn");
        winOpened = false;
        loseOpened = false;
        memoryOpened = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Memory < 0)
        {
            Memory = 0;
        }
        else if (Memory >= 100)
        {
            memoryOpened = true;
        }
    }

    /// <summary>
    /// Sets UI every tick
    /// </summary>
    void FixedUpdate()
    {

        HealthText.text ="HP: " + CurrentPlayerHealth + " / " + MaxPlayerHealth;
        MemoryText.text = Memory + " MB";
        float fillamount = Memory / 100f;
        MemoryFill.fillAmount = Mathf.Clamp01(fillamount);
        MemCheck();
    }

    /// <summary>
    /// Opens panel on player death
    /// </summary>
    public void OpenDeathPanel()
    {
        DeathPanel.GetComponent<Animation>().Play("Open");
    }

    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());

    }

    IEnumerator RespawnRoutine()
    {
        player.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1.75f);
        FadePanel.GetComponent<Animation>().Play("FadeOut");
        var sceneNow = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNow);
    }
    /// <summary>
    /// Checks for memeory limit reached and ends game if reached.
    /// </summary>
    public void MemCheck()
    {
        if (memoryOpened)
        {
            StartCoroutine(PlayErrorSound());
            
           
        }

    }


    IEnumerator PlayErrorSound()
    {
        RestartPanel.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(RestartPanel.GetComponent<AudioSource>().clip.length);
        RestartPanel.GetComponent<Animation>().Play("Open");
    }

    /// <summary>
    /// If array of spawners is less than or equal to zero
    /// win game 
    /// </summary>
    public void SpawnerCheck()
    {
        var objects = FindObjectsOfType<FixedRateSpawner>();
        if (objects.Length <=0)
        {
            WinPanel.GetComponent<Animation>().Play("Open");
        }
    }
}
