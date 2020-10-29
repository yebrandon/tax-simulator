using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Button : MonoBehaviour
{

    // Update is called once per frame
    public void nextScene()
    {
        Debug.Log("a");
        SceneManager.LoadScene(sceneName: "intro_scene");
    }
}
