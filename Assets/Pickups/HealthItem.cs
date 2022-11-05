using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private int healthAmount = 1;
    
    // reference to boxcollider as trigger
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        this._boxCollider2D = GetComponent<BoxCollider2D>();
        this.rb = GetComponent<Rigidbody2D>();
        
        // give item random velocity upwards
        this.rb.AddForce(new Vector2(Random.Range(-4, 4), 5), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // when player enters the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if the player enters the trigger
        if (other.gameObject.CompareTag("Player"))
        {
            // Disable the box collider to prevent the player from getting the health item again
            this._boxCollider2D.enabled = false;
            
            // destroy the object
            Destroy(gameObject);
            
            // add health to the player
            other.gameObject.GetComponent<PlayerLogic>().Heal(healthAmount);
        }
    }
    
    
}
