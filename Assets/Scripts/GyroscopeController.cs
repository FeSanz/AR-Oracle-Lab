using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GyroscopeController : MonoBehaviour
{
    private Gyroscope _gyroscope;
    private bool _gyroEnabled;
    
    private Camera _camera;
    void Start()
    {
        _gyroEnabled = EnabledGyro();
        
         _camera = GetComponent<Camera>();
         StartCoroutine(WaitStart());
        
    }
    
    void Update()
    {
        StartCoroutine(OrientationScreen());
        if (_gyroEnabled)
        {
            //transform.Rotate(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, Input.gyro.rotationRateUnbiased.z);
            
            Vector3 previousEulerAngles = transform.eulerAngles;
            Vector3 gyroInput = -Input.gyro.rotationRateUnbiased;

            Vector3 targetEulerAngles = previousEulerAngles + gyroInput * Time.deltaTime * Mathf.Rad2Deg;
            targetEulerAngles.x = 0.0f;
            targetEulerAngles.z = 0.0f;

            transform.eulerAngles = targetEulerAngles;
        }
    }

    /// <summary>
    /// Verifica soporte el giroscopio y lo habilita
    /// </summary>
    /// <returns></returns>
    private bool EnabledGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            _gyroscope = Input.gyro;
            _gyroscope.enabled = true;
            return true;
        }

        return false;
    }
    
    /// <summary>
    /// Desactivar animacion inicial de cámara
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitStart()
    {
        yield return new WaitForSeconds(2.7f);
        GetComponent<Animator>().enabled = false;
    }
    
    /// <summary>
    /// Verifica orientacion del dispositivo
    /// </summary>
    /// <returns></returns>
    IEnumerator OrientationScreen() {
        //Orientacion vertical
        if (Input.deviceOrientation == DeviceOrientation.Portrait ||
            Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            _camera.fieldOfView = 100;  
        }
        //Orientacion horizontal
        else if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft ||
              Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            _camera.fieldOfView = 50;
        }
        else
        {
            _camera.fieldOfView = 100;
            yield return null;
        }
    }
}
