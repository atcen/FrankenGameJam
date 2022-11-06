using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BaseEnemy : MonoBehaviour
{

    [SerializeField] public int health = 1;
    [SerializeField] private GameObject healthItem;
    [SerializeField] public Characters weakTo;
    
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _hitSounds;

    // Start is called before the first frame update
    void Start()
    {
        this._audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Destroy(gameObject);
        
        // Spawn Health Item with a chance of 33%
        if (Random.Range(0, 3) == 0)
        {
            Instantiate(healthItem, transform.position, Quaternion.identity);
        }
    }

    public void TakeDamage(int damage, Characters from)
    {
        if(from != weakTo) { return; }
        this.health -= damage;
        if (Characters.Knight == from)
        {
            this._audioSource.clip = this._hitSounds[Random.Range(0, this._hitSounds.Length)];
            this._audioSource.Play();
        }
        if(this.health <= 0)
        {
            Die();
        }
    }
}
