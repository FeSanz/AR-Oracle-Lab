using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SoundValidate : MonoBehaviour
{
    [SerializeField, Tooltip("Notificacion para aumentar volumen tipo SnackBar")]
    private GameObject SnackBar;

    [SerializeField, Tooltip("Audios de cada secuencia")]
    private AudioSource[] AudioStage;
    void Update()
    {
        if (AudioSettings.Mobile.muteState)
        {
            SnackBar.SetActive(true); 
        }
        else
        {
            SnackBar.SetActive(false);
        } 
    }
    
    public void MuteAudio()
    {
        foreach (AudioSource audio in AudioStage)
        {
            audio.mute = true;
        }
    }
    
    public void SpeakAudio()
    {
        foreach (AudioSource audio in AudioStage)
        {
            audio.mute = false;
        }
    }
    
    public void ReloadScene(string nameScene)
    {
        SceneManager.LoadScene(nameScene);
    }
   
}
