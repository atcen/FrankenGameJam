using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseEnemy : MonoBehaviour
{

    [SerializeField] public int health = 1;
    [SerializeField] private GameObject healthItem;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        if(this.health <= 0)
        {
            Die();
        }
    }
}
