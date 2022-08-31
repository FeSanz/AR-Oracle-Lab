using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField, Tooltip("Velocidad de rotación con ratón")]
    private float SpeedRotation = 2.0f;
    [SerializeField, Tooltip("Velocidad de rotación automática")]
    private float SpeedRotationAuto = 6.0f;
    [SerializeField, Tooltip("Campo de visión de la camara por default")]
    private float FieldOfViewDefault = 60.0f;
    [SerializeField, Tooltip("Velocidad del zoom automático")]
    private float SpeedZoomAuto = 0.005f;
    [SerializeField, Tooltip("Valor máximo para alejarse con zoom")]
    private float ZoomMaximum = 70.0f;
    [SerializeField, Tooltip("Valor minímo para acercarse con zoom")]
    private float ZoomManimum = 30.0f;
    [SerializeField, Tooltip("Tiempo de espera para activar la autorotación")]
    private float waitTime;

    private Camera _camera;
    private float _idleTime = 0;
    
    private float _horizontal = 0f;
    private float _vertical = 0f;
    

    void Update()
    {
        
        
        //Valida y ejecuta rotacion y zoom con raton
        if (Input.GetMouseButton(0))
        {
            _idleTime = 0;

            _vertical -= SpeedRotation * Input.GetAxis("Mouse Y");
            _horizontal += SpeedRotation * Input.GetAxis("Mouse X");
            
            transform.eulerAngles = new Vector3(_vertical, _horizontal, 0f);
        } 
        else if (_camera.fieldOfView < ZoomMaximum && Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _idleTime = 0;
            _camera.fieldOfView = _camera.fieldOfView + 5;
        } 
        else if (_camera.fieldOfView > ZoomManimum && Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _idleTime = 0;
            _camera.fieldOfView = _camera.fieldOfView - 5;
        }
        else
        {
            _idleTime += Time.deltaTime; //Inicia tiempo de inactividad
        }
    }
 
    
}
