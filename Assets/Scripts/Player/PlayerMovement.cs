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

    Vector2 moveInput;

    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer spriteRenderer;
    private PlayerLogic playerLogic;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.playerLogic = GetComponent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
    }

    void OnHorizontalMovement(InputValue value)
    {
        this.moveInput = value.Get<Vector2>();

        if (this.moveInput.x != 0)
        {
            this.animator.SetBool("isRunning", true);
        }
        else
        {
            this.animator.SetBool("isRunning", false);
        }

        if (this.moveInput.x < 0)
        {
            this.spriteRenderer.flipX = true;
        }
        else
        {
            this.spriteRenderer.flipX = false;
        }

    }


    void OnJump(InputValue value)
    {
        this.rb.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);

    }
    
    void OnSwapToKnight(InputValue value)
    {
        playerLogic.TransformPlayer();
        Debug.Log("Knight");
    }

    void OnSwapToMage(InputValue value)
    {
        playerLogic.TransformPlayer();
        Debug.Log("Mage");
    }

    void Run()
    {
        Vector2 horizontal = new Vector2(this.moveInput.x * this.speed, this.rb.velocity.y);
        this.rb.velocity = horizontal;
        }

}
