using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField] float fireballSpeed = 6f;


    Rigidbody2D rb;
    PlayerMovement player;
    [SerializeField] private float xspeed;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xspeed = player.transform.localScale.x * fireballSpeed;
        if(xspeed < 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(xspeed) * 0.35f, 0.35f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(xspeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemies")
        {
            other.gameObject.GetComponent<BaseEnemy>().TakeDamage(1,Characters.Mage);
        }
        Destroy(gameObject);
    }

}
