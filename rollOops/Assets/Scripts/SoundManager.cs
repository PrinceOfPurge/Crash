using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource coinsource;
    public AudioClip coinSound; 

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        coinsource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
