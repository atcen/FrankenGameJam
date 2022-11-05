using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField] float fireballSpeed = 5f;


    Rigidbody2D rb;
    PlayerMovement player;
    [SerializeField] private float xspeed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xspeed = player.transform.localScale.x * fireballSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(xspeed, 0f);
    }
}
