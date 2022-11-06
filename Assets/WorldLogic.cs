using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLogic : MonoBehaviour
{
    // refernce to child objects
    [SerializeField] private GameObject deadlands;
    [SerializeField] private GameObject greenlands;
    private float timeSinceLastSwitch = 0f;
    // Start is called before the first frame update
    void Start()
    {
        this.deadlands.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // random chance of 1 in 1000 to change the world
        if (Random.Range(0, 1000) == 0 && timeSinceLastSwitch > 10f)
        {
            TransformWorld();
            timeSinceLastSwitch = 0f;
        }
        //delta time is the time since the last frame
        timeSinceLastSwitch += Time.deltaTime;
    }
    
    void TransformWorld()
    {
        this.deadlands.SetActive(!this.deadlands.activeSelf);
        this.greenlands.SetActive(!this.greenlands.activeSelf);
    }
}
