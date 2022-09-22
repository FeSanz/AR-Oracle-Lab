using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AutofocusBehaviour : MonoBehaviour
{
    void Start()
    {
        VuforiaApplication.Instance.OnVuforiaStarted += OnVuforiaStarted;
        VuforiaApplication.Instance.OnVuforiaPaused += OnPaused;
    }
    /// <summary>
    /// Enfoque continuo de la cámara para una mejor detección
    /// </summary>
    private void OnVuforiaStarted()
    {
        VuforiaBehaviour.Instance.CameraDevice.SetFocusMode( FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        VuforiaBehaviour.Instance.CameraDevice.SetCameraMode(Vuforia.CameraMode.MODE_DEFAULT);
    }
    
    /// <summary>
    /// Reanuda el enfoque en caso de reanudar la app
    /// </summary>
    /// <param name="paused">Valida pausa</param>
    private void OnPaused(bool paused)
    {
        if (!paused) // Resumed
        {
            // Establecer de nuevo el modo de enfoque automático cuando se reanuda la app
            VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
}
