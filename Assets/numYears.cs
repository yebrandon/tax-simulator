using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TaxSim;
using UnityEngine.SceneManagement;

public class numYears : MonoBehaviour
{
    Wallet wallet = new Wallet();
    int numOfYears = 17;
    Text instruction;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        instruction = GetComponent<Text>();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    // Update is called once per frame
    void Update()
    {
        instruction.text = "Years: " + numOfYears;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        if (scene.name == "black")
        {
            numOfYears++;
        }
        if (numOfYears >= 23 && numOfYears <= 25)
        {
            numOfYears = 65;
            SceneManager.LoadScene("end_scene");
        }


    }
}
