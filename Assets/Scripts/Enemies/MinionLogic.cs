using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float scale = Random.Range(1f, 3f);
        transform.localScale = new Vector2(scale, scale);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
