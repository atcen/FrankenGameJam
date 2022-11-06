using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HammerScript: MonoBehaviour
{
    CapsuleCollider2D hammerCollider;
    // Start is called before the first frame update
    void Start()
    {
        hammerCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void Attack(int attack)
    {
        Collider2D[] enemies = Physics2D.OverlapCapsuleAll(hammerCollider.bounds.center, hammerCollider.size, hammerCollider.direction, 0);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.gameObject.tag == "Enemies")
            {
                enemy.gameObject.GetComponent<BaseEnemy>().TakeDamage(attack, Characters.Knight);
            }
        }
    }
}
