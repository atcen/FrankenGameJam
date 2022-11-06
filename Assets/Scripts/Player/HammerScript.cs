using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript: MonoBehaviour
{

    private List<Collider2D> gegner;

    // Start is called before the first frame update
    void Start()
    {
        gegner = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemies")
        {
            gegner.Add(collision);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Enemies")
        {
            gegner.Remove(collision);
        }
    }

    public void Attack(int attack)
    {
        foreach(Collider2D c in gegner)
        {
            c.gameObject.GetComponent<BaseEnemy>().TakeDamage(1);
        }
    }
}
