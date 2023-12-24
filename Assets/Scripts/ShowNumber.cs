using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowNumber : MonoBehaviour
{
    public SceneLoader loader;
    public TMP_Text t;

    // Start is called before the first frame update
    void Start()
    {
        t.text = loader.GetNumber().ToString();
    }
}
