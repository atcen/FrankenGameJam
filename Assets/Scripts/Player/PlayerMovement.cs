using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerMovement : MonoBehaviour
{
    
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnHorizontalMovement(InputValue value)
    {
        Vector2 horizontal = value.Get<Vector2>();
        this.rb.velocity = new Vector2(horizontal.x * this.speed, this.rb.velocity.y);
    }

    void OnJump(InputValue value)
    {
        this.rb.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
    }
    
    void OnSwapToKnight(InputValue value)
    {
        Debug.Log("Knight");
    }

    void OnSwapToMage(InputValue value)
    {
        Debug.Log("Mage");
    }

}
