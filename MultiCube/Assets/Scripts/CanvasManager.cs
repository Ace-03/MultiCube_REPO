using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject canvas;
    public void OnLoggedIn()
    {
        canvas.SetActive(true);
    }
}
