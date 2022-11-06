using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;
using Slider = UnityEngine.UIElements.Slider;


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

    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float activeAttackCooldown = 0f;

    [SerializeField] private float transformCooldown = 2f;
    [SerializeField] private float activeTransformCooldown = 0f;

    [SerializeField] public bool isAlive = true;

    [SerializeField] GameObject fireball;
    [SerializeField] Transform fireballSpawn;

    float damageCooldown = 0f;
    [SerializeField] float damageMaxCooldown = 2f;

    private PlayerMovement playerMovement;
    private CapsuleCollider2D myBodyCollider;
    private HammerScript hammer;

    private Animator _animator;
    
    // reference to audio source
    private AudioSource[] _audioSources;
    private AudioSource _hitSoundSource;
    private AudioSource _attackSoundSource;
    [SerializeField] private AudioClip[] _hitSounds;
    [SerializeField] private AudioClip[] _fireballSounds;
    
    // Start is called before the first frame update
    void Start()
    {
        this.playerMovement = GetComponent<PlayerMovement>();
        this.myBodyCollider = GetComponent<CapsuleCollider2D>();
        this._animator = GetComponent<Animator>();
        this.fireballSpawn = GetComponent<Transform>();
        this.hammer = gameObject.transform.Find("Hammer").GetComponent<HammerScript>();
        this._audioSources = GetComponents<AudioSource>();
        
        this._hitSoundSource = _audioSources[1];
        this._attackSoundSource = _audioSources[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.isAlive) { return; }
        EnemyDetection();
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
            this.activeAttackCooldown -= Time.deltaTime;
        }
        if (this.activeTransformCooldown > 0)
        {
            this.activeTransformCooldown -= Time.deltaTime;
        }
        if (this.damageCooldown > 0)
        {
            this.damageCooldown -= Time.deltaTime;
        }
    }

    public void OnAttack(InputValue value)
    {
        if(this.activeAttackCooldown > 0) { return; }
        this.playerMovement.Attack();
        if (this.activeCharacter == Characters.Mage)
        {
            Instantiate(fireball, fireballSpawn.position, transform.rotation);
            this.activeAttackCooldown = this.attackCooldown*0.5f;
            this._attackSoundSource.clip = this._fireballSounds[Random.Range(0, this._fireballSounds.Length)];
            this._attackSoundSource.Play();
        }
        else
        {
            this.activeAttackCooldown = this.attackCooldown;
            hammer.Attack(1);
        }
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
        if(damageCooldown > 0) { return; }
        this.health = this.health - damage;
        _hitSoundSource.clip = _hitSounds[Random.Range(0, _hitSounds.Length)];
        _hitSoundSource.Play();
        damageCooldown = damageMaxCooldown;
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
        if (!isAlive) { return; }
        if (this.activeTransformCooldown > 0) { return; }
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

    private void EnemyDetection()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            this.TakeDamage(1);
        }
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