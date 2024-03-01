using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostFocus : MonoBehaviour
{
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        { 
            Application.Quit();
        }
    }
}
