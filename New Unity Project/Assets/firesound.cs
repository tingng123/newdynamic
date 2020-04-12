using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firesound : MonoBehaviour
{   private AudioSource audioSource;
    public AudioClip glassbreak;
    // Start is called before the first frame update
    void Start()
    {
                audioSource = GetComponent<AudioSource>();
            audioSource.clip = glassbreak;
            audioSource.pitch = (Random.Range(0.7f, 1.6f));
        audioSource.Play();

   

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
