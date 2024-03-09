using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugFactor : MonoBehaviour
{

    public TMP_Text m_TextMeshPro;
    public MapSpriteShifter shifter;

    private void Start()
    {
        m_TextMeshPro.text = shifter.movementSmoothFactor.ToString();
    }

    public void Plus()
    {
        shifter.movementSmoothFactor += 0.001f;
        m_TextMeshPro.text = shifter.movementSmoothFactor.ToString();
    }

    public void Minus()
    {
        shifter.movementSmoothFactor -= 0.001f;
        m_TextMeshPro.text = shifter.movementSmoothFactor.ToString();
    }
}
