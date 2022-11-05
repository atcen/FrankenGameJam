using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


/*
 * Basic logic for the player;
 */
public class PlayerLogic : MonoBehaviour
{

    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int health = 5;

    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float mana = 100f;

    [SerializeField] private float maxStamina = 100f;
    [SerializeField] private float stamina = 100f;

    [SerializeField] private float manaReg = 0.1f;
    [SerializeField] private float staminaReg = 0.1f;

    [SerializeField] public Characters activeCharacter = Characters.Knight;

    [SerializeField] private float attackCooldown = 60f;
    [SerializeField] private float activeAttackCooldown = 0f;

    [SerializeField] private float transformCooldown = 20f;
    [SerializeField] private float activeTransformCooldown = 0f;

    [SerializeField] private bool isAlive = true;

    private PlayerMovement playerMovement;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        this.playerMovement = GetComponent<PlayerMovement>();
        this._animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isAlive) { return; }
        if (this.activeCharacter == Characters.Mage)
        {
            RecoverStamina(this.staminaReg);
        }
        else
        {
            RecoverMana(this.manaReg);
        }
        if (this.activeAttackCooldown > 0)
        {
            this.activeAttackCooldown -= 1;
        }
        if (this.activeTransformCooldown > 0)
        {
            this.activeTransformCooldown -= 1;
        }
    }

    public void OnAttack(InputValue value)
    {
        if(this.activeAttackCooldown > 0) { return; }
        this.activeAttackCooldown = this.attackCooldown;
        playerMovement.Attack();
    }

    public void Heal(int healAmount)
    {
        if (this.health + healAmount < this.maxHealth)
        {
            this.health += healAmount;
        }
        else
        {
            this.health = this.maxHealth;
        }
    }

    void RecoverStamina(float staminaAmount)
    {
        if (this.stamina + staminaAmount < this.maxStamina)
        {
            this.stamina += staminaAmount;
        }
        else
        {
            this.stamina = this.maxStamina;
        }
    }

    void RecoverMana(float manaAmount)
    {
        if (this.mana + manaAmount < this.maxMana)
        {
            this.mana += manaAmount;
        }
        else
        {
            this.mana = this.maxMana;
        }
    }


    public void TakeDamage(int damage)
    {
        this.health = this.health - damage;
        if (this.health <= 0)
        {
            this.Die();
        }
    }


    public void Die()
    {
        this.isAlive = false;
    }


    /*
     * Transforms between Mage and Warrior
     */
    public void TransformPlayer(Characters character)
    {
        if(this.activeTransformCooldown > 0) { return; }
        if (character == activeCharacter) { return; }
        
        if (character == Characters.Knight)
        {
            this.activeCharacter = Characters.Knight;
            // Switch Animator Layer
            this._animator.SetTrigger("FadeOut");
            this._animator.SetLayerWeight(1,0);
            this._animator.SetLayerWeight(0,1);
            this._animator.SetTrigger("FadeIn");
        }
        else
        {
            this.activeCharacter = Characters.Mage;
            // Switch Animator Layer
            this._animator.SetTrigger("FadeOut");
            this._animator.SetLayerWeight(0,0);
            this._animator.SetLayerWeight(1,1);
            this._animator.SetTrigger("FadeIn");
        }
        this.activeTransformCooldown = this.transformCooldown;
    }
}



/*
 * Enum for possible Characters
 */
public enum Characters
{
    Mage,
    Knight
}

