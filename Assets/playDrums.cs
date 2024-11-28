using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Timers;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public enum drumkit 
{
    NODRUM,
    KICKDRUM,
    SNAREDRUM,
    HIHAT,
    RIDECYMBAL
}

public class playDrums : MonoBehaviour
{
    public drumkit drum;
    public Timer timer; 
    private bool isPlaying = false;
    private GameObject audio;
    private AudioSource audioSource; 
    private double[] timeSignatures = {};
    private int[,] beats = { {}, {} };

    void Start()
    {
        //timer.BeginInit();
        audio = GameObject.Find("Audio Manager");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isPlaying = true; 
            audioSource = audio.GetComponent<AudioSource>();
            audioSource.Play();
        }

        switch(drum) 
        {
            case drumkit.NODRUM:
            {
                //Do Nothing
            } break;
            case drumkit.KICKDRUM:
            {

            } break;
            case drumkit.SNAREDRUM:
            {

            } break;
            case drumkit.HIHAT:
            {

            } break;
            case drumkit.RIDECYMBAL:
            {

            } break;
            default:
            break;
        }  
    }

    void changeSprite(String objectName, Sprite newSprite)
    {
        GameObject drum = GameObject.Find(name);
        SpriteRenderer sprite = drum.GetComponent<SpriteRenderer>();
        
        sprite.sprite =  newSprite;
    }
}
