using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    [SerializeField] private int health;

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
