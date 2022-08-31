using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite[] frameGif;
    [SerializeField] private Image ImageObject;

    void Update()
    {
        ImageObject.sprite = frameGif[(int) (Time.time * 10) % frameGif.Length];
    }
}
