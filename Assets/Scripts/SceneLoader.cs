using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private static int number = 0;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public void LoadScene(int sceneNumber)
    {
        number++;
        SceneManager.LoadScene(sceneNumber);
    }

    public int GetNumber() 
    {
        return number;
    }
}
