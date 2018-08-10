using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRegion : MonoBehaviour {

    Rigidbody2D _rb2d;
    BoxCollider2D _collider;
    [Header("Size for the camera in each room")]
    public int CameraSizeForRoom;
    private Player player;
    //public DoorController door;

    void Awake()
    {
        _collider = gameObject.GetComponent<BoxCollider2D>();
        _rb2d = gameObject.AddComponent<Rigidbody2D>();
        _rb2d.isKinematic = true;
        player = FindObjectOfType<Player>();
    }

    void SetNewCameraBounds()
    {
        Camera.main.orthographicSize = CameraSizeForRoom;
        CameraAlign cam = Camera.main.gameObject.GetComponent<CameraAlign>();
        cam.SetNewBounds(_collider.bounds);
        
    }

    IEnumerator MovePlayer()
    {
        player.canMove = false;
        yield return  new WaitForSeconds(.25f);
        player.canMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Worked");
            SetNewCameraBounds();
            //StartCoroutine(MovePlayer());
           // StartCoroutine(door.SetDoorsActive());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
           // door.SetDoorsActive();
        }
    }
}
