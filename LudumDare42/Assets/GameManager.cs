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

    public GameObject ToolTipPanel;
    public Text ToolTipBody;
    public Text ToolTipTitle;

    public Image MemoryFill;
    public Text MemoryText;
    public Text HealthText;
    public AudioClip LevelSong;
    public AudioClip BossSong;
    bool winOpened;
    bool loseOpened;
    bool memoryOpened;
    bool WarningOpened;
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
        WarningOpened = false;
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Memory < 0)
        {
            Memory = 0;
        }
        else if (Memory > 750 && !WarningOpened)
        {
            OpenToolTip("Memory Limit Close!","Memory at 75% compacity!");
            WarningOpened = true;
        }
        else if (Memory >= 1000)
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
        float fillamount = Memory / 1000f;
        MemoryFill.fillAmount = Mathf.Clamp01(fillamount);
        MemCheck();
        SpawnerCheck();
    }

    /// <summary>
    /// Opens panel on player death
    /// </summary>
    public void OpenDeathPanel()
    {
        StartCoroutine(OpenDeathPanelRoutine());
    }

    IEnumerator OpenDeathPanelRoutine()
    {
        DeathPanel.GetComponent<Animation>().Play("Open");
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }

    public void Respawn()
    {
        if (Time. timeScale < 1f)
        {
            Time.timeScale = 1f;
        }
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
        var SoundToPlay = RestartPanel.GetComponent<AudioSource>();
        SoundToPlay.clip = RestartPanel.GetComponent<LosePanelScript>().ErrorSound;
        SoundToPlay.Play();
        yield return new WaitForSeconds(RestartPanel.GetComponent<AudioSource>().clip.length);
        
        RestartPanel.GetComponent<Animation>().Play("Open");
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// If array of spawners is less than or equal to zero
    /// win game 
    /// </summary>
    public void SpawnerCheck()
    {
        var objects = FindObjectsOfType<BossVirus>();
        if (objects.Length <=0)
        {
            StartCoroutine(enumeratorWinPanel());
        }
    }
     IEnumerator enumeratorWinPanel()
    {
        WinPanel.GetComponent<Animation>().Play("Open");
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0f;
    }

    public void OpenToolTip(string ToolTipTitle, string ToolTipBody)
    {
        StartCoroutine(OpenToolTipRoutine(ToolTipTitle, ToolTipBody));
    }

    IEnumerator OpenToolTipRoutine(string ToolTipTitleString, string ToolTipBodyString)
    {
        ToolTipPanel.GetComponent<Animation>().Play("Open");
        ToolTipBody.text = ToolTipBodyString;
        ToolTipTitle.text = ToolTipTitleString;
        yield return new WaitForSeconds(3f);
        ToolTipPanel.GetComponent<Animation>().Play("Close");
    }
}
