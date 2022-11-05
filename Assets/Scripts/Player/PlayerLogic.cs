using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private Characters activeCharacter = Characters.Warrior;

    [SerializeField] private float attackCooldown = 60f;
    [SerializeField] private float activeAttackCooldown = 0f;

    [SerializeField] private float transformCooldown = 20f;
    [SerializeField] private float activeTransformCooldown = 0f;


    [SerializeField] private bool isAlive = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isAlive) { return; }
        if (activeCharacter == Characters.Mage)
        {
            recoverStamina(this.staminaReg);
        }
        else
        {
            recoverMana(this.manaReg);
        }
        if (activeAttackCooldown > 0)
        {
            activeAttackCooldown -= 1;
        }
        if (activeTransformCooldown > 0)
        {
            activeTransformCooldown -= 1;
        }
    }

    void attack()
    {
        if(activeAttackCooldown > 0) { return; }
        this.activeAttackCooldown = attackCooldown;
    }

    void heal(int healAmount)
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

    void recoverStamina(float staminaAmount)
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

    void recoverMana(float manaAmount)
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


    void takeDamage(int damage)
    {
        this.health = this.health - damage;
        if (this.health <= 0)
        {
            die();
        }
    }


    void die()
    {
        this.isAlive = false;
    }


    /*
     * Transforms between Mage and Warrior
     */
    void transformPlayer()
    {
        if(this.transformCooldown > 0) { return; }
        if (this.activeCharacter == Characters.Mage)
        {
            this.activeCharacter = Characters.Warrior;
        }
        else
        {
            this.activeCharacter = Characters.Mage;
        }
        this.activeTransformCooldown = this.transformCooldown;
    }
}



/*
 * Enum for possible Characters
 */
enum Characters
{
    Mage,
    Warrior
}

