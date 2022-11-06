using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    [SerializeField] private GameObject[] paragraphs;
    [SerializeField] private int[] delays;
    [SerializeField] private GameObject nextButton;
    private float timeSince = 0;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        paragraphs[index].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (index < (paragraphs.Length))
        {
            
            if (timeSince >= delays[index])
            {
                paragraphs[index].SetActive(false);
                index++;
                paragraphs[index].SetActive(true);
            }  
        }
        else
        {
            nextButton.SetActive(true);
        }
        
        Debug.Log(timeSince);
        timeSince += Time.deltaTime;

             
    }
    
    
}
