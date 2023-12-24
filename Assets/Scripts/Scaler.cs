using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    public float scale = 1;
    public float maxScale = 3.5f;
    public float minScale = 0.6f;

    public MapSpriteShifter shifter;

    private void Start()
    {
        Rescale();
    }

    public void ScaleIncrease()
    { 
        if (scale < maxScale) 
        {
            scale *= 2;
            Rescale();
        }
    }

    public void ScaleDecrease()
    {
        if (scale > minScale)
        {
            scale /= 2;
            Rescale();
        }
    }

    private void Rescale()
    { 
        transform.localScale = new Vector3 (scale, scale, 1);
        shifter.UpdateImage();
    }

}
