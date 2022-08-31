using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuBahaviour : MonoBehaviour
{
    [SerializeField, Tooltip("GridLayout del menú de opciones")]
    private GridLayoutGroup _gridLayoutGroup;

    [Header("ORIENTACION VERTICAL")]
    [SerializeField] private int[] _paddingPortrait;
    [SerializeField] private int[] _spacingPortrait;
    
    [Header("ORIENTACION HORIZONTAL")]
    [SerializeField] private int[] _paddingLandscape;
    [SerializeField] private int[] _spacingLandscape;

    void Update()
    {
        StartCoroutine(CheckForChangeOrientationScreen());
    }
    
    IEnumerator CheckForChangeOrientationScreen() {
        switch (Input.deviceOrientation) 
        {
            //No se puede determinar la orientación del dispositivo.
            case DeviceOrientation.Unknown:
                yield return null;
                break;
            //Dispositivo en posición vertical y botón de inicio en la parte inferior
            case DeviceOrientation.Portrait: 
                ConstraintColumCount(1, _paddingPortrait[0], _paddingPortrait[1],_spacingPortrait[0], _spacingPortrait[1]);
                break;
            //Dispositivo en posición vertical y botón de inicio en la parte superior
            case DeviceOrientation.PortraitUpsideDown:
                ConstraintColumCount(1, _paddingPortrait[0], _paddingPortrait[1],_spacingPortrait[0], _spacingPortrait[1]);
                break;
            //Dispositivo en posición horizontal y botón de inicio en el lado izquierdo
            case DeviceOrientation.LandscapeLeft:
                ConstraintColumCount(2, _paddingLandscape[0], _paddingLandscape[1],_spacingLandscape[0], _spacingLandscape[1]);
                break;
            //Dispositivo en posición horizontal y botón de inicio en el lado derecho
            case DeviceOrientation.LandscapeRight:
                ConstraintColumCount(2, _paddingLandscape[0], _paddingLandscape[1],_spacingLandscape[0], _spacingLandscape[1]);
                break;
            default:
                yield return null;
                break;
        }
    }

    /// <summary>
    /// Establece el tipo de limitación de layout
    /// </summary>
    /// <param name="count">Numero de columnas</param>
    private void ConstraintColumCount(int columns, int paddingTop,int paddingBottom, int spacing_x, int spacing_y)
    {
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;  //Layout por columnas
        _gridLayoutGroup.constraintCount = columns; //Numero de columnas
        _gridLayoutGroup.padding.top = paddingTop;
        _gridLayoutGroup.padding.bottom = paddingBottom;
        _gridLayoutGroup.spacing = new Vector2(spacing_x, spacing_y);
    }

    public void ExitOfApplication()
    {
        Application.Quit();
    }
}
