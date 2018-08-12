using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {

    #region PlayerVariables

    public bool canMove;
    private float movespeed = 6f;
    private Rigidbody2D rbody;
    public GameObject EquippedProjectile;
    public float ShootSpeed = 10f;
    public int MaxHP;
    public int CurrentHP;
    public AudioSource audio;
    private int ShotDelay;
    private int HurtDelay;
    public Transform shotSpot;

    public GameObject DeathParticles;
    

    #endregion

    #region Animator Vars
    private Animator anim;
    private GameManager gm;
    private bool isMoving;
    private Vector2 LastMove;
    #endregion


    // Use this for initialization
    void Start()
    {
        ShotDelay = 0;
        HurtDelay = 0;
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        canMove = true;
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        gm = FindObjectOfType<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (canMove)
        {
            case true:
                Movement();
                FireProjectile();
                break;
            case false:
                break;
        }
    }

    /// <summary>
    /// Handles player movement and blend tree variables
    /// </summary>
    private void Movement()
    {
        isMoving = false;

        if (Input.GetAxisRaw("Horizontal") > .5f || Input.GetAxisRaw("Horizontal") < -.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * 5f * Time.deltaTime, 0f, 0f));
            LastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            isMoving = true;


        }

        if (Input.GetAxisRaw("Vertical") > .5f || Input.GetAxisRaw("Vertical") < -.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * 5f * Time.deltaTime, 0f));
            LastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            isMoving = true;


        }

        
        anim.SetFloat("LastMoveX", LastMove.x);
        anim.SetFloat("LastMoveY", LastMove.y);
        anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        anim.SetBool("IsMoving", isMoving);
        

    }

    /// <summary>
    /// Public call to hurt player
    /// </summary>
    public void HurtPlayer()
    {
        StartCoroutine(PlayerHurt());
    }

    /// <summary>
    /// private enum that hurts player 
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerHurt()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        gm.CurrentPlayerHealth--;

        Die();
        yield return new WaitForSeconds(.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
        HurtDelay = 0;
    }

    /// <summary>
    /// Kill player function
    /// </summary>
    public void Die()
    {
        Debug.Log("It happened");
        if (gm.CurrentPlayerHealth <= 0)
        {
            Instantiate(DeathParticles, gameObject.transform.position, Quaternion.identity);

            gm.Respawn();
        }

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Enemy":
                if (HurtDelay > 30)
                {
                    HurtPlayer();

                }
                break;
        }
    }
    void FixedUpdate()
    {
        ShotDelay += 1;
        HurtDelay += 1;
    }
    /// <summary>
    /// 
    /// </summary>
    void FireProjectile()
    {
        Vector2 shooting = new Vector2(LastMove.x, LastMove.y);

        if (Input.GetButton("Fire"))
        {
            if (ShotDelay > 20)
            {
                EquippedProjectile = EZ_Pooling.EZ_PoolManager.Spawn(EquippedProjectile.transform, shotSpot.transform.position, gameObject.transform.rotation).gameObject;
                EquippedProjectile.GetComponent<Rigidbody2D>().AddForce(shooting * 8, ForceMode2D.Impulse);
                ShotDelay = 0;
            }
            else
            {
                return;
            }

        }

    }
}
