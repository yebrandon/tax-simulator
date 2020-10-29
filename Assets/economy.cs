using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TaxSim;
using UnityEngine.SceneManagement;

public class economy : MonoBehaviour
{
    public static Wallet wallet = new Wallet();
    public static float income = 20000;
    bool wasAtParty = false;

    Text instruction;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
        instruction = GetComponent<Text>();
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    // Update is called once per frame
    void Update()
    {
        instruction.text = "Balance: $" + wallet.GetBalance();
        if (SceneManager.GetActiveScene().name == "work_scene")
        {

        }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);
        if (scene.name == "black")
        {
            Debug.Log("Black level loaded.");
            wallet.Tick();
            wallet.ReceivePaycheck(income);
            if (wasAtParty) {
                Debug.Log("help");
                wallet.Spend(10000);
                wasAtParty = false;
            }

        }
        if (scene.name == "party_scene")
        {
            wasAtParty = true;
        }
    }
}
