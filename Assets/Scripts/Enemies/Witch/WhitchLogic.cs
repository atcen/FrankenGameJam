using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhitchLogic : MonoBehaviour
{

    [SerializeField] public int health;
    [SerializeField] public int startingHealth = 99;

    [SerializeField] GameObject fireball;
    [SerializeField] Transform t;

    [SerializeField] GameObject ghost;
    [SerializeField] GameObject stone;
    public int phase = 0;
    public int maxPhases = 3;
    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        this.t = GetComponent<Transform>();
        NextPhase();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage, Characters from)
    {

        health -= damage;

        if (from == Characters.Knight) 
        {
            damage = damage * 3;
                }
        this.health -= damage;


        if (this.health <= 0)
        {
            Die();
        }
        else if (health/ (startingHealth / maxPhases) < maxPhases-phase)
        {
            NextPhase();
        }

        if(health <= startingHealth / 2)
        {
            transformWitch();
        }
    }

    public void NextPhase()
    {
        if(phase < maxPhases)
        {
            phase++;

            int minions = phase * 5;
            int ghosts = Random.Range(0, minions);

            for (int i = 0; i < minions; i++)
            {
                if (i < ghosts)
                {
                    spawnGhost();
                }
                else
                {
                    spawnStone();
                }
            }
        }
    }

    public void transformWitch()
    {
    }

    public void witchAttack()
    {

    }

    public void spawnStone()
    {
        Vector3 pos = transform.position;
        pos.x = pos.x + Random.Range(-5f, 5f);
        pos.y = pos.y + 3f;
        Instantiate(stone, pos , transform.rotation);
    }

    public void spawnGhost()
    {
        Vector3 pos = transform.position;
        pos.x = pos.x + 3f;
        pos.y = pos.y + Random.Range(-5f, 5f);
        Instantiate(ghost, pos , transform.rotation);
    }



}
