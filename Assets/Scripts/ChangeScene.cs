using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
  /// <summary>
  /// Cambio de escena por nombre
  /// </summary>
  /// <param name="nameScene">Nombre de la escena</param>
  public void ChangeScenebyName(string nameScene)
  {
    Screen.orientation = ScreenOrientation.AutoRotation; 
    SceneManager.LoadScene(nameScene);
  }

  /// <summary>
  /// Abrir direcciones url
  /// </summary>
  /// <param name="url">Link</param>
  public void OpenURL(string url)
  {
    Application.OpenURL(url);
  }
}
