using System;
using System.Collections;
using System.Collections.Generic;
using Crystal;
using Unity.VisualScripting;
using UnityEngine;

public class IntegralBehaviour : MonoBehaviour
{
    [SerializeField, Tooltip("Objetos del menu")] private GameObject[] menu;

    [SerializeField, Tooltip("Objetos del contenido")] private GameObject[] content;

    //[SerializeField, Tooltip("Script para muesca")] private SafeArea safeArea;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait; 
    }

    public void ContentShow(int index)
    {
        MenuHide();
        foreach (GameObject item in content)
        {
            item.SetActive(false);
        }
        content[0].SetActive(true);
        content[index].SetActive(true);
    }
    
    public void ContentHide()
    {
        foreach (GameObject item in content)
        {
            item.SetActive(false);
        }
        
        MenuShow();
    }
    
    
    private void MenuShow()
    {
        //safeArea.enabled = true;
        foreach (GameObject item in menu)
        {
            item.SetActive(true);
        }
    }
    
    private void MenuHide()
    {
        //safeArea.enabled = false;
        foreach (GameObject item in menu)
        {
            item.SetActive(false);
        }
    }
}
