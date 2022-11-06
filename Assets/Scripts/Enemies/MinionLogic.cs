using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionLogic : MonoBehaviour
{

    private BaseEnemy baseEnemy;
    [SerializeField] float moveSpeed = 3f;
    Rigidbody2D rb;
    [SerializeField] float scale;
    float flipCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        scale = Random.Range(1f, 3f);
        transform.localScale = new Vector2(scale, scale);
        baseEnemy = GetComponent<BaseEnemy>();
        baseEnemy.health = (int)scale;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        flipCooldown--;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "ground") { return; }
        if (flipCooldown > 0) { return; }

        moveSpeed = -moveSpeed;
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed) * scale, scale);
        flipCooldown = 200;
    }
}
