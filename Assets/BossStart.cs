using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class BossStart : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            _audioSource.Play();
            // change lens orthographic size
            _cinemachineVirtualCamera.m_Lens.OrthographicSize = 7;
            // TODO lerp value 
        }
    }

}
