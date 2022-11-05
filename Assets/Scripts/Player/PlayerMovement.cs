using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [FormerlySerializedAs("audioClipsKnight")] [SerializeField] private AudioClip[] footstepsKnight;
    [FormerlySerializedAs("audioClipsMage")] [SerializeField] private AudioClip[] footstepsMage;
    
    Vector2 moveInput;

    private Rigidbody2D rb;

    private Animator animator;

    private SpriteRenderer spriteRenderer;
    private PlayerLogic playerLogic;

    private CapsuleCollider2D capsuleCollider2D;
    
    private AudioSource audioSource;
    private AudioClip[] activeFootsteps;

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        this.playerLogic = GetComponent<PlayerLogic>();
        this.audioSource = GetComponent<AudioSource>();
        
        this.activeFootsteps = this.footstepsKnight;

    }

    // Update is called once per frame
    void Update()
    {
        Run();
        this.audioSource.clip = this.activeFootsteps[0];

        if (animator.GetBool("isRunning") && this.capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Platforms")))
        {
            this.audioSource.enabled = true;
        }
        else
        {
            this.audioSource.enabled = false;
        }

        if (this.rb.velocity.x != 0)
        {
                   transform.localScale = new Vector2(Mathf.Sign(this.rb.velocity.x), 1f);
        }

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

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        this.animator.SetTrigger("LeaveJump");
        this.animator.SetBool("isJumping", false);
    }
    
    void OnJump(InputValue value)
    {
        if (!this.capsuleCollider2D.IsTouchingLayers(LayerMask.GetMask("Platforms")))
        {
            return;
        }
        this.rb.AddForce(Vector2.up * this.jumpForce, ForceMode2D.Impulse);
        this.animator.ResetTrigger("LeaveJump");
        this.animator.SetTrigger("EnterJump");
        this.animator.SetBool("isJumping", true);

    }
    
    void OnSwapToKnight(InputValue value)
    {
        playerLogic.TransformPlayer(Characters.Knight);
        this.activeFootsteps = this.footstepsKnight;
        this.audioSource.clip = this.activeFootsteps[0];
    }

    void OnSwapToMage(InputValue value)
    {
        playerLogic.TransformPlayer(Characters.Mage);
        this.activeFootsteps = this.footstepsMage;
        this.audioSource.clip = this.activeFootsteps[0];
    }

    void Run()
    {
        Vector2 horizontal = new Vector2(this.moveInput.x * this.speed, this.rb.velocity.y);
        this.rb.velocity = horizontal;
    }
    
    public void Attack()
    {
        this.animator.SetTrigger("Attack");
    }

}
